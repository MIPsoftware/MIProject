﻿
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
        User FindUserByEmail(string Email);
        User FindUserByName(string UserName);
        IEnumerable<User> FindAvailableUsersForChat(Guid UserId);
        bool ValidatePassword(LoginModel input);
        IEnumerable<User> FindAvailableUsersForLocalChat(Guid UserId);
        IEnumerable<User> GetAllUsersExcept(ICollection<Guid> guids);
    }
}