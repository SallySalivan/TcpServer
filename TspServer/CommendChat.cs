using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TspServer
{
    class CommendChat
    {
        private Validate Valid = new Validate();
        protected internal void ComandMessege(string message, Client client, Server server)
        {
            var search = Valid.IsAllOrPrivate(message);
            switch (search)
            {
                case "Как дела?":
                    server.ServerMessage("Хорошо, прекрасное время!", client.Id);
                    break;
                case "Что делаешь?":
                    server.ServerMessage("Общаюсь с тобой :)", client.Id);
                    break;
                case "Как настроение?":
                    server.ServerMessage("У меня прекрасное настроение!", client.Id);
                    break;
                case ".CountUser":
                    var Names = server.NamesUsers();
                    server.ServerMessage(Names, client.Id);
                    break;
                case "Пока":
                    server.ServerMessage("Прощай, заходи еще:)", client.Id);
                    server.RemoveConnection(client.Id);
                    break;
                case "/All":
                    server.AllMessage(message, client.Id);
                    break;
                case "/privat":
                    server.PrivateMessage(message, client.Id);
                    break;
                default:
                    server.ServerMessage("Такой команды нет :(", client.Id);
                    break;
            }
        }
    }
}
