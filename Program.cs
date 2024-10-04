namespace Task1 
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TimeSpan interval = TimeSpan.FromMinutes(30);          
            Console.WriteLine("Укажите путь до каталога:");
            string path = Console.ReadLine();
            if (Directory.Exists(path))
            {
                DirectoryInfo dirTime = new DirectoryInfo(path);
                LookFiles(dirTime, interval);
            }
            else Console.WriteLine("Папка по данному пути не найдена:");
            Console.ReadLine();
        }
        static void LookFiles(DirectoryInfo directory, TimeSpan interval)
        {
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                CheckFile(file, interval);
            }
            DirectoryInfo[] dirs = directory.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                CheckDir(dir, interval);
            }
        }
        static void CheckFile(FileInfo file, TimeSpan interval)
        {
            if (DateTime.Now - file.LastAccessTime > interval)
            {
                Console.WriteLine($"Файл {file.Name} не использовался 30 минут и будет удален:");
                try
                {
                    file.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        static void CheckDir(DirectoryInfo dir, TimeSpan interval)
        {
            if ((DateTime.Now - dir.LastAccessTime > interval) & !IsFilesUsedInInterval(dir, interval))
            {
                Console.WriteLine($"Папка {dir.Name} не использовалась 30 минут и будет удалена:");
                try
                {
                    dir.Delete(true);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        } 
        static bool IsFilesUsedInInterval(DirectoryInfo dir, TimeSpan interval)
        {
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                if (DateTime.Now - dir.LastAccessTime < interval) return true;
            }
            DirectoryInfo[] dirs = dir.GetDirectories();
            foreach (DirectoryInfo directory in dirs)
            {
                IsFilesUsedInInterval(directory, interval);
            }
            return false;
        }
    }
}
