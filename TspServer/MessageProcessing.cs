using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TspServer
{
    class MessageProcessing
    {
        public string AllMessage(string message, string id, List<Client> clients)
        {
            var client = clients.Where(x => x.Id == id).FirstOrDefault();
            string result = message.Replace("/All", client.UserName + ":");
            return result;
        }

        public string PrivateUserMessega(string message)
        {
            var split = message.Split(' ');
            return split[1];
        }

        public string PrivateNewMessage(string message , string id, List<Client> clients)
        {
            var user = PrivateUserMessega(message);
            var client = clients.Where(x => x.Id == id).FirstOrDefault();
            string result = message.Replace("/privat","ЛС");
            result = result.Replace(user, client.UserName);
            return result;
        }
    }
}
