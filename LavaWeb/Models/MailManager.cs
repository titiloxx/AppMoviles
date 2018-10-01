using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace LavaWeb.Models
{
   

    public class MailManager
    {
        SmtpClient client = null;

        public MailManager()
        {
   
        }

        public void MandarEmail(string emailDestino, string asunto, string mensaje)
        {
            client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("lavacheats@gmail.com", "appsmoviles2018");
            MailMessage mm = new MailMessage("lavacheats@gmail.com", emailDestino, asunto, mensaje);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            client.Send(mm);
        }
    }
}