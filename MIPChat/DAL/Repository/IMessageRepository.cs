using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPChat.DAL.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        ICollection<Message> GetNewMessages(Guid userId, Guid chatId);
        Dictionary<ChatModel, ICollection<Message>> GetAllNewMessages(Guid userId);
        ICollection<Message> GetAllMessagesInPeriod(Guid userId, DateTime firstDate, DateTime secondDate);
        ICollection<Message> GetNewMessagesInInterval(Guid chatId, int FirstIndex, int LastIndex);
        ICollection<Message> GetAllMessagesSince(Guid userId, DateTime date);
    }
}
