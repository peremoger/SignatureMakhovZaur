class BlockingQueue<T> : IDisposable
{
    private readonly Queue<T> blocks; // использована очередь чтобы выдержать постоянный порядок блоков
    private readonly object lockObj = new object();
    public bool isDisposed;

    // пополнение очереди новой порцией данных
    public void Enqueue(T item)
    {
        blocks.Enqueue(item);
        Monitor.Pulse(lockObj); // если есть данные в очереди, уведомляем поток в очереди
    }

    // метод, запускаемый в потоках
    // при наличии данных в очереди извлекает и
    // вызывает метод вычисления значения хэш-функции
    public bool Dequeue(out T value)
    {
        if (isDisposed)
        {
            return false;
        }

        if (blocks.Count == 0)
        {
            Monitor.Wait(lockObj);
        }

        value = this.blocks.Dequeue();
        
        return true;
    }

    public void Dispose()
    {
        this.isDisposed = true;
        Monitor.PulseAll(lockObj); // уведомляем все потоки
    }
}