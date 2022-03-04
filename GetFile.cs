using System;
using System.IO;

namespace SignatureMakhovZaur
{
    class GetFile
    {
        private readonly string path;
        private readonly long blockSize;
        private readonly long countBlock;
        private readonly byte[] buffer;

        public GetFile(string path, long size)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    this.path = path;
                    blockSize = size;
                    buffer = new byte[size];
                    countBlock = fileInfo.Length / size - 1; // получаем количество блоков и убираем последний (по заданию)
                }
                else
                {
                    throw new FileNotFoundException("Файл не найден!");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Environment.Exit(0);
            }
        }

        public void Read()
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int counter = 0;
                using (PersonalThreadPool personalThread = new PersonalThreadPool()) // создаем свой пул потоков и работаем с ним
                {
                    while (counter < countBlock)
                    {
                        fs.Read(buffer, 0, (int)blockSize);
                        counter++;

                        personalThread.Queue(buffer); // отправка в очередь порции данных
                    }
                }
            }
        }

    }
}
