using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Linq;


namespace FinalTask
{

    [Serializable]
    internal class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal AverageScore { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            List<string> LGroup = new List<string>();
            string GroupCache = "";

            BinaryFormatter formatter = new BinaryFormatter();
            using (var fs = new FileStream("C:\\Temp\\Students.dat", FileMode.OpenOrCreate))
            {
                var student = (Student[])formatter.Deserialize(fs);
                //Выводим полученные данные (параллельно формируем группы)
                for (int il = 0; il < student.Count(); il++)
                {
                    Console.WriteLine(student[il].Name + " / " + student[il].Group + " / " + student[il].DateOfBirth);
                    if (GroupCache.Contains(student[il].Group + ",") == false)
                    {
                        LGroup.Add(student[il].Group);
                        GroupCache = GroupCache + student[il].Group + ",";
                    }
                }
                //Выводим список групп
                for (int ig = 0; ig < LGroup.Count; ig++)
                    Console.WriteLine(LGroup[ig]);
                //Сохранение данных в файлы по группам
                if (Directory.Exists("C:\\Temp\\Student\\") != true) // Проверим, что директория существует
                    Directory.CreateDirectory("C:\\Temp\\Student\\");
                for (int ig = 0; ig < LGroup.Count; ig++)
                {
                    StreamWriter sw = new StreamWriter("C:\\Temp\\Student\\" + LGroup[ig] + ".txt");
                    for (int il = 0; il < student.Count(); il++)
                        if (student[il].Group == LGroup[ig])
                            sw.WriteLine(student[il].Name + "\t" + student[il].DateOfBirth);
                    sw.Close();
                }
            }
        }

    }
}