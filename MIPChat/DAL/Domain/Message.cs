using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        [ForeignKey("Author")]
        [Required]
        public Guid AuthorId { get; set; }
        [ForeignKey("Chat")]
        [Required]
        public Guid ChatId { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public ChatModel Chat { get; set; }
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

        public override int GetHashCode()
        {
            var hashCode = 869073862;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(MessageId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(AuthorId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(ChatId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            hashCode = hashCode * -1521134295 + TheTimeOfSending.GetHashCode();
            return hashCode;
        }
    }
}
