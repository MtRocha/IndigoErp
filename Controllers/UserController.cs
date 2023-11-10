using IndigoErp.DAO;
using IndigoErp.Models;
using IndigoErp.Services;
using Microsoft.AspNetCore.Mvc;

namespace IndigoErp.Controllers
{
    public class UserController : GeneralController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestEmail()
        {
            ViewBag.Title = "Recuperação de Senha";
            ViewBag.Message = "Insira seu E-mail para que possamos Enviar um Código de recuperação";
            ViewBag.Placeholder = "E-mail";
            return View("FrmAnyService");

        }

        public IActionResult Login(LoginModel login)
        {
            LoginDAO dao = new LoginDAO();

            if (dao.VerifyUser(login) != null)
            {
                HttpContext.Session.SetString("Logged", "true");
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("Email", "E-Mail ou Senha Inválidos");
                return View("Index",login);
            }
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
                code += random.Next(0, 9).ToString();
            }
        
            return code;
        }

    }
}
