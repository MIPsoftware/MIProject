using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class ChatModel
    {
        [Key]
        public Guid ChatId { get; set; }
        [Required(ErrorMessage = "У чата должно быть имя")]
        public string Name { get; set; }
        public bool IsLocal { get { return (Users.Count == 2); } set {;} }
        public byte[] Icon { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Guid> UserIDs { get; set; }

        public ChatModel()
        {
            Messages = new List<Message>();
            Users = new List<User>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is ChatModel objAsChatModel)) return false;
            else return Equals(objAsChatModel);
        }

        public bool Equals(ChatModel other)
        {
            if (other == null) return false;
            return (this.ChatId.Equals(other.ChatId));
        }

        public override int GetHashCode()
        {
            var hashCode = 869073862;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(ChatId);
            hashCode = hashCode * -1521134295 + EqualityComparer<String>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(IsLocal);
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + Messages.GetHashCode();
            hashCode = hashCode * -1521134295 + Users.GetHashCode();
            return hashCode;
        }

    }
}