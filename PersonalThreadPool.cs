using System;
using System.Collections.Generic;
using System.Threading;

namespace SignatureMakhovZaur
{
    class PersonalThreadPool : IDisposable
    {
        private readonly Thread[] threads;
        private readonly Queue<byte[]> blocks; // использована очередь чтобы выдержать постоянный порядок блоков
        private readonly object lockObj = new object();
        private long counter;        
        public bool isDisposed;

        // конструктор с созданием и запуском потоков, количественно
        // равных числу доступных процессов
        public PersonalThreadPool()
        {
            int countThreads = Environment.ProcessorCount;

            threads = new Thread[countThreads];
            blocks = new Queue<byte[]>();

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(ActiveWork);
                threads[i].Start();
            }
        }

        // пополнение очереди новой порцией данных
        public void Queue(byte[] buffer)
        {
            lock (lockObj)
            {
                blocks.Enqueue(buffer);

                if (blocks.Count > 0)
                {
                    Monitor.Pulse(lockObj); // если есть данные в очереди, уведомляем поток в очереди
                }
            }
        }

        // метод, запускаемый в потоках
        // при наличии данных в очереди извлекает и
        // вызывает метод вычисления значения хэш-функции
        public void ActiveWork()
        {
            while (true)
            {
                lock (lockObj)
                {
                    if (isDisposed)
                    {
                        return;
                    }

                    if (blocks.Count > 0)
                    {
                        Interlocked.Increment(ref counter);
                        Console.WriteLine("{0} {1}", counter, Signature.HashSign(blocks.Dequeue()));
                    }
                    else
                    {
                        Monitor.Wait(lockObj); // если очередь пуста, то освобождаем блокировку
                        continue;
                    }
                }
            }
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                lock (lockObj)
                {
                    if (!isDisposed) // double check чтобы не проиграть
                    {
                        isDisposed = true;
                        Monitor.PulseAll(lockObj); // уведомляем все потоки
                    }
                }
            }
        }
    }
}
