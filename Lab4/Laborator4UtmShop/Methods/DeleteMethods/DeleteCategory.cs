namespace Laborator4UtmShop.Methods.DeleteMethods
{
    public class DeleteCategory
    {
        private static async Task<string> DeleteCategoryApi(int categoryId)
        {
            string apiUrl = $"https://localhost:44370/api/Category/categories/{categoryId}";

            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return "Categoria a fost stearsa cu succes.";
                    }
                    return $"Error: {response.StatusCode}";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Eroare la crearea cererii Http: {ex.Message}");
                throw;
            }
        }

        public static async void DeleteCategoryWithId()
        {
            try
            {
                Console.WriteLine("Introduce ID-ul categoriei:\n");
                int categoryId = int.Parse(Console.ReadLine());
                string response = await DeleteCategoryApi(categoryId);
                Console.WriteLine(response);
            }
            catch {
                Console.WriteLine("Eroare la introducerea ID-ului");
            }
        }
    }
}
