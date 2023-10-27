using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestEmail()
        {
            return View("FrmRequestEmail");

        }

        public IActionResult SendVerificationCode(string email)
        {
            string subject = "Código de Recuperação";
            string body = "Seu código de recuperação é " + GenerateCode(); ;

            var mailService = new MailService();

            mailService.SendMail(email, subject, body);

            return RedirectToAction("Index");
        
        }

        public IActionResult ForgetPassword() 
        {
            return View("recuperacao");
        }

        public string GenerateCode()
        { 
            var random = new Random();
            string code = string.Empty;

            for (int i = 0; i < 6; i++)
            {
                code += random.Next(1, 101).ToString();
            }
        
            return code;
        }

    }
}
