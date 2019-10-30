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
  public class UserRepository : BaseRepository<User>, IUsersRepository
  {
      public UserRepository(ChatDBContext Context) : base(Context)
      {

      }

      public User FindUserByEmail(string Email)
      {
          return  dbSet.Find(Email);
      }

      public User FindUserByName(string UserName)
      {
          return dbSet.FirstOrDefault(user => 
          user.Name.ToLower().Equals(UserName.ToLower()));
      }

        public override User FindById(Guid Userid)
        {
            return dbSet.FirstOrDefault(user =>
            user.UserId.Equals(Userid));
        }

        public bool ValidatePassword(LoginModel input)
      {
          User check =  FindUserByEmail(input.Email);

          if (check.Password == input.Password)
              return true;
        
          return false;
      }

      public IEnumerable<User> FindAvailableUsersForLocalChat(Guid UserId)
      {
          User user = FindById(UserId);
          List<User> ChattedUsers = 
              user.Chats.Where(chat => chat.IsLocal)
              .SelectMany(chat => chat.Users).ToList();

          return dbSet.Where(u => !ChattedUsers.Contains(u));
      } 
      public IEnumerable<User> FindAvailableUsersForChat(Guid UserId)
      { 
          return dbSet.Where(u => u.UserId!=UserId);
      }
      public IEnumerable<User> GetAllUsersExcept(ICollection<Guid> guids)
      {
          return dbSet
              .Where(user => !guids.Contains(user.UserId));
      }
  }
}