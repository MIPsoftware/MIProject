using MIPChat.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using MIPChat.DAL.Domain;

namespace MIPChat.DAL.UnitOfWork
{
    public class ChatRepository : BaseRepository<Chat>, IChatRepository
    {

        public ChatRepository(ChatDBContext Context) : base(Context)
        {

        }

        public IEnumerable<Chat> FindAllChatsByNameQueryWithoutMessagesAndUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName));
        }

        public IEnumerable<Chat> FindAllChatsIncludeMessagesAndUsers()
        {
            return dbSet
                 .Include(c => c.Messages)
                 .Include(c => c.Users);
        }

        public IEnumerable<Chat> FindAllChatsByNameQueryIncludeMessagesAndUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
               .Include(c => c.Messages)
               .Include(c => c.Users);
        }

        public IEnumerable<Chat> FindAllChatsByNameQueryIncludeUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
              .Include(c => c.Users);
        }

        public IEnumerable<Chat> FindAllChatsByNameQueryIncludeMessages(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
               .Include(c => c.Messages);
        }

        public IEnumerable<Chat> FindAllChatsWithoutMessagesAndUsers()
        {
            return dbSet;
        }

        public IEnumerable<Chat> FindAllChatsIncludeUsers()
        {
            return dbSet
                .Include(chat => chat.Users);
        }

        public IEnumerable<Chat> FindAllChatsIncludeMessages()
        {
            return dbSet
                .Include(chat => chat.Messages);
        }
    }
}