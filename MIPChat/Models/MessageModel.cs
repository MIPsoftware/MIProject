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
        public int Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Content { get; set; }
      
        public DateTime TheTimeOfSending { get; set; }
    }
}