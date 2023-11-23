using System.Net;
using System.Net.Mail;

namespace IndigoErp.Services
{
    public class MailService : IMailService
    {
        private string setAdress => "smtp.gmail.com";

        private int setPortNumber => 587;

        private string mailName => "indigoerp3773@gmail.com";

        private  string mailPassword => "gmwq eirn wmny asly";

        public  void SendMail(string email, string subject, string body)
        { 
        
            var mailMessage = new MailMessage();
            var smtp = new SmtpClient(setAdress,setPortNumber);

            mailMessage.From = new MailAddress(mailName);

            mailMessage.To.Add(email);

            mailMessage.Subject = subject;

            mailMessage.Body = body;

            mailMessage.IsBodyHtml = false;;

            smtp.EnableSsl= true;

            smtp.UseDefaultCredentials= false;

            smtp.Credentials = new NetworkCredential(mailName, mailPassword);

            smtp.Send(mailMessage);

        }

    }
}
