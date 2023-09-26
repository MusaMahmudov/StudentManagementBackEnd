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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddBusinessService();
builder.Services.AddDataAccessServices(builder.Configuration);
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
        if(feature.Error is IBaseException)
        {
            var exception = (IBaseException)feature.Error;
            statusCode = exception.HttpStatusCode;
            message = exception.errorMessage;
        }
        var response = new ResponseDTO(statusCode,message);
        context.Response.StatusCode = (int)statusCode;
         await context.Response.WriteAsJsonAsync(response);
        await context.Response.CompleteAsync();
    });
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
