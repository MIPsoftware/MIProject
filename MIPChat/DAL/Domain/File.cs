using System;
using System.ComponentModel.DataAnnotations;

namespace MIPChat.DAL.Domain
{
    public class File
    {
        [Key]
        public Guid FileId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}