using discounter.Models.JsonResults;
using MvcApplication3.Models;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;

namespace MvcApplication3.Controllers
{
    public class HomeController : ControllerWrapper
    {
        private DataModel dataModel;

        protected DataModel Dm { get { return dataModel ?? (dataModel = new DataModel()); } }

        public ActionResult Index()
        {
            //ViewData["CaptсhaGuid"] = NewCaptcha();

            ViewData["PostSpoiler4"] = Dm.GetItem("262");

            return View();
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Feedback(FormCollection collection)
        {

            JsonMessage jm = new JsonMessage();

            try
            {
                string system = collection["system"];
                string name = collection["name"];
                string phone = collection["phone"];
                string message = collection["message"];

                //MailMessage oMsg = new MailMessage();
                //oMsg.From = new MailAddress(ConfigurationManager.AppSettings["messageFrom"]);
                //oMsg.To.Add(new MailAddress(ConfigurationManager.AppSettings["messageTo"]));
                //oMsg.Subject = "Запись на замер";
                //oMsg.Body = "Система: " + system + "\n";
                //oMsg.Body += "Имя: " + name + "\n";
                //oMsg.Body += "Телефон: " + phone + "\n";
                //if (message != null)
                //{
                //    oMsg.Body += "Сообщение: " + message + "\n";
                //}
                //SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTP"]);
                //smtp.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["SMTP_login"], ConfigurationManager.AppSettings["SMTP_password"]);
                //smtp.Send(oMsg);

                System.Web.Mail.MailMessage mail = new System.Web.Mail.MailMessage();

                string SMTP_SERVER    	= "http://schemas.microsoft.com/cdo/configuration/smtpserver";
                string SMTP_SERVER_PORT = "http://schemas.microsoft.com/cdo/configuration/smtpserverport";
                string SEND_USING 		= "http://schemas.microsoft.com/cdo/configuration/sendusing";
                string SMTP_USE_SSL   	= "http://schemas.microsoft.com/cdo/configuration/smtpusessl";
                string SMTP_AUTHENTICATE= "http://schemas.microsoft.com/cdo/configuration/smtpauthenticate";
                string SEND_USERNAME  	= "http://schemas.microsoft.com/cdo/configuration/sendusername";
                string SEND_PASSWORD  	= "http://schemas.microsoft.com/cdo/configuration/sendpassword";

                mail.Fields[SMTP_SERVER] = ConfigurationManager.AppSettings["SMTP"];
                mail.Fields[SMTP_SERVER_PORT] = 465;
                mail.Fields[SEND_USING] = 2;
                mail.Fields[SMTP_USE_SSL] = true;
                mail.Fields[SMTP_AUTHENTICATE] = 1;
                mail.Fields[SEND_USERNAME] = ConfigurationManager.AppSettings["SMTP_login"];
                mail.Fields[SEND_PASSWORD] = ConfigurationManager.AppSettings["SMTP_password"];

                mail.From = ConfigurationManager.AppSettings["messageFrom"];
                mail.To = ConfigurationManager.AppSettings["messageTo"];
                mail.Subject = "Запись на замер c сайта demyr";
                mail.BodyFormat = System.Web.Mail.MailFormat.Text;
                mail.Body = "Система: " + system + "\n";
                mail.Body += "Имя: " + name + "\n";
                mail.Body += "Телефон: " + phone + "\n";
                if (message != null)
                {
                    mail.Body += "Сообщение: " + message + "\n";
                }

                System.Web.Mail.SmtpMail.SmtpServer = ConfigurationManager.AppSettings["SMTP"] + ":465";
                System.Web.Mail.SmtpMail.Send(mail);

                jm.Result = true;
                jm.Message = "Мы получили Ваш запрос и скоро свяжемся с Вами...";
            }
            catch (Exception e)
            {
                jm.Result = true;
                jm.Message = "Во время отправки произошла ошибка - " + e.ToString();
            }

            
                

            return Json(jm);
        }
    }
}
