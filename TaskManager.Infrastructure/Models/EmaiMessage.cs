using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Models
{
    public class EmailMessage
    {
        public EmailMessage()
        {
            Recipients = new List<EmailAddress>();
            Sender = new List<EmailAddress>();
        }

        public List<EmailAddress> Recipients { get; set; }
        public List<EmailAddress> Sender { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
