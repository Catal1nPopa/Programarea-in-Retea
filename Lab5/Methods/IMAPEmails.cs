using EmailApp.Entities;
using EmailApp.Services;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;

namespace EmailApp.Methods
{
    public class IMAPEmails
    {
        public static async Task<List<EmailParameters>> GetEmails(Credentials credentials)
        {
            string host = "imap.gmail.com";
            int port = 993;
            bool useSsl = true;

           List<EmailParameters> inboxEmails = new List<EmailParameters>();

            using (var client = new ImapClient())
            {
                await client.ConnectAsync(host, port, useSsl);
                await client.AuthenticateAsync(credentials.Email, credentials.Password);

                var inbox = client.Inbox;
                await inbox.OpenAsync(FolderAccess.ReadOnly);

                var uids = await inbox.SearchAsync(SearchQuery.All);
                List<MimeMessage> emailsList = new List<MimeMessage>();
                foreach (var uid in uids)
                {
                    var message = await inbox.GetMessageAsync(uid);
                    emailsList.Add(message);
                }

                inboxEmails = ParseEmails.parseEmails(emailsList);

                await client.DisconnectAsync(true);
            }

            return inboxEmails;
        }
    }
}
