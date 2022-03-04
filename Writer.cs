


class Writer
{
    private readonly BlockingQueue<FileBlock> workingQueue;
    private readonly Thread thread;

    public Writer(BlockingQueue<FileBlock> workingQueue, HashAlgorithm hashAlgorithm)
    {
        this.workingQueue = workingQueue;
        this.hashAlgorithm = hashAlgorithm;

        this.thread = new Thread(() => Work());
        this.thread.Start();
    }

    private void Work()
    {
        while (workingQueue.Dequeue(out var block))
        {
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