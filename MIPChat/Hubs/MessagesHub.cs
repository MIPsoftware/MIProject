using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MIPChat.Hubs
{
    public class MessagesHub : Hub
    {
        public async Task SelectGroup(Guid chatId)
        {
            await Groups.Add(Context.ConnectionId, chatId.ToString());
        }
        public async Task LeaveGroup(Guid chatId)
        {
            await Groups.Remove(Context.ConnectionId, chatId.ToString());
        }
        public async Task SendMessage(Guid userId, Guid chatId, string message, dynamic attachment=null)
        {
            await Clients.Group(chatId.ToString()).OnSendMessage(userId, chatId, message);
        }
        public async Task onChatUpdate(Guid chatId)
        {
            await Clients.All.OnChatUpdate(chatId);
        }

    }
}