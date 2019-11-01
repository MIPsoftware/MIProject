using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MIPChat.Hubs
{
    public class MessagesHub : Hub
    {
        public void SendUserMessage(Guid userId, string messageValue)
        {
            Clients.User(userId.ToString()).addMessage(messageValue);
        }

        public void SendChatMessage(Guid chatID, string messageValue)
        {
            Clients.Group(chatID.ToString()).addMessage(messageValue);
        }

        public void onChatUpdate(Guid chatId)
        {
            Clients.All.OnChatUpdate(chatId);
        }

    }
}