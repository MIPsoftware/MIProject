using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using MIPChat.DAL.Domain;

namespace MIPChat.Models
{
    public class User
    {   /*[Key, Column(Order = 0)]*/
        [Key]
        public Guid UserId { get; set; }
        /*[EmailAddress]*/
        [Required(ErrorMessage ="У пользователя должен быть Email")]
        /*[Key, Column(Order = 1)]*/
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastLogIn { get; set; }
        public DateTime LastLogOut { get; set; }
        public byte[] Icon { get; set; }

        public ICollection<ChatModel> Chats;
        public User()
        {
            Chats = new List<ChatModel>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is User objAsUser)) return false;
            else return Equals(objAsUser);
        }

        public bool Equals(User other)
        {
            if (other == null) return false;
            if (this.UserId.Equals(other.UserId))
                return true;
            else if (this.Email.Equals(other.Email))
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            var hashCode = 1141116631;
            hashCode = hashCode * -1521134295 + EqualityComparer<Guid>.Default.GetHashCode(UserId);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Surname);
            hashCode = hashCode * -1521134295 + LastLogIn.GetHashCode();
            hashCode = hashCode * -1521134295 + LastLogOut.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<byte[]>.Default.GetHashCode(Icon);
            hashCode = hashCode * -1521134295 + EqualityComparer<ICollection<ChatModel>>.Default.GetHashCode(Chats);
            return hashCode;
        }
    }
}
