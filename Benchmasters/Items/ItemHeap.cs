
namespace Benchmasters.Items
{
    /// <summary>
    /// Represents a bunch of items with the same type and quantity value.
    /// </summary>
    public class ItemHeap
    {
        public ItemType Type { get; }
        public int Quantity { get; }

        public bool IsEmpty => Quantity == 0;

        public ItemHeap(ItemType type) : this(type, 0) { }

        public ItemHeap(ItemType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
