using MIPChat.DAL.Domain;
using MIPChat.Models;
using System;
using System.Collections.Generic;

namespace MIPChat.DAL.Repository
{
    public interface IMessageRepository : IRepository<Message>
    {
        ICollection<Message> FindNewMessages(Guid userId, Guid chatId);
        Dictionary<Chat, ICollection<Message>> FindAllNewMessages(Guid userId);
        ICollection<Message> FindAllMessagesInPeriod(Guid userId, DateTime firstDate, DateTime secondDate);
        ICollection<Message> FindNewMessagesInInterval(Guid chatId, int FirstIndex, int LastIndex);
        ICollection<Message> FindAllMessagesSince(Guid userId, DateTime date);
    }
}
