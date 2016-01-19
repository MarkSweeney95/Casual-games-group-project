using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Xna.Framework;

namespace Chat
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        #region Updating Methode

        public void UpdatePosition(Vector2 newPlayerPositon)
        {
            Clients.Others.updatePosition(newPlayerPositon);
        }

        #endregion
    }

}