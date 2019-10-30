using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class MessageModel
    {
        [Key]
        public Guid MessageId { get; set; }
        [Required]
        public User Author { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public String TheTimeOfSending { get; set; }
    }
}