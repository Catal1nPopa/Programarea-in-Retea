using Laborator4UtmShop.Methods.DeleteMethods;
using Laborator4UtmShop.Methods.GetMethods;
using Laborator4UtmShop.Methods.PostMethods;
using Laborator4UtmShop.Methods.PutMethods;


class Program
{
    static async Task Main(string[] args)
    {
        bool showMenu = true;
        while (showMenu)
        {
            showMenu = await MainMenu();
        }
    }

    static async Task<bool> MainMenu()
    {
        Console.Clear();
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                        Meniu Principal                     ║");
        Console.WriteLine("╠════════════════════════════════════════════════════════════╣");
        Console.WriteLine("║ 1. Afiseaza toate categoriile                              ║");
        Console.WriteLine("║ 2. Adauga o categorie noua                                 ║");
        Console.WriteLine("║ 3. Sterge o categorie                                      ║");
        Console.WriteLine("║ 4. Schimba titlu unei categorii                            ║");
        Console.WriteLine("║ 5. Afisare informatii despre categorie utilizand ID-ul     ║");
        Console.WriteLine("║ 6. Afisare informatii despre categorie utilizand titlul    ║");
        Console.WriteLine("║ 7. Afisare produse din categorie                           ║");
        Console.WriteLine("║ 8. Adauga produs nou intr-o categorie                      ║");
        Console.WriteLine("║ 9. Iesire                                                  ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝");

        switch (Console.ReadLine())
        {
            case "1":
                await GetCategories.getCategory();
                WaitForKey();
                return true;
            case "2":
                await AddCategory.addCategory();
                WaitForKey();
                return true;
            case "3":
                await DeleteCategory.DeleteCategoryWithId();
                WaitForKey();
                return true;
            case "4":
                await ChangeTittle.ChangeTitleCategoryWithId();
                WaitForKey();
                return true;
            case "5":
                await GetInfoCategory.GetInfoCategoryById();
                WaitForKey();
                return true;
            case "6":
                await GetInfoCategory.GetInfoCategoryApiByTitle();
                WaitForKey();
                return true;
            case "7":
                await GetProductsFromCategory.GetProductsByIdCategory();
                WaitForKey();
                return true;
            case "8":
                await AddProducts.AddProduct();
                WaitForKey();
                return true;
            case "9":
                return false;
            default:
                return true;
        }
    }

    static void WaitForKey()
    {
        Console.WriteLine("\nApasă orice buton pentru a reveni la meniul principal...");
        Console.ReadKey();
    }

}