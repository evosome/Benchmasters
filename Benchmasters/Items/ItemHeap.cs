
namespace Benchmasters.Items
{
    /// <summary>
    /// Represents a bunch of items with the same type and quantity value.
    /// </summary>
    public struct ItemHeap
    {
        public ItemType Type { get; private set; }
        public int Quantity { get; private set; }

        public bool IsEmpty => Quantity == 0;

        /// <summary>
        /// Determine whether the item heap is nothing (has no item type)
        /// </summary>
        public bool IsNothing => Type == null;

        /// <summary>
        /// Item heap with no item type.
        /// </summary>
        public static ItemHeap Nothing => new(null);

        public ItemHeap(ItemType type) : this(type, 0) { }

        public ItemHeap(ItemType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }
    }
}
