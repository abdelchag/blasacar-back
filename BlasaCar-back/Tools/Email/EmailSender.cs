using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Email
{
    public class EmailSender : IEmailSender
    {
        public  string _apiKey;
        public  string _fromName;
        public  string _fromEmail;

        //public EmailSender(IConfiguration config)
        //{
        //    _apiKey = config["SendGrid:ApiKey"];
        //    _fromName = config["SendGrid:FromName"];
        //    _fromEmail = config["SendGrid:FromEmail"];

        //}

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);
        }

        Task IEmailSender.SendEmailAsync(string email, string subject, string message)
        {
            throw new NotImplementedException();
        }
    }
}
