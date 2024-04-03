using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laborator4UtmShop.Methods.GetMethods
{
    public class GetInfoCategory
    {
        ////////////By ID
        private static async Task<string> GetInfoCategoryApiID(int idCategory)
        {
            string apiUrl = $"https://localhost:44370/api/Category/categories/{idCategory}";

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

        public static async Task GetInfoCategoryById()
        {
            Console.WriteLine("Give category ID:\n");
            int categoryId = int.Parse(Console.ReadLine());
            string response = await GetInfoCategoryApiID(categoryId);
            Console.WriteLine(response);
        }

        //////////////By Name
        private static async Task<string> GetInfoCategoryApiTitle(string nameCategory)
        {
            string apiUrl = $"https://localhost:44370/api/Category/categories/search?categoryName={nameCategory}";

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

        public static async Task GetInfoCategoryApiByTitle()
        {
            Console.WriteLine("Give category title:\n");
            string categoryTitle = Console.ReadLine();
            string response = await GetInfoCategoryApiTitle(categoryTitle);
            Console.WriteLine(response);
        }
    }
}
