using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.IO;


namespace ServerHttp
{
    class Server
    {
       
         static async Task Main(string[] args)
        {
           await  Listen();
        }
        static private async Task Listen()
        {
            HttpListener listener8000 = new HttpListener();
            listener8000.Prefixes.Add("http://localhost:8000/getKey/");
            listener8000.Start();
            //Console.WriteLine("Ожидание подключений...");


            HttpListener listener8001 = new HttpListener();
            listener8001.Prefixes.Add("http://localhost:8001/");
            listener8001.Start();


            while (true)
            {
                await ListenKey8000(listener8000);
                await ListenMessage8001(listener8001);
            }

        }
        static private async Task ListenMessage8001(HttpListener listener8001)
        {                   
            
            HttpListenerContext context = await listener8001.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            //считывывем поток post
            var originalStream = new StreamReader(context.Request.InputStream);
            string  content = originalStream.ReadToEnd();
        

            //Console.WriteLine("запрос {0}", content);
            //Console.WriteLine("запрос {0}", context.Request.HttpMethod);


            content = content.Trim('[', ']');
            content = content.Replace("\"", "");
            string[] contentSplit = content.Split(',');//[name][key][message]


            string responseString = "";
            if (ClientList.SearchNameAndKey(contentSplit[0], contentSplit[1]))
            {
                responseString = "Сообщение получено сервером";
                Console.WriteLine("{0}: {1}", contentSplit[0], contentSplit[2]);
            }
            else
            {
                responseString = "Ошибка авторизации";
            }
          

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

        }
        static private async Task ListenKey8000(HttpListener listener8000)
        {

            HttpListenerContext context = await listener8000.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            //Console.WriteLine("запрос {0}", context.Request.RawUrl);

            string userName = context.Request.RawUrl.Replace("/getKey/", "");

            string responseString = generateKey();

            ClientList.ClientListAdd(userName, responseString);

            Console.WriteLine("{0} подключился к серверу", userName);

            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

        }
        private static string generateKey()
        {
              

            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Создаем генератор случайных чисел.
            Random rand = new Random();

                // Сделайте слово.
                string word = "";
                for (int j = 1; j <= 16; j++)
                {
                    // Выбор случайного числа от 0 до 25
                    // для выбора буквы из массива букв.
                    int letter_num = rand.Next(0, letters.Length - 1);
                    Thread.Sleep(1);//контроль рандомной генерации
                    // Добавить письмо.
                    word += letters[letter_num];
                }
            return word;
        }
    }
    
}
