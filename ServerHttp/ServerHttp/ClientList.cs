using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerHttp
{
    static class ClientList
    {

        static ArrayList clientlist = new ArrayList() ;
        public static void ClientListAdd(string name,string key)
        {
            clientlist.Add(new Client(name, key) );

        }
        public static bool SearchNameAndKey(string name, string key)
        {

            foreach (Client client in clientlist)
            {
                if (client.UserName == name && client.UserKey == key)
                    return true;
            }
            return false;
        }


    }

    class Client
    {
        string userName;
        string userKey;

        public Client(string userName, string userKey)
        {
            this.UserName = userName;
            this.userKey = userKey;
            Console.WriteLine("[{0}] [{1}]",userName,userKey);
        }

        public string UserKey { get => userKey; set => userKey = value; }
        public string UserName { get => userName; set => userName = value; }
    }

}
