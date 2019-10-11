using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MIPChat.DAL.Repository;
using MIPChat.Models;

namespace MIPChat.DAL.UnitOfWork
{
    public class ChatRepository : BaseRepository<ChatDBContext,ChatModel>,IChatRepository
    {
        public ChatRepository():base(new ChatDBContext())
        {

        }

        public async Task<IEnumerable<ChatModel>> FindAllChatsByNameQuery(string Name)
        {
            return await _dbSet.Where(c => c.Name.Contains(Name)).ToListAsync();
        }

        public override void Insert(ChatModel entity)
        {
            IUsersRepository Users = new UserRepository();
            foreach (User user in entity.Users)
            {
                user.Chats.Add(entity);
            }

            Users.Update(entity.Users);
            base.Insert(entity);
        }

    }
}