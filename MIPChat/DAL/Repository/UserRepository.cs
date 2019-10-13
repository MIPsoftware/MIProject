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
        public async Task<ICollection<User>> GetAllUsersExceptAsync(ICollection<Guid> guids)
        {
            return await _dbSet
                .Where(user => !guids.Contains(user.UserId))
                .ToListAsync();
        }

            DateTime lastLogout = TheUser.LastLogOut;

            if (TheUser.LastLogIn > lastLogout)
                return new List<Message>();

            return await Task.Run(() => _context.Chats
                .Where(c => c.ChatId == chatId)
                .Include(c => c.Messages)
                .FirstOrDefault()
                .Messages
                .Where(m => m.TheTimeOfSending >= lastLogout)
                .ToList());   
        }

        public async Task<Dictionary<ChatModel, ICollection<Message>>> GetAllNewMessagesAsync(Guid userId)
        {
            User TheUser = await _dbSet.
                Where(user => user.UserId == userId).
                Include(user => user.Chats).
                FirstOrDefaultAsync();

            Dictionary<ChatModel,ICollection<Message>> allMessages = new Dictionary<ChatModel,ICollection<Message>>();

            foreach (var chat in TheUser.Chats)
                allMessages.Add(chat, await GetNewMessagesAsync(userId, chat.ChatId));

            return allMessages;
        }

        
    }
}