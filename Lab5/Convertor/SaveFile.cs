using Org.BouncyCastle.Utilities;
using System.Xml.Linq;

namespace EmailApp.Convertor
{
    public class SaveFile
    {
        public static void Save(string directoryPath, string Name,byte[] bytes)
        {
            //if (File.Exists(Path.Combine(directoryPath, Name)))
            //{
            //    DeleteTempFile.Delete(Path.Combine(directoryPath, Name));
            //    return;
            //}
            string filePath = Path.Combine(directoryPath, Name);
           
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
            }
            Console.WriteLine("Fisierul a fost salvat cu succes!");
        }
    }
}
