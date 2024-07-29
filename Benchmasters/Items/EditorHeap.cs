
using Godot;

namespace Benchmasters.Items
{
    /// <summary>
    /// Represents a bunch of items with the same type and quantity value, but
    /// can be edited in Godot editor.
    /// </summary>
    [GlobalClass]
    public partial class EditorHeap : Resource
    {
        [Export]
        public ItemType Type { get; set; }

        [Export]
        public int Quantity { get; set; }

        public ItemHeap ItemHeap => new(Type, Quantity);
    }
}
