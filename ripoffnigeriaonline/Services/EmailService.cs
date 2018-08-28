using Microsoft.AspNet.Identity;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ripoffnigeria.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            //await configSendGridasync(message);
            sendMail(message);
        }
        void sendMail(IdentityMessage message)
        {

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("info@rip-offnigeria.com");
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.Body = message.Body;
            msg.IsBodyHtml = true;
            try
            {
                SmtpClient smtpClient = new SmtpClient("mail.rip-offnigeria.com");
                NetworkCredential Credentials = new NetworkCredential("info@rip-offnigeria.com", "S3cur!ty1984kemi");
                smtpClient.Credentials = Credentials;
                smtpClient.Send(msg);
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
            
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        //private async Task configSendGridasync(IdentityMessage message)
        //{
        //    try
        //    {
        //        SmtpClient client = new SmtpClient();

        //        MailMessage myMessage = new MailMessage();
        //        string userName = "ablework9@gmail.com";
        //        string password = "ad3bayoOy3wol3";
        //        myMessage.To.Add(message.Destination);
        //        myMessage.From = new System.Net.Mail.MailAddress("ablwork9@gmail.com");
        //        myMessage.Subject = message.Subject;
        //        myMessage.Body = message.Body;

        //        client.Port = 587;
        //        client.Credentials = new System.Net.NetworkCredential(userName,password);
        //        client.Host = "smtp.gmail.com";
        //        client.EnableSsl = true;
        //        client.Timeout = 10000;
        //        client.Send(myMessage);
        //    }
        //    catch(Exception ex)
        //    {

        //        Console.WriteLine(ex.Message);
        //    }

        //}

    }
}


