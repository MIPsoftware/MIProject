using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MIPChat.Models;

namespace MIPChat.DAL.UnitOfWork
{
    public class ChatRepository : BaseRepository<ChatDBContext,ChatModel>,IChatRepository
    {
        public ChatRepository(ChatDBContext context):base(context)
        {

        }

        public async Task<IEnumerable<ChatModel>> FindAllChatsForUser(User user)
        {
            return await _dbSet.Where(c => c.Users.Contains(user)).ToListAsync();
        }
    }
}