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
        Task<ICollection<Message>> GetNewMessagesAsync(Guid userId, Guid chatId);
        Task<Dictionary<ChatModel, ICollection<Message>>> GetAllNewMessagesAsync(Guid userId);
        Task<ICollection<Message>> GetAllMessagesInPeriod(Guid userId, DateTime firstDate, DateTime secondDate);
    }
}
