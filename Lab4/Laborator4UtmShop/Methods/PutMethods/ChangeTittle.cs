using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4UtmShop.Methods.PutMethods
{
    public class ChangeTittle
    {
        private static async Task<string> ChangeTitleApi(int idCategory, string newTitle)
        {
            string apiUrl = $"https://localhost:44370/api/Category/{idCategory}";

            var data = new
            {
                title = newTitle
            };
            var jsonData = JsonConvert.SerializeObject(data);

            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(apiUrl, content);

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

        public static async void ChangeTitleCategoryWithId()
        {
            try
            {
                Console.WriteLine("Introduce ID-ul categoriei:\n");
                int categoryId = int.Parse(Console.ReadLine());

                Console.WriteLine("Introduce titlu nou:\n");
                string newTitle = Console.ReadLine();

                string response = await ChangeTitleApi(categoryId, newTitle);
                Console.WriteLine(response);
            }
            catch
            {
                Console.WriteLine("Eroare la introducerea id-ului sau titlului");
            }
        }
    }
}
