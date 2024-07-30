
using System;
using System.Linq;

namespace Benchmasters.Items
{
    /// <summary>
    /// Represents container of items. Item presented as pair of item type and
    /// quantity. All items are located in slots of the certain size.
    /// </summary>
    public class ItemBag
    {
        /// <summary>
        /// Represents single cell in inventory.
        /// </summary>
        public class Slot
        {
            private ItemHeap _itemHeap;

            public ItemType Type => _itemHeap.Type;
            public int Quantity => _itemHeap.Quantity;

            public bool IsEmpty => Quantity == 0;

            public ItemHeap ItemHeap => _itemHeap;

            public Slot()
            {
                _itemHeap = ItemHeap.Nothing;
            }

            public void ReplaceWith(ItemHeap heap)
            {
                _itemHeap = heap;
            }

            /// <summary>
            /// Add quantity of the given heap to the quantity of slot.
            /// Adding empty and nothing heaps not allowed.
            /// </summary>
            /// <param name="heap">Heap to merge this slot with</param>
            /// <returns>true, if merging is performed</returns>
            public bool Merge(ItemHeap heap)
            {
                // Do not merge if heap is empty or nothing
                if (heap.IsEmpty || heap.IsNothing)
                {
                    return false;
                }
                // Do not merge if heaps are not similar
                if (heap.Type != Type)
                {
                    return false;
                }
                _itemHeap = new ItemHeap(heap.Type, heap.Quantity + Quantity);
                return true;
            }

            /// <summary>
            /// Try slice a new item heap from this slot.
            /// </summary>
            /// <param name="amount">Amount to slice</param>
            /// <param name="result">Resulting item heap, if slicing performed</param>
            /// <returns>true, if slicing performed</returns>
            public bool TrySlice(int amount, out ItemHeap result)
            {
                result = ItemHeap.Nothing;

                if (amount > Quantity)
                {
                    return false;
                }

                _itemHeap = new ItemHeap(Type, Quantity - amount);
                result = new ItemHeap(Type, amount);

                return true;
            }

            /// <summary>
            /// Slice all of this slot.
            /// </summary>
            /// <returns>Sliced item heap</returns>
            public ItemHeap SliceAll()
            {
                _itemHeap = new ItemHeap(Type, 0);
                return new ItemHeap(Type, Quantity);
            }
        }

        private readonly Slot[] _slots;

        public ItemBag(int capacity)
        {
            _slots = new Slot[capacity];
        }

        private Slot FindFirstEmptySlot()
        {
            return _slots.First((heap) => heap.IsEmpty);
        }

        private Slot FindSlotWithTypeOf(ItemType type)
        {
            return _slots.First((slot) => slot.Type == type);
        }

        /// <summary>
        /// Push item heap to inventory. 
        /// </summary>
        /// <param name="heap">Item heap to push</param>
        /// <returns>true, if pushed, otherwise false</returns>
        public bool Push(ItemHeap heap)
        {
            if (heap.IsEmpty)
            {
                return false;
            }

            Slot availableSlot;

            // try to merge with the heap of same type
            availableSlot = FindSlotWithTypeOf(heap.Type);
            if (availableSlot != null && availableSlot.Merge(heap))
            {
                return true;
            }

            // or try replace with empty heap
            availableSlot = FindFirstEmptySlot();
            if (availableSlot != null)
            {
                availableSlot.ReplaceWith(heap);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Add item to inventory at the certain index. Empty heaps not allowed to add.
        /// </summary>
        /// <param name="heap">Item heap to add</param>
        /// <param name="index">Index to add to</param>
        /// <returns>true, if added, otherwise false</returns>
        public bool Add(ItemHeap heap, int index)
        {
            if (heap.IsEmpty)
            {
                return false;
            }

            Slot availableSlot;

            availableSlot = Get(index);
            if (heap.Type == availableSlot.Type)
            {
                availableSlot.Merge(heap);
                return true;
            }
            if (availableSlot.IsEmpty)
            {
                availableSlot.ReplaceWith(heap);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get slot on the given index.
        /// </summary>
        /// <param name="index">Index of the slot</param>
        /// <returns>Slot reference</returns>
        public Slot Get(int index)
        {
            return _slots[index];
        }

        public ItemHeap Pop(int index)
        {
            var certainHeap = Get(index);
            return certainHeap.SliceAll();
        }

        /// <summary>
        /// Has item heap, matching this predicate.
        /// </summary>
        /// <param name="predicate">Mathcing predicate</param>
        /// <returns>true if has otherwise false</returns>
        public bool Has(Func<ItemHeap, bool> predicate)
        {
            return _slots.All((slot) => predicate(slot.ItemHeap));
        }
    }
}
