using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4UtmShop.Methods.PostMethods
{
    public class AddProducts
    {
        private static async Task<string> AddProductApi(int idCategory, string titleCategory, Decimal price)
        {
            string apiUrl = $"https://localhost:44370/api/Category/categories/{idCategory}/products";

            var data = new
            {
                title = titleCategory,
                price
            };
            var jsonData = JsonConvert.SerializeObject(data);

            try
            {
                using (var client = new HttpClient())
                {
                    var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);

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

        public static async void AddProduct()
        {
            try
            {
                Console.WriteLine("\nIntroduce ID-ul categoriei:\n");
                int idCategory = int.Parse(Console.ReadLine());

                Console.WriteLine("\nIntroduce numele produsului:\n");
                string title = Console.ReadLine();

                Console.WriteLine("\nIntroduce pretul produsului:\n");
                Decimal price = decimal.Parse(Console.ReadLine());

                string response = await AddProductApi(idCategory, title, price);
                Console.WriteLine(response);
            }
            catch
            {
                Console.WriteLine("Eroare la introducerea datelor\n");
            }
        }
    }
}
