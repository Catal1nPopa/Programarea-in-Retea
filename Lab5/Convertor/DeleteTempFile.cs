namespace EmailApp.Convertor
{
    public class DeleteTempFile
    {
        public static void Delete(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Console.WriteLine("Fisierul temporar a fost sters cu succes.");
                }
                else
                {
                    Console.WriteLine("Fisierul temporar nu a fost gasit.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
