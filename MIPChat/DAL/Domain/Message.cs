using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MIPChat.DAL.Domain
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public Chat Chat { get; set; }
        [Required]
        public Guid ChatId { get; set; }
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
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(ChatId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Content);
            hashCode = hashCode * -1521134295 + TheTimeOfSending.GetHashCode();
            return hashCode;
        }
    }
}
