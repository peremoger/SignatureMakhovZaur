public class FileBlock : OrdereBlockingQueueItem
{
    public int OrderNumber;
    public byte[] Data;
    public string Hash { get; set; }
    public FileBlock(byte[] data, int orderNumber)
    {
        this.Data = data;
        this.OrderNumber = orderNumber;
    }

    public SetHash(string hash)
    {
        Hash = hash;
    }
}