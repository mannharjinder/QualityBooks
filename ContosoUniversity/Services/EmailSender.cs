using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

namespace ContosoUniversity.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            var mes = new MimeMessage();
            mes.From.Add(new MailboxAddress("Mann", "mannh04@myunitec.ac.nz"));
            mes.To.Add(new MailboxAddress("User", email));
            mes.Subject = subject;

            mes.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                client.Connect("smtp.office365.com", 587, false);

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                client.Authenticate("mannh04@myunitec.ac.nz", "MyOffice365");

                client.Send(mes);
                client.Disconnect(true);
            }
                return Task.CompletedTask;
        }
    }
}
