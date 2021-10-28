using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http;
using System;
using System.Text;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClientHttp
{
    public static class Client
    {
        static readonly HttpClient client = new HttpClient();
        static string userName;
        static string userKey;
        static string userResponse;
        public static string UserName { get => userName; set => userName = value; }
        public static string UserKey { get => userKey; set => userKey = value; }
        public static string UserResponse { get => userResponse; set => userResponse = value; }

        static async Task Main()
        {
            for (int i = 0; i < 10; i++)
            {
                await getKey(generateKey());
                await postMessage(userName, userKey, "TestMessage");
            }
            Console.ReadKey();
        }
        private static string generateKey()
        {


            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

            // Создаем генератор случайных чисел.
            Random rand = new Random();

            // Сделайте слово.
            string word = "";
            for (int j = 1; j <= 4; j++)
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
        public static async Task getKey(string name)
        {
          
            try
            {
                Console.WriteLine(name+": Запрос на получение ключа");
                userName = name /*"test2"*//* Console.ReadLine()*/;

                HttpResponseMessage response = await client.GetAsync("http://localhost:8000/getKey/"+userName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                

                userKey = responseBody;
                userResponse = DateTime.Now.ToString("HH:mm:ss")+": Получен ключ [" + userKey +"] для пользователя " + userName + ".";
                Console.WriteLine(responseBody);
                Console.WriteLine("[{0}] [{1}]", userName, userKey);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
           
        }

        public static async Task postMessage(string name,string key, string message)
        {
          
            try
            {
               
                string[] cont = { name, key, message };


                string json = JsonSerializer.Serialize(cont);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:8001/";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);
                
                string result = response.Content.ReadAsStringAsync().Result;
                userResponse = DateTime.Now.ToString("HH:mm:ss") +": "+ result;
                Console.WriteLine(result);



            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
    
}

