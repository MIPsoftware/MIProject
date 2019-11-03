using System;
using System.ComponentModel.DataAnnotations;

namespace MIPChat.Models
{
    public class MessageModel
    {
        [Key]
        public Guid MessageId { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public String TheTimeOfSending { get; set; }
    }
}