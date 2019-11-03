using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIPChat.DAL;
using MIPChat.DAL.Domain;
using MIPChat.DAL.Repository;
using MIPChat.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MIPChat.UnitTests
{
    [TestClass]
    public class MessagesRepositoryUnitTest
    {

        private List<Message> messages { get; set; }

        private List<User> users { get; set; }

        private List<Chat> chats { get; set; }

        [TestInitialize]
        public void Init()
        {
            users = new List<User>
            {
                new User() { Email = "111@yahoo.com", Name = "A", UserId = Guid.NewGuid()},
                new User() { Email = "222@yahoo.com", Name = "B", UserId = Guid.NewGuid()},
                new User() { Email = "333@yahoo.com", Name = "C", UserId = Guid.NewGuid()}
            };

            chats = new List<Chat>
            {
                new Chat
                {
                    ChatId = Guid.NewGuid(),
                    Icon = new byte[1],
                    IsLocal = true,
                    Messages = new List<Message>(),
                    Name = "Local chat",
                    Users = new List<User> { users[0], users[1] }
                },
                new Chat
                {
                    ChatId = Guid.NewGuid(),
                    Icon = new byte[1],
                    IsLocal = false,
                    Messages = new List<Message>(),
                    Name = "Local chat",
                    Users = new List<User> { users[0], users[1] }
                },
            };

        }

        public Mock<DbSet<Message>> GetMockMessagesDbSet()
        {
            var mockSet = new Mock<DbSet<Message>>();
            mockSet.As<IQueryable<Message>>().Setup(message => message.Provider).Returns(messages.AsQueryable().Provider);
            mockSet.As<IQueryable<Message>>().Setup(message => message.Expression).Returns(messages.AsQueryable().Expression);
            mockSet.As<IQueryable<Message>>().Setup(message => message.ElementType).Returns(messages.AsQueryable().ElementType);
            mockSet.As<IQueryable<Message>>().Setup(message => message.GetEnumerator()).Returns(messages.AsQueryable().GetEnumerator());
            mockSet.Setup(message => message.Find(It.IsAny<object[]>())).Returns<object[]>((ids) => messages.FirstOrDefault(message => message.MessageId == (Guid)ids[0]));

            return mockSet;
        }


        private Mock<DbSet<User>> GetMockUsersDbSet()
        {
            var mockSet = new Mock<DbSet<User>>();
            mockSet.As<IQueryable<User>>().Setup(m => m.Provider).Returns(users.AsQueryable().Provider);
            mockSet.As<IQueryable<User>>().Setup(m => m.Expression).Returns(users.AsQueryable().Expression);
            mockSet.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(users.AsQueryable().ElementType);
            mockSet.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(users.AsQueryable().GetEnumerator());
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns<object[]>((ids) => users.FirstOrDefault(u => u.UserId == (Guid)ids[0]));

            return mockSet;
        }

        [TestMethod]
        public void FindAllMessagesInPeriod_IsNotEmpty()
        {
            DateTime from = DateTime.Parse("07/02/2019");
            DateTime to = DateTime.Now;

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},

            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository repository = new MessageRepository(mockContext.Object);

            var result = repository.FindAllMessagesInPeriod(chats[0].ChatId, from, to);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void FindAllMessagesInPeriod_IsEmpty()
        {
            DateTime from = DateTime.Parse("07/02/2019");
            DateTime to = DateTime.Now;

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("05/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("05/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("05/02/2019")},

            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository repository = new MessageRepository(mockContext.Object);

            var result = repository.FindAllMessagesInPeriod(chats[0].ChatId, from, to);

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void FindAllMessagesSince_IsNotEmpty()
        {
            DateTime from = DateTime.Parse("07/02/2019");

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},

            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository repository = new MessageRepository(mockContext.Object);

            var result = repository.FindAllMessagesSince(chats[0].ChatId, from);

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void FindAllMessagesSince_IsEmpty()
        {
            DateTime from = DateTime.Parse("10/02/2019");


            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},

            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository repository = new MessageRepository(mockContext.Object);

            var result = repository.FindAllMessagesSince(chats[0].ChatId, from);

            Assert.AreEqual(0, result.Count);
        }


        [TestMethod]
        public void FindNewMessagesInInterval_returnLessThanRequired()
        {
            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")}
            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);

            var result = messageRepository.FindNewMessagesInInterval(chats[0].ChatId, 20, 1);
            Assert.AreEqual(messages.Count - 1, result.Count);
        }

        [TestMethod]
        public void FindNewMessagesInInterval_returnJustRequired()
        {
            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")}
            };

            chats[0].Messages = messages;

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(a => a.Set<Message>()).Returns(messageSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);

            var result = messageRepository.FindNewMessagesInInterval(chats[0].ChatId, messages.Count - 1, 0);
            Assert.AreEqual(messages.Count, result.Count);
        }

        [TestMethod]
        public void FindNewMessages_isNotEmpty()
        {
            User user = new User {
                Email = "111@yahoo.com",
                Name = "A",
                UserId = Guid.NewGuid(),
                LastLogIn = DateTime.Parse("06/02/2019"),
                LastLogOut = DateTime.Parse("06/03/2019")
            };

            users = new List<User>
            {
                user
            };

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/04/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/04/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/04/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/01/2019")}
            };

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();
            Mock<DbSet<User>> usersSet = GetMockUsersDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(context => context.Set<Message>()).Returns(messageSet.Object);
            mockChatContext.Setup(context => context.Set<User>()).Returns(usersSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);
           
            var result = messageRepository.FindNewMessages(user.UserId, chats[0].ChatId);
            Assert.AreEqual(3, result.Count);  
        }
        
        [TestMethod]
        public void FindNewMessages_isEmpty()
        {
            User user = new User {
                Email = "111@yahoo.com",
                Name = "A",
                UserId = Guid.NewGuid(),
                LastLogIn = DateTime.Parse("11/02/2019"),
                LastLogOut = DateTime.Parse("11/03/2019")
            };

            users = new List<User>
            {
                user
            };

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")}
            };

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();
            Mock<DbSet<User>> usersSet = GetMockUsersDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(context => context.Set<Message>()).Returns(messageSet.Object);
            mockChatContext.Setup(context => context.Set<User>()).Returns(usersSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);
           
            var result = messageRepository.FindNewMessages(user.UserId, chats[0].ChatId);
            Assert.AreEqual(0, result.Count);  
        }

        [TestMethod]
        public void FindAllNewMessages_IsNotEmpty()
        {
            User user = new User
            {
                Email = "111@yahoo.com",
                Name = "A",
                UserId = Guid.NewGuid(),
                LastLogIn = DateTime.Parse("06/02/2019"),
                LastLogOut = DateTime.Parse("06/03/2019"),
                Chats = chats
            };

            users = new List<User>
            {
                user
            };

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("05/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("08/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("09/02/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("06/02/2019")}
            };

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();
            Mock<DbSet<User>> usersSet = GetMockUsersDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(context => context.Set<Message>()).Returns(messageSet.Object);
            mockChatContext.Setup(context => context.Set<User>()).Returns(usersSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);

            var result = messageRepository.FindAllNewMessages(user.UserId);
            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        public void FindAllNewMessages_IsEmpty()
        {
            User user = new User
            {
                Email = "111@yahoo.com",
                Name = "A",
                UserId = Guid.NewGuid(),
                LastLogIn = DateTime.Parse("07/02/2019"),
                LastLogOut = DateTime.Parse("08/03/2019"),
                Chats = new List<Chat> { chats[0] }
            };

            users = new List<User>
            {
                user
            };

            messages = new List<Message>
            {
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("02/01/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("02/01/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId = chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("02/01/2019")},
                new Message{MessageId = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    Content = "1231",
                    ChatId =chats[0].ChatId,
                TheTimeOfSending = DateTime.Parse("02/01/2019")}
            };

            Mock<DbSet<Message>> messageSet = GetMockMessagesDbSet();
            Mock<DbSet<User>> usersSet = GetMockUsersDbSet();

            Mock<ChatDBContext> mockChatContext = new Mock<ChatDBContext>();
            mockChatContext.Setup(context => context.Set<Message>()).Returns(messageSet.Object);
            mockChatContext.Setup(context => context.Set<User>()).Returns(usersSet.Object);

            MessageRepository messageRepository = new MessageRepository(mockChatContext.Object);

            var result = messageRepository.FindAllNewMessages(user.UserId);
            Assert.AreEqual(0, result.Count);
        }
    }
}
