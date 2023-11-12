using Microsoft.EntityFrameworkCore;
using StudentManagement.Business.Mappers;
using StudentManagement.Business.Services.Implementations;
using StudentManagement.Business.Services.Interfaces;
using StudentManagement.DataAccess.Contexts;
using StudentManagement.DataAccess.Repositories.Implementations;
using StudentManagement.DataAccess.Repositories.Interfaces;
using StudentManagement.DataAccess;
using StudentManagement.Business;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using StudentManagement.Business.Exceptions;
using StudentManagement.Business.DTOs.CommonDTOs;
using StudentManagement.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using StudentManagement.Business.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccessServices(builder.Configuration);
builder.Services.AddScoped<AppDbContextInitializer>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecurityKey"])),
        
    };

});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigin", builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
        builder.AllowAnyOrigin();
    });

});
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan =TimeSpan.FromHours(4);
});
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireDigit = true;
    options.Lockout.AllowedForNewUsers = true;
    options.Lockout.MaxFailedAccessAttempts = 3;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);

}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandler(error =>
{
    error.Run(async context =>
    {
        var feature = context.Features.Get<IExceptionHandlerFeature>();
        var statusCode = HttpStatusCode.InternalServerError;
        string message = feature.Error.Message;
        if (feature.Error is IBaseException)
        {
            var exception = (IBaseException)feature.Error;
            statusCode = exception.HttpStatusCode;
            message = exception.errorMessage;
        }
        var response = new ResponseDTO(statusCode, message);
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(response);
        await context.Response.CompleteAsync();
    });
});

using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
    await initializer.InitializerAsync();
    await initializer.UserSeedAsync();

};
app.UseHttpsRedirection();
app.UseCors("AllowAllOrigin");
app.UseAuthentication();
app.UseMiddleware<PreventLoginIfAuthenticatedMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
