
using Benchmasters.Items;
using Godot;
using System.Linq;

namespace Benchmasters.Recipes
{
    [GlobalClass]
    public partial class Recipe : Resource
    {
        [Export]
        public EditorHeap[] InputItems { get; set; }

        [Export]
        public EditorHeap[] OutputItems { get; set; }

        public bool CanCraftOf(ItemBag bag)
        {
            // check, whether a bag has item heaps with type of input item and quantity bigger than input item
            return InputItems.All(
                (editorHeap) => bag.Has(
                    (heap) => heap.Type == editorHeap.Type && heap.Quantity >= editorHeap.Quantity
                )
            );
        }
    }
}
