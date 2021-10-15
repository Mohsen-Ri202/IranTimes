using Microsoft.VisualStudio.Web.CodeGeneration.Utils.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IranTimes
{
    public class MessageSender : IMessageSender
    {
        public Task SendEmailAsync(string toEmail, string Subject, string Message, bool IsMessageHtml = false)
        {
            using (var Client=new SmtpClient())
            {
                var credential = new NetworkCredential()
                {
   UserName= "mohsebwater1379",
    Password="mr20142014"
                };
                Client.Credentials = credential;
                Client.Host = "smtp.gmail.com";
                Client.Port = 587;
                Client.EnableSsl = true;
                //Client.UseDefaultCredentials = true;
                using var emailmessage = new MailMessage()
                {
                    To = { new MailAddress(toEmail) },
                    From = new MailAddress("mohsebwater1379@gmail.com"),
                    Subject = Subject,
                    Body = Message,
                    IsBodyHtml = IsMessageHtml
                };
                Client.Send(emailmessage);
            }
            return Task.CompletedTask;
        }
    }
}
