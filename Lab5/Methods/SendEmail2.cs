using System.Net;
using System.Net.Mail;
using System.Web.WebPages;
using EmailApp.Convertor;
using EmailApp.Entities;

namespace EmailApp.Methods
{
    public class SendEmail2
    {
        public static async Task SendEmail(Credentials credentials, SendEmail emailParameters)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(credentials.Email);
            message.To.Add(new MailAddress(emailParameters.ToEmail));
            message.ReplyToList.Add(new MailAddress(emailParameters.ReplyTo));
            message.Subject = emailParameters.Subject;
            message.Body = emailParameters.Body;
            //message.IsBodyHtml = true;

            if (!emailParameters.Attachment.IsEmpty())
            {

                ConvertFromBase64.ConvertString(emailParameters.Attachment, emailParameters.AttachmentName);

                string filePath = $"D:\\UTM\\ANUL 3\\SEM 2\\PR\\TempEmail\\{emailParameters.AttachmentName}";

                System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(filePath);
                message.Attachments.Add(attachment);
            }

            // File.Delete(filePath);
            using (var smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                try
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(credentials.Email, credentials.Password);
                    smtpClient.EnableSsl = true;

                    await smtpClient.SendMailAsync(message);
                    //DeleteTempFile.Delete(filePath);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
