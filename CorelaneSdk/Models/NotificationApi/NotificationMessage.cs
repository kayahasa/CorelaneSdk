using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorelaneSdk.Models.NotificationApi
{
    public class NotificationMessage
    {
        public NotificationMessage() { }
        public NotificationMessage(string title, string body)
        {
            Title = title;
            Body = body;
        }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
