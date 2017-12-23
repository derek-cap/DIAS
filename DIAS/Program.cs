using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DIAS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://10.10.22.119:8081");
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = client.GetAsync("api/todo").Result;
                if (response.IsSuccessStatusCode)
                {
                    // Parse the response body. 
                    var items = response.Content.ReadAsAsync<IEnumerable<TodoItem>>().Result;
                    foreach (var item in items)
                    {
                        Console.WriteLine(item);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
