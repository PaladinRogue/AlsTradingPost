using System.ComponentModel.DataAnnotations;

namespace Authentication.Domain.NotificationTypes
{
    public class EmailChannelTemplate : NotificationChannelTemplate
    {
        protected EmailChannelTemplate()
        {
        }

        [Required]
        public string Template { get; protected set; }

        [Required]
        public string Subject { get; protected set; }
    }
}