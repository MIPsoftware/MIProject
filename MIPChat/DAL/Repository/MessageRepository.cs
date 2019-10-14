using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL.Repository
{
    public class MessageRepository:BaseRepository<ChatDBContext,Message>,IMessageRepository
    {
        public MessageRepository():base(new ChatDBContext())
        {

        }
        public MessageRepository(ChatDBContext Context) : base(Context)
        {

        }

        public async Task<ICollection<Message>> GetNewMessagesAsync(Guid userId, Guid chatId)
        {
            UserRepository userRepository = new UserRepository(_context);

            User TheUser = await userRepository.FindById(userId);

            if (TheUser.LastLogIn > TheUser.LastLogOut)
                return new List<Message>();

            return await _dbSet.Where(message => message.ChatId == chatId && message.TheTimeOfSending > TheUser.LastLogOut).ToListAsync();
        }

        public async Task<Dictionary<ChatModel, ICollection<Message>>> GetAllNewMessagesAsync(Guid userId)
        {
            UserRepository userRepository = new UserRepository(_context);

            User TheUser = await userRepository.FindById(userId);

            Dictionary<ChatModel, ICollection<Message>> allMessages = new Dictionary<ChatModel, ICollection<Message>>();

            foreach (var chat in TheUser.Chats)
                allMessages.Add(chat, await GetNewMessagesAsync(userId, chat.ChatId));

            return allMessages;
        }

        public async Task<ICollection<Message>> GetAllMessagesInPeriod(Guid chatId, DateTime firstDate, DateTime secondDate)
        {
            return await _dbSet.Where(mes => mes.ChatId == chatId && mes.TheTimeOfSending >= firstDate && mes.TheTimeOfSending <= secondDate).ToListAsync();
        }
    }
}