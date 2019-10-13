using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public DateTime TheTimeOfSending { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Message objAsMessage)) return false;
            else return Equals(objAsMessage);
        }

        public bool Equals(Message other)
        {
            if (other == null) return false;
            return (this.MessageId.Equals(other.MessageId));
        }
    }
}
