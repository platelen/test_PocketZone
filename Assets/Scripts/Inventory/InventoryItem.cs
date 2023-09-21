using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
    public class InventoryItem : ScriptableObject
    {
        public string ItemName;
        public Sprite ItemImage;
        public int numberHeld;
        public bool IsUsable;
        public bool IsUnique;
    }
}