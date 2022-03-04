using System;

namespace SignatureMakhovZaur
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите полный адрес файла: ");
            string path = Console.ReadLine();

            Console.Write("Какого размера будут блоки ?: ");
            long size = Converter.Convert(Console.ReadLine());

            GetFile currentFile = new GetFile(path, size);

            currentFile.Read();

            Console.WriteLine("Работа окончена.");

            Console.ReadKey();
        }
    }
}
