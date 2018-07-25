using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService_eTracking
{
    class sent_email
    {
        public void CallMail(string email, string touser, string subject, string Detail)
        {
            MailMessage Mail = new MailMessage();

            Mail.To.Add(new MailAddress(email));
            //Mail.[Bcc].Add(New MailAddress("pichayanee.rojrattanapon@daimler.com"))
            //   Mail.Bcc.Add(new MailAddress("ajn_149@hotmail.com"));

            Mail.From = new MailAddress("adevsolutionsplus@gmail.com"); // MailServer
            Mail.Subject = "[e-Tracking] " + subject;

            //'*** HTML Tag ***// 

            StringBuilder BD = new StringBuilder();
            BD.Append("<body bgcolor='#F8F7F1'>");
            BD.Append("<div style='font-size:12px; width:550px; line-height:20px; " +
                      "font-family:Tahoma, Geneva, sans-serif; border: 1px dashed #e1e1e1;background-color: #FFFFFF;padding: 0 20px;'>");
            BD.Append("<p>Dear " + touser + "</p>");
            BD.Append("<br/>");
            BD.Append("<p>" + subject + "</p>");
            BD.Append("<p>" + Detail + "</p>");
            BD.Append("<br/>");
            //BD.Append(@"<p>Please Check : http://103.40.142.138:8088/ </p>");
            BD.Append("<br/>");
            BD.Append("<p>Best Regards,</p>");
            BD.Append("<br/>");
            BD.Append("<p>e-Tracking</p>");

            BD.Append("</div>");
            BD.Append("</body>");

            Mail.Body = BD.ToString();

            try
            {
                Mail.IsBodyHtml = true;
                SmtpClient client = new SmtpClient();
                client.UseDefaultCredentials = true;
                client.Host = "smtp.gmail.com";
                client.Port = 587;
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new NetworkCredential("adevsolutionsplus@gmail.com", "Admin2016");
                client.Timeout = 20000;

                client.Send(Mail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
