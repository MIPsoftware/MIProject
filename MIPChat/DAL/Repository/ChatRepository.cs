using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MIPChat.DAL.Repository;
using MIPChat.Models;

namespace MIPChat.DAL.UnitOfWork
{
  public class ChatRepository : BaseRepository<ChatModel>,IChatRepository
  { 

        public ChatRepository(ChatDBContext Context) : base(Context)
        {

        }

        public IEnumerable<ChatModel> FindAllChatsByNameQueryWithoutMessagesAndUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName));
        }

        public IEnumerable<ChatModel> FindAllChatsIncludeMessagesAndUsers()
        {
            return dbSet
                 .Include(c => c.Messages)
                 .Include(c => c.Users);
        }

        public IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeMessagesAndUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
               .Include(c => c.Messages)
               .Include(c => c.Users);
        }

        public IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeUsers(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
              .Include(c => c.Users);
        }

        public IEnumerable<ChatModel> FindAllChatsByNameQueryIncludeMessages(string ChatName)
        {
            return dbSet.Where(c => c.Name.Contains(ChatName))
               .Include(c => c.Messages);
        }

        public IEnumerable<ChatModel> FindAllChatsWithoutMessagesAndUsers()
        {
           return dbSet;
        }

        public IEnumerable<ChatModel> FindAllChatsIncludeUsers()
        {
            return dbSet
                .Include(chat => chat.Users);
        }

        public IEnumerable<ChatModel> FindAllChatsIncludeMessages()
        {
            return dbSet
                .Include(chat => chat.Messages);
        }
    }
} 