using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NipssDOS.Data.Model;

namespace NipssDOS.Data.Model
{
    public class Message
    {
        public Message()
        {
            Date = DateTime.UtcNow.AddHours(1);
        }

        public int Id { get; set; }
        public string Recipient { get; set; }
        public string Mail { get; set; }
        public string Title { get; set; }
        public string SentVia { get; set; }
        public string Result { get; set; }
        public DateTime Date { get; set; }
        public DateTime DateSent { get; set; }
        public int Retries { get; set; }
        public NotificationStatus NotificationStatus { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}
