using Microsoft.AspNet.SignalR;
using System;

namespace MIPChat.Hubs
{
    public class AlertHub : Hub
    {
        public void OnAccountCreation(Guid newUserID)
        {

        }

        public void OnErrorLoad(Guid userID)
        {

        }


    }
}