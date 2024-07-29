
using Godot;

namespace Benchmasters.Items
{
    /// <summary>
    /// Represents item kind.
    /// </summary>
    [GlobalClass]
    public partial class ItemType : Resource
    {
        [Export]
        public string Name { get; set; }

        [Export]
        public string Description { get; set; }

        [Export(PropertyHint.Enum, "Common,Uncommon,Rare,Epic,Legendary")]
        public Rarity Rarity { get; set; }

        [Export]
        public Texture2D Icon { get; set; }
    }
}
