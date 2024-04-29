using EmailApp.Entities;
using MimeKit;

namespace EmailApp.Attachment
{
    public class CheckAttachments
    {
        public static AttachmentModel? EmailContainsAttachment(MimeMessage message)
        {
            foreach (var attachment in message.Attachments)
            {
                if (attachment is MimePart)
                {
                    var part = (MimePart)attachment;
                       var stringBase64 =  SaveAttachment.SaveAttachmentAsBase64(part);

                        return stringBase64; 
                }
            }
            return null;
        }

    }
}
