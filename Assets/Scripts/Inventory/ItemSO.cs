using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ItemSO")]
    public class ItemSo : ScriptableObject
    {
        [field: SerializeField] public bool IsStackable { get; set; }
        public int ID => GetInstanceID();

        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        [field: SerializeField] public string NameItem { get; set; }
        [field: SerializeField] public Sprite ImageItem { get; set; }
    }
}