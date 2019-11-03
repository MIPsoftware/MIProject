using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MIPChat.DAL.Repository
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(ChatDBContext Context) : base(Context)
        {

        }

        public Dictionary<ChatModel, ICollection<Message>> FindAllNewMessages(Guid userId)
        {
            User TheUser = context.Set<User>().FirstOrDefault(user => user.UserId == userId);

            Dictionary<ChatModel, ICollection<Message>> allMessages = new Dictionary<ChatModel, ICollection<Message>>();

            foreach (var chat in TheUser.Chats)
            {
                var newMessages = FindNewMessages(userId, chat.ChatId);
                if (newMessages.Count != 0)
                    allMessages.Add(chat, newMessages);
            }
               

            return allMessages;
        }

        public ICollection<Message> FindNewMessages(Guid userId, Guid chatId)
        {
            var users = context.Set<User>();

            User TheUser = users
                .Include(u => u.Chats)
                .FirstOrDefault(user => user.UserId == userId);

            if (TheUser.LastLogIn > TheUser.LastLogOut || TheUser.Chats.Select(c => c.ChatId).Contains(chatId))
                return new List<Message>();

            return dbSet.Where(message => message.ChatId == chatId
            && message.TheTimeOfSending > TheUser.LastLogOut).ToList();
        }

        public ICollection<Message> FindNewMessagesInInterval(Guid chatId, int FirstIndex, int LastIndex)
        {
            List<Message> input = dbSet.Where(message => message.ChatId == chatId).ToList();

            if (input.Count <= FirstIndex - LastIndex)
                FirstIndex = input.Count - 1;

            List<Message> query = new List<Message>();

            for (int i = input.Count - 1 - FirstIndex; i <= input.Count - 1 - LastIndex; i++)
            {
                query.Add(input[i]);
            }

            return query;
        }

        public ICollection<Message> FindAllMessagesInPeriod(Guid chatId, DateTime firstDate, DateTime secondDate)
        {
            return dbSet.
                   Where(mes => mes.ChatId == chatId &&
                   mes.TheTimeOfSending >= firstDate &&
                   mes.TheTimeOfSending <= secondDate).ToList();
        }

        public ICollection<Message> FindAllMessagesSince(Guid chatId, DateTime date)
        {
            return FindAllMessagesInPeriod(chatId, date, DateTime.Now);
        }

        


    }
}
