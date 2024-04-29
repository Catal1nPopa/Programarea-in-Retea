namespace EmailApp.Entities
{
    public class EmailParameters
    {
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Attachment { get; set; }
        public string AttachmentName { get; set; }
    }
}
