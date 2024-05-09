using ETıcaretAPI.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ETıcaretAPI.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
           await SendMailAsync(new[] {to}, subject, body, isBodyHtml);
        }

        public async Task SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            mail.Subject = subject;
            mail.Body = body;
            foreach (var to in tos) 
            {
                mail.To.Add(to);
            }
            mail.From = new(_configuration["Mail:UserName"], "IronStore",System.Text.Encoding.UTF8);

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:UserName"], _configuration["Mail:Password"]);
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Host = "smtp.gmail.com";
            await smtp.SendMailAsync(mail);
        }

        public async Task SendPasswordResetMailAsync(string to,string userId,string resetToken)
        {
            StringBuilder mail = new();
            mail.AppendLine($"Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"{_configuration["AngularClientUrl"]}/update-password/{userId}/{resetToken}\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>-IronStore");

            //StringBuilder mail = new();
            //mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            //mail.AppendLine($"{_configuration["AngularClientUrl"]}/update-password/"); 
            ////mail.AppendLine("/update-password/");
            //mail.AppendLine(userId);
            //mail.AppendLine("/");
            //mail.AppendLine(resetToken);
            //mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>-IronStore");
            await SendMailAsync(to, "Şifre Yenileme Talebi", mail.ToString());
        }
    }
}
