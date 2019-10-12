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

        public async Task<User> FindUserByEmail(string Email)
        {
           return await _dbSet.FindAsync(Email); 
        }

        public override async Task<IEnumerable<User>> FindAll()
        {
            return await _dbSet.Include(u => u.Chats).ToListAsync();
        }

        public override async Task<User> FindById(Guid Id)
        {
            return await _dbSet.FindAsync(Id);
        }

        public async Task<IEnumerable<User>> FindAvailableUsersForLocalChat(Guid UserId)
        {
            User user = FindById(UserId).Result;
            List<User> ChattedUsers = new List<User>();
           
            foreach(var item in user.Chats.Where(chat => chat.Users.Count == 2).Select(chat => chat.Users))
            {
               
                ChattedUsers.AddRange(item);
            }

            return await _dbSet.Where(u => !ChattedUsers.Contains(u)).ToListAsync();
        }
    }
}