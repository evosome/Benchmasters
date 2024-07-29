
using System;
using System.Linq;

namespace Benchmasters.Items
{
    public class Inventory
    {
        private readonly ItemHeap[] _itemHeaps;

        public Inventory(int capacity)
        {
            _itemHeaps = new ItemHeap[capacity];
            Array.Fill(_itemHeaps, default);
        }

        private ItemHeap FindFirstEmpty()
        {
            return _itemHeaps.First((heap) => heap.IsEmpty);
        }

        private ItemHeap FindWithSameType(ItemType type)
        {
            return _itemHeaps.First((heap) => heap.Type == type);
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

            // try to merge with the heap of same type
            var sameHeap = FindWithSameType(heap.Type);
            if (sameHeap != null)
            {
                sameHeap.Merge(heap);
                return true;
            }

            // or try replace with empty heap
            var emptyHeap = FindFirstEmpty();
            if (emptyHeap != null)
            {
                emptyHeap.ReplaceWith(heap);
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

            var certainHeap = Get(index);
            if (certainHeap != null && certainHeap.Type == heap.Type)
            {
                certainHeap.Merge(heap);
                return true;
            }

            return false;
        }

        public ItemHeap Get(int index)
        {
            return _itemHeaps[index];
        }

        public ItemHeap Pop(int index)
        {
            var certainHeap = Get(index);
            return certainHeap.WithdrawAll();
        }
    }
}
