using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4UtmShop.Methods.PostMethods
{
    public class AddCategory
    {
        private static async Task<string> AddCategoryApi(string title)
        {
            string apiUrl = "https://localhost:44370/api/Category/categories";

            var data = new
            {
                title
            };
            var jsonData = JsonConvert.SerializeObject(data);

            try
            {
                using ( var client = new HttpClient() )
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl,content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        return responseContent;
                    }
                    return $"error: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error {ex.Message}");
                throw;
            }
        }

        public static async void addCategory()
        {
            try
            {
                Console.WriteLine("\nIntroduce titlu categoriei:\n");
                string title = Console.ReadLine();
                string response = await AddCategoryApi(title);
                Console.WriteLine(response);
            }
            catch
            {
                Console.WriteLine("Eroare la introducerea titlului");
            }
        }
    }
}
