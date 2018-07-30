using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.IO;

namespace gilbor.Controllers
{
    public class HomeController : Controller
    {
        SmtpClient smtp = new SmtpClient("mail.gilbor.com.ng", 2525);
        string Mail_support = "gilbornigerialtd@gmail.com"; //Domain Email for sending mail to subscriber incase of any support or help
        string MailFrom_noReply = "noreply@gilbor.com.ng"; //Domain Email for sending mail to fresh subscribers, newly register business or product
        string displayName = "Gilbor";
        public string gilbor_url = "http://gilbor.com.ng";
        string pswd = "speedy@123";
        public class contactusViewModel
        {
            public string name { get; set; }
            public string email { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
        [HttpPost]
        public async Task<JsonResult> contactus (contactusViewModel model)
        {
            try
            {
                MailAddress
      maFrom = new MailAddress(MailFrom_noReply, displayName, Encoding.UTF8),
      // maFrom = new MailAddress("info@ihealthng.somee.com", "iHealth"),
      maTo = new MailAddress(Mail_support);
                MailMessage mail = new MailMessage(maFrom.Address, maTo.Address);
                mail.Subject = model.subject;
                mail.Body = Notification_Email_Body_Creator("Admin", model.message);
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                NetworkCredential Credentials = new NetworkCredential(MailFrom_noReply, pswd);
                smtp.Credentials = Credentials; /*smtp.EnableSsl = true;*/
                await smtp.SendMailAsync(mail);
                await notifySender(model.name, model.email);
                return Json("yes", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
            
        }

        private async Task notifySender(string name, string email)
        {
            try
            {
                MailAddress
      maFrom = new MailAddress(MailFrom_noReply, displayName, Encoding.UTF8),
      // maFrom = new MailAddress("info@ihealthng.somee.com", "iHealth"),
      maTo = new MailAddress(email);
                MailMessage mail = new MailMessage(maFrom.Address, maTo.Address);
                mail.Subject = "Gilbor";
                mail.Body = Notification_Email_Body_Creator(name,"<p>This is to notify you that your email has been received, and a member of our team will get back you asap. </p>"+
                    "<p>Thank you. <br/><br/> Kind Regards <br/><br/> Gilbor Team</p>") ;
                mail.BodyEncoding = Encoding.UTF8;
                mail.SubjectEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                NetworkCredential Credentials = new NetworkCredential(MailFrom_noReply, pswd);
                smtp.Credentials = Credentials; /*smtp.EnableSsl = true;*/
                await smtp.SendMailAsync(mail);             
              
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Notification_Email_Body_Creator(string Name, string Message)
        {
            string body = string.Empty;
            string path = HostingEnvironment.MapPath("~/EmailTemplates/NotificationTemp.cshtml");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("Name", Name);
            body = body.Replace("Message", Message);
            return body;
        }
    }
}
