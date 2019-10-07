using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL.Repository
{
    public class UserRepository : BaseRepository<ChatDBContext, User>, IUsersRepository
    {
        public UserRepository(ChatDBContext context) : base(context)
        {
        }

        public Task<User> FindUserByEmail(string Email)
        {
            return _dbSet.FindAsync(Email); 
        }
        public async Task<ICollection<User>> AllExcept(ICollection<Guid> guids)
        {
            return await _dbSet
                .Where(user => !guids.Contains(user.Id))
                .ToListAsync();
        }
    }
}