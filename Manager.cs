using System;
using System.IO;
using System.Security.Cryptography;

namespace SignatureMakhovZaur
{
    class Manager
    {
        private readonly BlockingQueue<FileBlock> workingQueue;
        private readonly HashAlgorithm hashAlgorithm;

        private readonly string filePath;
        private readonly long blockSize;

        public Manager(HashAlgorithm algorithm, string path, long blockSize)
        {
            this.workingQueue = new BlockingQueue<FileBlock>();
            this.hashAlgorithm = SHA256.Create();
            this.filePath = path;
            this.blockSize = blockSize;
        }

        public void Start()
        {
            // Создаем обработчики
            int countHandlers = Environment.ProcessorCount;
            var handlers = new BlockHandler[countHandlers];

            for (int i = 0; i < handlers.Length; i++)
            {
                handlers[i] = new BlockHandler(queue, shaHashAlgorithm);
            }

            // Создаем читателя
            long countBlock;
            byte[] buffer;
            countBlock = fileInfo.Length / this.blockSize - 1; // получаем количество блоков и убираем последний (по заданию)

            Manager.GetFile(path, size);
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                int counter = 0;
                while (counter < countBlock)
                {
                    fs.Read(buffer, 0, (int)size);
                    var fileBlock = new FileBlock(buffer, counter);
                    counter++;

                    queue.Queue(fileBlock); // отправка в очередь порции данных
                }
            }
        }


        static private void GetFile(string path, long size)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                {
                    this.path = path;
                    blockSize = size;
                    buffer = new byte[size];

                }
                else
                {
                    throw new FileNotFoundException("Файл не найден!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                Environment.Exit(0);
            }
        }
    }
}
