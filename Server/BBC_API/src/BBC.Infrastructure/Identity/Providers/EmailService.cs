using BBC.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BBC.Infrastructure.Identity.Providers
{
    public class EmailService: IEmailService
    {
        private readonly ConfigEmail _email;
        //private readonly IWebHostEnvironment _env;

        public EmailService(
            ConfigEmail email
            //IWebHostEnvironment env
            )
        {
            _email = email;
            //_env = env;
        }


        public async Task SendAsync(string EmailDisplayName, string Subject, string Body, string From, string To)
        {
            using (var client = new SmtpClient(_email.SMTPServer, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(EmailDisplayName, Subject, Body, From, To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendEmailConfirmationAsync(string EmailAddress, string CallbackUrl)
        {
            using (var client = new SmtpClient(_email.SMTPServer, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, "Confirm your email", $"Please confirm your email by clicking here: <a href='{CallbackUrl}'>link</a>", _email.From, EmailAddress, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendPasswordResetAsync(string EmailAddress, string CallbackUrl)
        {
            using (var client = new SmtpClient(_email.SMTPServer, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }

                PrepareMailMessage(_email.DisplayName, "Reset your password", $"Please reset your password by clicking here: <a href='{CallbackUrl}'>link</a>", _email.From, EmailAddress, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendException(Exception ex)
        {
            using (var client = new SmtpClient(_email.SMTPServer, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }
                //{_env.EnvironmentName}
                PrepareMailMessage(_email.DisplayName, $"(EnvironmentName) INTERNAL SERVER ERROR", $"{ex.ToString()}", _email.From, _email.To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        public async Task SendSqlException(SqlException ex)
        {
            using (var client = new SmtpClient(_email.SMTPServer, _email.Port))
            using (var mailMessage = new MailMessage())
            {
                if (!_email.DefaultCredentials)
                {
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_email.UserName, _email.Password);
                }
                //{_env.EnvironmentName}

                PrepareMailMessage(_email.DisplayName, $"(EnvironmentName) SQL ERROR", $"{ex.ToString()}", _email.From, _email.To, mailMessage);

                await client.SendMailAsync(mailMessage);
            }
        }

        private void PrepareMailMessage(string EmailDisplayName, string Subject, string Body, string From, string To, MailMessage mailMessage)
        {
            mailMessage.From = new MailAddress(From, EmailDisplayName);
            mailMessage.To.Add(To);
            mailMessage.Body = Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = Subject;
        }
    }
}
