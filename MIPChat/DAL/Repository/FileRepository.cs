using MIPChat.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.DAL.Repository
{
    public class FileRepository : BaseRepository<File>,IFileRepository
    {
        public FileRepository(ChatDBContext context) : base(context)
        {
        }
    }
}