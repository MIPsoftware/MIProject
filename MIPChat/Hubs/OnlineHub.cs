using Microsoft.AspNet.SignalR;
using MIPChat.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIPChat.Hubs
{
    public class OnlineHub : Hub
    {
        private static List<User> Users { get; set; }

        public OnlineHub()
        {
            //Users = new UserRepository(new DAL.ChatDBContext()).FindAll().Result.ToList();
        }



        public void Connect(string userName)
        {
            var id = Context.ConnectionId;


        }

        // Отключение пользователя
        public override Task OnDisconnected(bool stopCalled)
        {
            var item = Users.FirstOrDefault(x => x.UserId.ToString() == Context.ConnectionId);
            if (item != null)
            {
                Users.Remove(item);
                var id = Context.ConnectionId;
                Clients.All.onUserDisconnected(id, item.Name);
            }

            return base.OnDisconnected(stopCalled);
        }
    }
}
