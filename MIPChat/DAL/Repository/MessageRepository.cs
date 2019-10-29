using MIPChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;

namespace MIPChat.DAL.Repository
{
 public class MessageRepository:BaseRepository<Message>,IMessageRepository
 {
     public MessageRepository(ChatDBContext Context) : base(Context)
     {

     }

     public ICollection<Message> GetNewMessages(Guid userId, Guid chatId)
     {
         UserRepository userRepository = new UserRepository(context);

         User TheUser =  userRepository.FindById(userId);

         if (TheUser.LastLogIn > TheUser.LastLogOut)
             return new List<Message>();

            List<Message> messagesInChat = dbSet.Where(message => message.Chat.ChatId == chatId
            && message.TheTimeOfSending > TheUser.LastLogOut).ToList();


         if (messagesInChat.Count < 20)
             return GetNewMessagesInInterval(chatId, 20, dbSet.Where(message => message.Chat.ChatId == chatId).ToList().Count);

         return dbSet.Where(message => message.Chat.ChatId == chatId && message.TheTimeOfSending > TheUser.LastLogOut).ToList();
     }

     public  Dictionary<ChatModel, ICollection<Message>> GetAllNewMessages(Guid userId)
     {
         UserRepository userRepository = new UserRepository(context);

         User TheUser = userRepository.FindById(userId);

         Dictionary<ChatModel, ICollection<Message>> allMessages = new Dictionary<ChatModel, ICollection<Message>>();

         foreach (var chat in TheUser.Chats)
             allMessages.Add(chat, GetNewMessages(userId, chat.ChatId));

         return allMessages;
     }

     public ICollection<Message> GetAllMessagesInPeriod(Guid chatId, DateTime firstDate, DateTime secondDate)
     {
         return dbSet.
                Where(mes => mes.Chat.ChatId == chatId && 
                mes.TheTimeOfSending >= firstDate && 
                mes.TheTimeOfSending <= secondDate).ToList();
     }


     public ICollection<Message> GetNewMessagesInInterval(Guid chatId,int FirstIndex,int LastIndex)
     {
         List<Message> input = dbSet.Where(message => message.Chat.ChatId == chatId).ToList();

         List<Message> query = new List<Message>();

         for(int i = input.Count-1-FirstIndex; i < input.Count-LastIndex; i++)
         {
             query.Add(input[i]);
         }

         return query;
     }
     
     public ICollection<Message> GetAllMessagesSince(Guid chatId, DateTime date)
     {
         return GetAllMessagesInPeriod(chatId, date, DateTime.Now);
     }
 }
}
