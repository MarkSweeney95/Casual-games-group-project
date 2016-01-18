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

            private static void Connection_Received(string obj)
        {
            Console.WriteLine("Message Recieved {0}", obj);
        }

        private static void recieved_a_message(string sender, string message)
        {
            Console.WriteLine("{0} : {1}", sender, message);
        }

        Action<string, string> SendMessageRecieved = recieved_a_message;



    }
}

