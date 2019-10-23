using System;
using System.Collections.Generic;
using System.Text;

namespace TspServer
{
    class Validate
    {
        public  string IsAllOrPrivate(string messege)
        {
            var result = messege.Split(' ');
            if (result[0] == "/All" || result[0] == "/privat")
                return result[0];

            return messege;
        }
    }
}
