namespace MIPChat.DAL.UnitOfWork
{
    internal class ChatRepository : IChatRepository
    {
        private ChatDBContext _context;

        public ChatRepository(ChatDBContext context)
        {
            _context = context;
        }
    }
}