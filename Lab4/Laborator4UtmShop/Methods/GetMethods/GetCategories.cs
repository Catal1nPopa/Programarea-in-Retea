using Laborator4UtmShop.Entities;
using Newtonsoft.Json;

namespace Laborator4UtmShop.Methods.GetMethods
{
    public class GetCategories
    {
        private static async Task<List<Category>> GetCategoriesFromApi()
        {
            string apiUrl = "https://localhost:44370/api/Category/categories";
            List<Category> categories = new List<Category>();
            try
            {
                using (var client = new HttpClient())
                {
                        var response = await client.GetAsync(apiUrl);
                        if (response.IsSuccessStatusCode)
                        {
                            string responseBody = await response.Content.ReadAsStringAsync();
                            dynamic data = JsonConvert.DeserializeObject(responseBody);

                            foreach (var item in data)
                            {
                                long id = (long)item["id"];
                                string name = (string)item["name"];
                                int itemsCount = (int)item["itemsCount"];
                                var category = new Category
                                {
                                    Id = id,
                                    Title = name,
                                    Products = itemsCount
                                };
                                categories.Add(category);
                            }
                        }
                        return categories;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<Category>();
            }
        }

        public static async Task getCategory()
        {
            try
            {
                List<Category> categories = await GetCategoriesFromApi();
                foreach (var category in categories)
                {
                    Console.WriteLine($"Category ID: {category.Id}, Name: {category.Title}, Products Count: {category.Products}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting categories: {ex.Message}");
            }
        }
    }
}
