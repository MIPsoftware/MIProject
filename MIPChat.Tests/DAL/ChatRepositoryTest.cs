using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MIPChat.DAL;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
using Moq;
using System.Linq;

namespace MIPChat.Tests.DAL
{
    [TestClass]
    public class ChatRepositoryTest
    {
        [TestMethod]
        public void UpdateTest() // need to test Update from BaseRepository
        {
            //Guid id = Guid.NewGuid();
            //string changedName = "mip3";

            //var data = new List<ChatModel> {
            //    new ChatModel { ChatId = id, Icon = null, Name = "MIP1" },
            //    new ChatModel { ChatId = Guid.NewGuid(), Icon = null, Name = "MIP2" }
            //};

            //var mockSet = new Mock<DbSet<ChatModel>>();
            //var mockContext = new Mock<ChatDBContext>();
            
            //mockContext.Setup(a => a.Chats).Returns(mockSet.Object);
            //mockContext.Setup(a => a.Set<ChatModel>()).Returns(mockSet.Object);
           

            //ChatRepository chatRepo = new ChatRepository(mockContext.Object);
            //data[0].Name = changedName;
            //chatRepo.Update(data[0]);
            //Assert.AreEqual(data[0], chatRepo.FindById(id));
        }
    }
}
