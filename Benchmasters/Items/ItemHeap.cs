
using System;

namespace Benchmasters.Items
{
    /// <summary>
    /// Represents a bunch of items with the same type and quantity value.
    /// </summary>
    public class ItemHeap
    {
        public ItemType Type { get; private set; }
        public int Quantity { get; private set; }

        public bool IsEmpty => Quantity == 0;

        public ItemHeap(ItemType type) : this(type, 0) { }

        public ItemHeap(ItemType type, int quantity)
        {
            Type = type;
            Quantity = quantity;
        }

        /// <summary>
        /// Merge other item heap into this one. Heaps must be
        /// with the same item type.
        /// </summary>
        /// <param name="heap">Other heap.</param>
        public void Merge(ItemHeap heap)
        {
            if (heap.Type != Type)
            {
                return;
            }
            Quantity += heap.Quantity;
            heap.WithdrawAll();
        }

        /// <summary>
        /// Change item type and item quantity to values of other heap. Other
        /// item heap will be empty. This item heap can be replaced with other, when it's empty.
        /// </summary>
        /// <param name="heap">Heap to replace with</param>
        public void ReplaceWith(ItemHeap heap)
        {
            if (Quantity != 0)
            {
                return;
            }

            Type = heap.Type;
            Quantity = heap.Quantity;
            heap.WithdrawAll();
        }

        /// <summary>
        /// Withdraw all.
        /// </summary>
        /// <returns>Splitted off item heap</returns>
        public ItemHeap WithdrawAll()
        {
            return Withdraw(Quantity);
        }

        /// <summary>
        /// Withdraw the certain amount.
        /// </summary>
        /// <param name="amount">Amount to withdraw</param>
        /// <returns>Splitted off item heap</returns>
        public ItemHeap Withdraw(int amount)
        {
            amount = Math.Min(amount, Quantity);
            Quantity -= amount;
            return new ItemHeap(Type, amount);
        }
    }
}
