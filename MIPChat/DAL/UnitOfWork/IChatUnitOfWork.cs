using MIPChat.DAL.Repository;
using System;

namespace MIPChat.DAL.UnitOfWork
{
    interface IChatUnitOfWork : IDisposable
    {
        IUsersRepository Users { get; }

        IChatRepository Chats { get; }

        IMessageRepository Messages { get; }

        IFileRepository Files { get; }

        void CommitChanges();
    }
}
