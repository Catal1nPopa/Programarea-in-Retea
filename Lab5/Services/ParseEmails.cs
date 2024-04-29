using EmailApp.Attachment;
using EmailApp.Entities;
using MimeKit;

namespace EmailApp.Services
{
    public class ParseEmails
    {
        public static List<EmailParameters> parseEmails(List<MimeMessage> emailsList)
        {
            List<EmailParameters> result = new List<EmailParameters>();
            foreach (var message in emailsList)
            {
                var stringBase64 = CheckAttachments.EmailContainsAttachment(message);
                if (stringBase64 != null)
                {
                    var emailParam = new EmailParameters()
                    {
                        FromEmail = message.From.ToString(),
                        ToEmail = message.To.ToString(),
                        ReplyTo = message.ReplyTo.ToString(),
                        Subject = message.Subject,
                        Body = message.TextBody,
                        Attachment = stringBase64.Content,
                        AttachmentName = stringBase64.ContentType
                    };
                    result.Add(emailParam);
                }
                else
                {
                    var emailParam2 = new EmailParameters()
                    {
                        FromEmail = message.From.ToString(),
                        ToEmail = message.To.ToString(),
                        ReplyTo = message.ReplyTo.ToString(),
                        Subject = message.Subject,
                        Body = message.TextBody
                    };
                    result.Add(emailParam2);
                }
            }
            return result;
        }
    }
}
