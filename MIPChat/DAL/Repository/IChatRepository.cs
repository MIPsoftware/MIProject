using MIPChat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MIPChat.DAL.UnitOfWork
{
    public interface IChatRepository : IRepository<ChatModel>
    {
        Task<IEnumerable<ChatModel>> FindAllChatsByNameQuery(string queryChatName);
    }
}