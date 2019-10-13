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

        public UserRepository(ChatDBContext Context) : base(Context)
        {

        }

        public async Task<User> FindUserByEmail(string Email)
        {
            return await _dbSet.FindAsync(Email);
        }

        public async Task<bool> PasswordCheck(LoginModel input)
        {
            User check = await FindUserByEmail(input.Email);

            if (check.Password == input.Password)
                return true;
          
            return false;
        }

        public override async Task<IEnumerable<User>> FindAll()
        {
            return await _dbSet.Include(u => u.Chats).ToListAsync();
        }

        public override async Task<User> FindById(Guid Id)
        {
            return await _dbSet.Include(u => u.Chats).FirstOrDefaultAsync(user => user.UserId == Id);
        }

        public async Task<IEnumerable<User>> FindAvailableUsersForLocalChat(Guid UserId)
        {
            User user = FindById(UserId).Result;
            List<User> ChattedUsers = 
                user.Chats.Where(chat => chat.IsLocal)
                .SelectMany(chat => chat.Users).ToList();

            return await _dbSet.Where(u => !ChattedUsers.Contains(u)).ToListAsync();
        }
    }
}