namespace Task2;

// Программа на C# для определения приблизительного размера папки
using System;
using System.IO;

class GFG
{

      static public void Main()
    {

        // Получаем информацию о каталоге с помощью метода DirectoryInfo()
        DirectoryInfo folder = new DirectoryInfo("C:\\temp");

        // Вызов метода FolderSize() 
        long totalFolderSize = folderSize(folder);

        Console.WriteLine("Total folder size in bytes: " +
                          totalFolderSize);
    }

    // Функция для расчета размера папки
    static long folderSize(DirectoryInfo folder)
    {
        long totalSizeOfDir = 0;

        // Все файлы в каталог
        FileInfo[] allFiles = folder.GetFiles();

        // Получение размера каждого файла
        foreach (FileInfo file in allFiles)
        {
            totalSizeOfDir += file.Length;
        }

        // Поиск подкаталогов
        DirectoryInfo[] subFolders = folder.GetDirectories();

        // Получение размеров каждого подкаталога 
        foreach (DirectoryInfo dir in subFolders)
        {
            totalSizeOfDir += folderSize(dir);
        }

        // Общий размер 
        return totalSizeOfDir;
    }
}