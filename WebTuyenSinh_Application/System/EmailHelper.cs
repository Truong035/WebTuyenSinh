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

            mailMessage.Subject = "Thông báo từ Trường Đại Học Giao Thông Vận Tải Phân Hiệu Tại TP.HCM";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;

            SmtpClient client = new SmtpClient();
            client.Credentials = new NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
            client.Host = "smtpout.secureserver.net";
            client.Port = 80;

            try
            {
                client.Send(mailMessage);
                return "Gửi mail thành công";
            }
            catch
            {
                // log exception
                return "Gửi mail thất bại";
            }

        }


        public string NotifyMail(List<string> userEmail, string confirmationLink, string Subject)
        {
       

            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("tmooquiz40@gmail.com");
                foreach (var item in userEmail)
                {
                    mailMessage.To.Add(new MailAddress(item));

                }

                mailMessage.Subject = "Thông báo từ Trường Đại Học Giao Thông Vận Tải Phân Hiệu Tại TP.HCM";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = confirmationLink;

                SmtpClient client = new SmtpClient();
                client.Credentials = new NetworkCredential("info@rainpuddleslabradoodles.com", "Mydoodles!");
                client.Host = "smtpout.secureserver.net";
                client.Port = 80;
                client.Send(mailMessage);
                return "Gửi mail thành công";
            }
            catch
            {
                // log exception
                return "Gửi mail thất bại";
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
