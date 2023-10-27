using System.Net;
using System.Net.Mail;

namespace IndigoErp.Services
{
    public class MailService : IMailService
    {
        private static string setAdress = "smtp.gmail.com";

        private static int setPortNumber= 587;

        private static string mailName = "indigoerp3773@gmail.com";

        private static string mailPassword = "kamilymatheusendrewpablo";

        public void SendRecoveryCode(string email, string subject, string body)
        { 
        
            var mailMessage = new MailMessage();
            var smtp = new SmtpClient();

            mailMessage.From = new MailAddress(mailName);

            mailMessage.To.Add(email);

            mailMessage.Subject = subject;

            mailMessage.Body = body;

            mailMessage.IsBodyHtml = false;

            smtp.EnableSsl= true;

            smtp.UseDefaultCredentials= false;

            smtp.Credentials = new NetworkCredential(mailName, mailPassword);

            smtp.Send(mailMessage);

        }

    }
}
