public interface OrderedBlockingQueueItem
{
    int OrderNumber { get; set; }
}

class OrderedBlockingQueue<T> where OrderedBlockingQueueItem : IDisposable
{
    private readonly Dictionary<int, T> orderNumberBlock; // использована очередь чтобы выдержать постоянный порядок блоков
    private readonly object lockObj = new object();
    public bool isDisposed;

    // пополнение очереди новой порцией данных
    public void Enqueue(T item)
    {
        blocks.Add(item.OrderNumber, item);
        Monitor.Pulse(lockObj); // если есть данные в очереди, уведомляем поток в очереди
    }

    // метод, запускаемый в потоках
    // при наличии данных в очереди извлекает и
    // вызывает метод вычисления значения хэш-функции
    public bool Dequeue<T>(int orderNumber, out T value)
    {
        if (isDisposed)
        {
            return false;
        }

        if (blocks.Count == 0)
        {
            Monitor.Wait(lockObj);
        }

        hasValue = this.orderNumberBlock.TryGetValue(orderNumber, out int value);

        if (!hasValue)
        {
            Monitor.Wait(lockObj);
        }

        return hasValue;
    }

    public void Dispose()
    {
        this.isDisposed = true;
        Monitor.PulseAll(lockObj); // уведомляем все потоки
    }
}