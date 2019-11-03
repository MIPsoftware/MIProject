using MIPChat.DAL.Domain;
using MIPChat.Models;
using System.Collections.Generic;

namespace MIPChat.DAL.UnitOfWork
{
    public interface IChatRepository : IRepository<Chat>
    {
        IEnumerable<Chat> FindAllChatsByNameQueryIncludeMessagesAndUsers(string ChatName);
        IEnumerable<Chat> FindAllChatsByNameQueryWithoutMessagesAndUsers(string ChatName);
        IEnumerable<Chat> FindAllChatsByNameQueryIncludeUsers(string ChatName);
        IEnumerable<Chat> FindAllChatsByNameQueryIncludeMessages(string ChatName);
        IEnumerable<Chat> FindAllChatsIncludeMessagesAndUsers();
        IEnumerable<Chat> FindAllChatsWithoutMessagesAndUsers();
        IEnumerable<Chat> FindAllChatsIncludeUsers();
        IEnumerable<Chat> FindAllChatsIncludeMessages();

    }
}