using MimeKit;
using System.Text;
using EmailApp.Entities;

namespace EmailApp.Attachment
{
    public class SaveAttachment
    {
        public static AttachmentModel SaveAttachmentAsBase64(MimePart attachment)
        {
                AttachmentModel attachmentModel = new AttachmentModel();
                using (var memoryStream = new MemoryStream())
                {
                    attachment.Content.DecodeTo(memoryStream);
                    attachmentModel.Content= Convert.ToBase64String(memoryStream.ToArray());
                    attachmentModel.ContentType = Path.GetExtension(attachment.FileName);
                    return attachmentModel;
                }
        }
    }
}
