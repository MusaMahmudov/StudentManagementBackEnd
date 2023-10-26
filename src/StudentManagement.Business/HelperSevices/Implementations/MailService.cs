using AutoMapper.Internal;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using StudentManagement.Business.DTOs;
using StudentManagement.Business.HelperSevices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Business.HelperSevices.Implementations
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(MailRequestDTO mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_configuration["MailSettings:Mail"]);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            if (mailRequest.Attachments != null)
            {
                byte[] fileBytes;
                foreach (var file in mailRequest.Attachments)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file.CopyTo(ms);
                            fileBytes = ms.ToArray();
                        }
                        builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
                    }
                }
            }
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration["MailSettings:Host"], int.Parse(_configuration["MailSettings:Port"]), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration["MailSettings:Mail"], _configuration["MailSettings:Password"]);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}
