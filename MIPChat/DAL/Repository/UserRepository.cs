using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL.Repository
{
    public class UserRepository : BaseRepository<ChatDBContext, User>, IUsersRepository
    {
        public UserRepository() : base(new ChatDBContext())
        {
        }

        public UserRepository(ChatDBContext context) : base(context) { }

        public async Task<User> FindUserByEmail(string Email)
        {
            return await _dbSet.FindAsync(Email);
        }

        public async Task<IEnumerable<User>> FindAvailableUsersForLocalChat(Guid UserId)
        {

            User user = FindById(UserId).Result;
            List<User> ChattedUsers = new List<User>();

            foreach (var item in user.Chats.Where(chat => chat.Users.Count == 2).Select(chat => chat.Users))
            {

                ChattedUsers.AddRange(item);
            }

            return await _dbSet.Where(u => !ChattedUsers.Contains(u)).ToListAsync();
        }
        public async Task<ICollection<User>> GetAllUsersExcept(ICollection<Guid> guids)
        {
            return await _dbSet
                .Where(user => !guids.Contains(user.UserId))
                .ToListAsync();
        }
        public async Task<ICollection<Message>> GetNewMessages(Guid userId, Guid chatId)
        {
            User TheUser = await FindById(userId);

            DateTime lastLogoutDate = TheUser.LastLogOut;
            DateTime lastLoginDate = TheUser.LastLogIn;

            if (lastLoginDate > lastLogoutDate)
                return await Task.Run( () => new List<Message>());

            return await Task.Run(()=> _context.Chats
                .Where(c => c.Id == chatId)
                .Include(c => c.Messages)
                .First()
                .Messages
                .Where(m => m.TheTimeOfSending >= lastLogoutDate)
                .ToList());
        }
    }
}