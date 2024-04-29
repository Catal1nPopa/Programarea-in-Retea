using System.Net;
using EmailApp.Attachment;
using EmailApp.Entities;
using EmailApp.Services;
using MailKit.Net.Pop3;
using MimeKit;

namespace EmailApp.Methods
{
    public class POP3Emails
    {
        public static async Task<List<EmailParameters>> GetEmails(Credentials credentials)
        {
            List<EmailParameters> response = new List<EmailParameters>();
            string host = "pop.gmail.com";
            int port = 995;
            bool useSsl = true;

            using (var client = new Pop3Client())
            {
                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(credentials.Email, credentials.Password);

                List<MimeMessage> messages = new List<MimeMessage>();
                for (int i = 0; i < client.Count; i++)
                {
                    var message = await client.GetMessageAsync(i);
                    messages.Add(message);
                }

               response = ParseEmails.parseEmails(messages);

            }

            return response;
        }
    }
}
