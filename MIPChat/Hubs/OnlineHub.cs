using Microsoft.AspNet.SignalR;
using MIPChat.DAL.Domain;
using MIPChat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
<<<<<<< HEAD
using System.Web;
using Microsoft.AspNet.SignalR;
using MIPChat.DAL.Repository;
using MIPChat.DAL.UnitOfWork;
using MIPChat.Models;
=======
>>>>>>> ee6694864790d7dfa1223a915ef6a88741873f23

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
