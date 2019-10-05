using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.Models
{
    public class MessageModel
    {
        public int Id { get; set; }

        public Guid AuthorId { get; set; }

        public string Content { get; set; }

        public DateTime TheTimeOfSending { get; set; }
    }
}