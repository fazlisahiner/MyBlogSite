using System;
using System.Collections.Generic;
using System.Linq;
//using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using MailKit.Net.Smtp;
using MimeKit;

namespace BlogSite.Web.Models
{
    public class EmailValidator
    {

        //public bool ValidateEmail(string emailAddress, MailboxAddress mailAddress)
        //{
        //    try
        //    {
        //        using (var client = new SmtpClient())
        //        {
        //            client.Connect("smtp.yourdomain.com", 587, false); // SMTP sunucu bilgileri
        //            client.Authenticate("username", "password"); // SMTP sunucu kimlik doğrulama bilgileri
        //            client.Send(new MimeMessage { From = new MailboxAddress("from@example.com"), To = { mailAddress } });
        //            client.Disconnect(true);
        //            return true; // Eğer bağlantı ve gönderim başarılı ise e-posta adresi geçerli
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // SMTP ile bağlantı hatası veya e-posta adresi geçerli değilse false dönebiliriz
        //        Console.WriteLine($"Error checking email: {ex.Message}");
        //        return false;
        //    }
        //}

        //public async Task<bool> VerifyEmailAsync(string email)
        //{
        //    try
        //    {
        //        var message = new MimeMessage();
        //        message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));
        //        message.To.Add(new MailboxAddress("", email));
        //        message.Subject = "Verify Email";

        //        using (var client = new MailKit.Net.Smtp.SmtpClient())
        //        {
        //            await client.ConnectAsync("smtp.example.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        //            await client.AuthenticateAsync("your-email@example.com", "your-email-password");

        //            // Send a test email
        //            await client.SendAsync(message);

        //            await client.DisconnectAsync(true);
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle accordingly
        //        return false;
        //    }
        //}


        public async Task<bool> VerifyEmailAsync(string email)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Your Name", "your-email@example.com"));
                message.To.Add(new MailboxAddress("", email));
                message.Subject = "Verify Email";

                message.Body = new TextPart("plain")
                {
                    Text = "This is a test email to verify the email address."
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.example.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync("your-email@example.com", "your-email-password");

                    // Send a test email
                    await client.SendAsync(message);

                    await client.DisconnectAsync(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle accordingly
                Console.WriteLine($"Email verification failed: {ex.Message}");
                return false;
            }
        }


    }
}