using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

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