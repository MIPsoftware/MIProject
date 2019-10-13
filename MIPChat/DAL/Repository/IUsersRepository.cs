
using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL
{
    public interface IUsersRepository : IRepository<User>
    {
        Task<User> FindUserByEmail(string Email);
        Task<bool> PasswordCheck(LoginModel input);
    }
}