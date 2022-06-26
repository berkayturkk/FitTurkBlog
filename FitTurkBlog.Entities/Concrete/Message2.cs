using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitTurkBlog.Entities.Concrete
{
    public class Message2
    {
        [Key]
        public int MessageID { get; set; }
        public int? MessageSenderID { get; set; }
        public int? MessageReceiverID { get; set; }
        public string MessageSubject { get; set; }
        public string MessageDetails { get; set; }
        public DateTime MessageDate { get; set; }
        public bool MessageStatus { get; set; }
        public Writer MessageSenderUser { get; set; }
        public Writer MessageReceiverUser { get; set; }

    }
}
