using E_commerce.Application.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce.Infrastructure.Services
{
    public sealed class MailService : IMailService
    {
        readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(to);
            mail.Subject=subject;
            mail.Body=body;
            mail.From = new("mmdlorkhan@gmail.com", "E-commerce", System.Text.Encoding.UTF8);
            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential("mmdlorkhan", "orkhan6991");
            smtp.Port = 587;
            smtp.EnableSsl= true;
            smtp.Host = "smtp.gmail.com";
            await smtp.SendMailAsync(mail);
        }
    }
}
