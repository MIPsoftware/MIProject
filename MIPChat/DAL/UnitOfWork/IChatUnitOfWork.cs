using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MIPChat.DAL.UnitOfWork
{
    interface IChatUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }

        IChatRepository Chats { get; }

        int CommitChanges();
    }
}
