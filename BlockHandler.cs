using System.Security.Cryptography;
using System.Threading;

class BlockHandler
{
    private readonly BlockingQueue<FileBlock> readQueue;
    private readonly HashAlgorithm hashAlgorithm;
    private readonly Thread thread;

    public BlockHandler(BlockingQueue<FileBlock> readQueue, HashAlgorithm hashAlgorithm)
    {
        this.readQueue = readQueue;
        this.hashAlgorithm = hashAlgorithm;

        this.thread = new Thread(() => Work());
        this.thread.Start();
    }

    private void Work()
    {
        while (readQueue.Dequeue(out var block))
        {
            block.SetHash(this.GetHash(block));
            Console.WriteLine("{0} {1}", counter, this.GetHash(block));
        }
    }

    private void GetHash(HashAlgorithm hashAlgorithm)
    {
        byte[] hash = hashAlgorithm.ComputeHash(bytes);

        // на stackoverflow до сих пор спорят выводить с помощью linq или bitconverter. Выбрал первый вариант :)
        string hashString = string.Join("", hash.Select(f => f.ToString("X2")).ToArray());

        return hashString;
    }
}