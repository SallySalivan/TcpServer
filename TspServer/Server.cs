using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TspServer
{
    class Server
    {
        static TcpListener tcpListener; 
        List<Client> clients = new List<Client>();
        static MessageProcessing MesPeoc;

        protected internal void AddConnection(Client Client)
        {
            clients.Add(Client);
        }
        protected internal void RemoveConnection(string id)
        {
            
            Client client = clients.FirstOrDefault(c => c.Id == id);
            
            if (client != null)
            {
                client.Close();
                clients.Remove(client);
            }        
        }

        protected internal void Listen()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Any, 8888);
                tcpListener.Start();
                Console.WriteLine("Сервер запущен. Ожидание подключений...");

                while (true)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();

                    Client Client = new Client(tcpClient, this);
                    Thread clientThread = new Thread(new ThreadStart(Client.Process));
                    clientThread.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }

        protected internal void AllMessage(string message, string id)
        {
            var newMessage = MesPeoc.NewMessage(message, id,clients);
            byte[] data = Encoding.Unicode.GetBytes(newMessage);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id != id) 
                {
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }

        protected internal void ServerMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].Id == id)
                {
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }

        protected internal void PrivateMessage(string message, string userName)
        {
            var client = clients.Where(x => x.UserName == userName).FirstOrDefault();

            byte[] data = Encoding.Unicode.GetBytes(message);
            client.Stream.Write(data, 0, data.Length);
        }

        protected internal void Disconnect()
        {
            tcpListener.Stop();

            for (int i = 0; i < clients.Count; i++)
            {
                clients[i].Close();
            }
            Environment.Exit(0);
        }
       
    }
}
