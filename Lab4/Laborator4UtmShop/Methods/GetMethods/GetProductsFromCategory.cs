using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4UtmShop.Methods.GetMethods
{
    public class GetProductsFromCategory
    {
        private static async Task<string> GetProductsByIdCategoryApi(int idCategory)
        {
            string apiUrl = $"https://localhost:44370/api/Category/categories/{idCategory}/products";

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);

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
                Console.WriteLine($"Error request {ex.Message}");
                throw;
            }
        }

        public static async Task GetProductsByIdCategory()
        {
            Console.WriteLine("Give category ID:\n");
            int categoryId = int.Parse(Console.ReadLine());
            string response = await GetProductsByIdCategoryApi(categoryId);
            Console.WriteLine(response);
        }
    }
}
