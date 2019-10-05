using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class ChatModel
    {
        public int Id { get; set; }
        public virtual ICollection<MessageModel> Messages { get; set; }
        public virtual ICollection<UserViewModel> Users { get; set; }
    }
}