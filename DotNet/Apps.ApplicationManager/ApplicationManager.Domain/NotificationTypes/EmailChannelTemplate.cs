using System;
using System.ComponentModel.DataAnnotations;

namespace ApplicationManager.Domain.NotificationTypes
{
    public class EmailChannelTemplate : NotificationChannelTemplate
    {
        protected EmailChannelTemplate()
        {
        }
        
        public override string Type
        {
            get => ChannelTemplateTypes.Email;
            protected set => throw new NotSupportedException();
        }

        [Required]
        public string Template { get; set; }

        [Required]
        public string Subject { get; set; }
    }
}