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

            List<Message> messagesInChat = await _dbSet.Where(message => message.ChatId == chatId
             && message.TheTimeOfSending > TheUser.LastLogOut).ToListAsync();


            if (messagesInChat.Count < 20)
                return await GetNewMessagesInIntervalAsync(chatId, 20, _dbSet.Where(message => message.ChatId == chatId).ToListAsync().Result.Count);

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

        public async Task<ICollection<Message>> GetAllMessagesInPeriodAsync(Guid chatId, DateTime firstDate, DateTime secondDate)
        {
            return await _dbSet.Where(mes => mes.ChatId == chatId && mes.TheTimeOfSending >= firstDate && mes.TheTimeOfSending <= secondDate).ToListAsync();
        }


        public async Task<ICollection<Message>> GetNewMessagesInIntervalAsync(Guid chatId,int FirstIndex,int LastIndex)
        {
            List<Message> input = await _dbSet.Where(message => message.ChatId == chatId).ToListAsync();

            List<Message> query = new List<Message>();

            for(int i = input.Count-1-FirstIndex; i < input.Count-LastIndex; i++)
            {
                query.Add(input[i]);
            }

            return query;
        }
        
        public async Task<ICollection<Message>> GetAllMessagesSinceAcync(Guid chatId, DateTime date)
        {
            return await GetAllMessagesInPeriodAsync(chatId, date, DateTime.Now);
        }
    }
}
