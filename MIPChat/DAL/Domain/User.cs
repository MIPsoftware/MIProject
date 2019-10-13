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
            return (this.UserId.Equals(other.UserId));
        }
    }
}
