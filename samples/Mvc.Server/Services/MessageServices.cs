using System;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Mvc.Server.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link http://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(email, subject, message);
        }

        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }


        private async Task Execute(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("kazanSummit", "sjbhdvghchjskjcj@gmail.com"));

            emailMessage.To.Add(new MailboxAddress("", email));

            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };


            using var client = new SmtpClient();

            try
            {
                if (!client.IsConnected)
                {
                    await client.ConnectAsync("smtp.gmail.com", 465, true);
                    
                    //await client.ConnectAsync("smtp.yandex.ru", 25, false);
                }

                if (!client.IsAuthenticated)
                {
                    await client.AuthenticateAsync("sjbhdvghchjskjcj@gmail.com", "edik1121");

                    //await client.AuthenticateAsync("info@vmetre.com", "Hl");
                }

                await client.SendAsync(emailMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
        }
    }
}
