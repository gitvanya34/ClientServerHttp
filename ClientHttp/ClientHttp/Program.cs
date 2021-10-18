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
    class Client
    {
        static readonly HttpClient client = new HttpClient();
        static string userName;
        static string userKey;

        static async Task Main()
        {
            //for (int i=0;i<50;i++)
                await getKey();
                 await postMessage();
        }

       static async Task getKey()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                Console.WriteLine("Введите имя пользователя:");
                userName = /*"test2"*/ Console.ReadLine();

                HttpResponseMessage response = await client.GetAsync("http://localhost:8000/getKey/"+userName);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                // Above three lines can be replaced with new helper method below
                // string responseBody = await client.GetStringAsync(uri);
                userKey = responseBody;
                Console.WriteLine(responseBody);
                Console.WriteLine("[{0}] [{1}]", userName, userKey);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }

        static async Task postMessage()
        {
            // Call asynchronous network methods in a try/catch block to handle exceptions.
            try
            {
                //Console.WriteLine("Введите имя пользователя:");
                //userName = Console.ReadLine();

                //HttpResponseMessage response = await client.GetAsync("http://localhost:8001/getKey/" + userName);
                //response.EnsureSuccessStatusCode();
                //string responseBody = await response.Content.ReadAsStringAsync();
                //// Above three lines can be replaced with new helper method below
                //// string responseBody = await client.GetStringAsync(uri);
                //userKey = responseBody;
                //Console.WriteLine(responseBody);
                //Console.WriteLine("[{0}] [{1}]", userName, userKey);

                string message = "hello word";
                string[] cont = { userName, userKey , message};


                string json = JsonSerializer.Serialize(cont);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var url = "http://localhost:8001/";
                using var client = new HttpClient();

                var response = await client.PostAsync(url, data);

                string result = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(result);

                //record Person(string Name, string Occupation);

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }
    
}

