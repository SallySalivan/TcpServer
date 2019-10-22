using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TspServer
{
    class MessageProcessing
    {
        public string NamesUsers(List<Client> clients)
        {
            List<string> Users = new List<string>();
            foreach (var item in clients)
            {
                Users.Add(item.UserName);
            }
            string NamesUsers = Users.Aggregate((x, y) => x + "," + y);
            return NamesUsers;
        }
        public string NewMessage(string message, string id, List<Client> clients)
        {
            var client = clients.Where(x => x.Id == id).FirstOrDefault();
            string result = message.Replace("/All", client.UserName + ":");
            return result;
        }
    }
}
