using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System.Configuration;

namespace OnlineMobileRecharge.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string appMail = "muhammadmohsinkhan.aptech@gmail.com";
        private readonly string appPassword = "xabi giuz jqdb uilq";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //var appSettingsReader = new AppSettingsReader();
            //var pp = appSettingsReader.GetValue("Email", typeof(string));
            //var appMail = appSettingsReader.GetValue("Mail", typeof(string)).ToString();
            //var appPassword = appSettingsReader.GetValue("Password", typeof(string)).ToString();

            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(appMail));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            //xabi giuz jqdb uilq
            // to generate app password https://myaccount.google.com/apppasswords

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate(appMail, appPassword);
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }
            return Task.CompletedTask;
        }
    }
}
