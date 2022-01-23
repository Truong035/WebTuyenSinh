using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebTuyenSinh_Application.System
{
  public class EmailHelper
    {
        public  string SendEmail(string userEmail, string confirmationLink, string Subject)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("tmooquiz40@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));
            mailMessage.Subject = Subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            try
            {
                using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
                {
                    client.Port = 587;
                    // Tạo xác thực bằng địa chỉ gmail và password
                    client.Credentials = new NetworkCredential("tmooquiz40@gmail.com", "0353573467");
                    client.EnableSsl = true;
                   client.Send(mailMessage);

                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
                // log exception
            }
          

        }


        public bool SendEmailPasswordReset(string userEmail, string link)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("tmooquiz40@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Password Reset";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = link;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch
            {
                // log exception
            }
            return false;
        }

        public bool SendEmailTwoFactorCode(string userEmail, string code)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("tmooquiz40@gmail.com");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Two Factor Code";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = code;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return true;
            }
            catch
            {
                // log exception
            }
            return false;
        }
    }
}
