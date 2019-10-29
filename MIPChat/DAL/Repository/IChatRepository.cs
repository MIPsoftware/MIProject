using MIPChat.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MIPChat.DAL.UnitOfWork
{
    public interface IChatRepository : IRepository<ChatModel>
    {
        IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeMessagesAndUsers(string ChatName);
        IEnumerable<ChatModel> FindAllChatsByNameQueryWithoutMessagesAndUsers(string ChatName);
        IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeUsers(string ChatName);
        IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeMessages(string ChatName);
        IEnumerable<ChatModel> FindAllChatsIncludeMessagesAndUsers();
        IEnumerable<ChatModel> FindAllChatsWithoutMessagesAndUsers();
        IEnumerable<ChatModel> FindAllChatsIncludeUsers();
        IEnumerable<ChatModel> FindAllChatsIncludeMessages();
   
    }
}