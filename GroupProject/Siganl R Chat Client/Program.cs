using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

namespace Siganl_R_Chat_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string name; // Program Class Variable
            Console.Write("Enter your Name: ");
            name = Console.ReadLine();

            IHubProxy proxy;
            HubConnection connection = new HubConnection("http://localhost:52037");
            proxy = connection.CreateHubProxy("ChatHub");



        }
    }
}
