using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MIPChat.DAL.Repository
{
    public class MessageRepository:BaseRepository<ChatDBContext,Message>,IMessageRepository
    {
        public MessageRepository():base(new ChatDBContext())
        {

        }
    }
}