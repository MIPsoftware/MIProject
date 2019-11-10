using Microsoft.AspNet.SignalR;
using MIPChat.DAL.Domain;
using MIPChat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using MIPChat.DAL.Repository;
using MIPChat.DAL.UnitOfWork;
using System;

namespace MIPChat.Hubs
{
    public class OnlineHub : Hub
    {
        private ChatUnitOfWork messangerData;

        public OnlineHub()
        {
            messangerData = new ChatUnitOfWork();
        }
        public async Task Connect(Guid userId)
        {
            await Groups.Add(Context.ConnectionId, "online");
        }

        // Отключение пользователя
        public override Task OnDisconnected(bool stopCalled)
        {
            
            return base.OnDisconnected(stopCalled);
        }
    }
}
