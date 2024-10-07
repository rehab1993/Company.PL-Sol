using Company.DAL.Models;
using Company.PL.Settings;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Net;
//using System.Net.Mail;

namespace Company.PL.Helpers
{
    public  class EmailSettings : IMailService
    {
        private  MailSettings  _options;

        public EmailSettings(IOptions<MailSettings> options) {
            _options = options.Value;
        }
        //public static void SendEmail(Email email)
        //{
        //    var Client = new SmtpClient("smtp.gmail.com", 587);
        //    Client.EnableSsl = true;
        //    Client.Credentials = new NetworkCredential("Rehab123abdallah@gmail.com", "Ay7aga");
        //    Client.Send("Rehab123abdallah@gmail.com", email.To, email.Subject, email.Body);
        //}
        public void SendEmail(Email email)
        {
            var mail = new MimeMessage {
            Sender = MailboxAddress.Parse(_options.Email),
            Subject = email.Subject,
            };       
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.DisplayName,_options.Email));
            var builder = new BodyBuilder();
            builder.TextBody = email.Body;
            mail.Body=builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host, _options.Port,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email, _options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
