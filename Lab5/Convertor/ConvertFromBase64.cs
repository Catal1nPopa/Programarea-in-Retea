namespace EmailApp.Convertor
{
    public class ConvertFromBase64
    {
        public static void ConvertString(string base64, string fileName)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string directoryPath = "D:\\UTM\\ANUL 3\\SEM 2\\PR\\TempEmail\\";
            SaveFile.Save(directoryPath, fileName, bytes);
        }
    }
}
