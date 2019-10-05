using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.DAL.Domain
{
    public class FileModel
    {
        [Key]
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}