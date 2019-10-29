using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIPChat.DAL;
using MIPChat.DAL.Repository;
using MIPChat.Models;
using Moq;

namespace MIPChat.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        public List<User> users { get; set; }

        [TestInitialize]
        public void Init()
        {
            users = new List<User>
            {
                new User() { Email = "111@yahoo.com", Name = "A", UserId = Guid.NewGuid()},
                new User() { Email = "222@yahoo.com", Name = "B", UserId = Guid.NewGuid()},
                new User() { Email = "333@yahoo.com", Name = "C", UserId = Guid.NewGuid()}
            };
        }

        public Mock<DbSet<User>> GetMockDbSet()
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
        public void FindByNameTest_NotEmptyResult()
        {
            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUser = repository.FindUserByName(users[0].Name);

            Assert.AreEqual(users[0], foundUser);
        }

        [TestMethod]
        public void FindByNameTest_EmptyResult()
        {
            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUser = repository.FindUserByName("E");

            Assert.IsNull(foundUser);
        }

        [TestMethod]
        public void GetAllUsersExceptTest_NotEmptyResult()
        {
            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUsers = repository.GetAllUsersExcept(new List<Guid>() { users[0].UserId, users[1].UserId });
            Assert.IsTrue(foundUsers.Count() == 1);
            var concUser = foundUsers.First();
            Assert.AreEqual(users[2], concUser);
        }

        [TestMethod]
        public void GetAllUsersExceptTest_EmptyResult()
        {
            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUsers = repository.GetAllUsersExcept(new List<Guid>() { users[0].UserId, users[1].UserId, users[2].UserId });

            Assert.IsTrue(foundUsers.Count() == 0);
        }

        [TestMethod]
        public void FindAvailableUsersForLocalChatTest_NotEmptyResult()
        {
            ChatModel chat = new ChatModel() { ChatId = Guid.NewGuid(), IsLocal = true, Users = new List<User> { users[0], users[1] } };
            users[0].Chats = new List<ChatModel> { chat };
            users[1].Chats = new List<ChatModel> { chat };

            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUsers = repository.FindAvailableUsersForLocalChat(users[0].UserId);

            var count = foundUsers.Count();

            Assert.IsTrue(foundUsers.Count() == 1);
            User foundUser = foundUsers.First();
            User expectedUser = users[2];
            Assert.AreEqual(expectedUser, foundUser);


        }

        [TestMethod]
        public void FindAvailableUsersForLocalChatTest_EmptyResult()
        {
            ChatModel chat0 = new ChatModel() { ChatId = Guid.NewGuid(), IsLocal = true, Users = new List<User> { users[0], users[1] } };
            ChatModel chat1 = new ChatModel() { ChatId = Guid.NewGuid(), IsLocal = true, Users = new List<User> { users[0], users[2] } };
            users[0].Chats = new List<ChatModel> { chat0, chat1 };
            users[1].Chats = new List<ChatModel> { chat0 };
            users[2].Chats = new List<ChatModel> { chat1 };

            var mockSet = GetMockDbSet();

            var mockContext = new Mock<ChatDBContext>();
            mockContext.Setup(a => a.Set<User>()).Returns(mockSet.Object);

            UserRepository repository = new UserRepository(mockContext.Object);

            var foundUsers = repository.FindAvailableUsersForLocalChat(users[0].UserId);

            var count = foundUsers.Count();
            Assert.IsTrue(count == 0);
        }


    }
}
