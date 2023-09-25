using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/InventorySO")]
    public class InventorySo : ScriptableObject
    {
        [SerializeField] private List<InventoryItem> _inventoryItems;
        [field: SerializeField] public int Size { get; private set; } = 10;

        public event Action<Dictionary<int, InventoryItem>> OnInventoryUpdate;

        public void Initialize()
        {
            _inventoryItems = new List<InventoryItem>();
            for (int i = 0; i < Size; i++)
            {
                _inventoryItems.Add(InventoryItem.GetEmptyItem());
            }
        }

        public InventoryItem GetItemAt(int itemIndex)
        {
            return _inventoryItems[itemIndex];
        }

        public void AddItem(ItemSo item, int quantity)
        {
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                {
                    _inventoryItems[i] = new InventoryItem
                    {
                        Item = item,
                        Quantity = quantity,
                    };
                    return;
                }
            }
        }

        public void AddItem(InventoryItem item)
        {
            AddItem(item.Item, item.Quantity);
        }

        [Serializable]
        public struct InventoryItem
        {
            public int Quantity;
            public ItemSo Item;
            public bool IsEmpty => Item == null;

            public InventoryItem ChangeQuantity(int newQuantity)
            {
                return new InventoryItem
                {
                    Item = this.Item,
                    Quantity = newQuantity,
                };
            }

            public static InventoryItem GetEmptyItem() => new InventoryItem
            {
                Item = null,
                Quantity = 0,
            };
        }


        public Dictionary<int, InventoryItem> GetCurrentInventoryState()
        {
            Dictionary<int, InventoryItem> returnValue =
                new Dictionary<int, InventoryItem>();
            for (int i = 0; i < _inventoryItems.Count; i++)
            {
                if (_inventoryItems[i].IsEmpty)
                    continue;
                returnValue[i] = _inventoryItems[i];
            }

            return returnValue;
        }

        public void SwapItem(int itemIndex1, int itemIndex2)
        {
            InventoryItem item1 = _inventoryItems[itemIndex1];
            _inventoryItems[itemIndex1] = _inventoryItems[itemIndex2];
            _inventoryItems[itemIndex2] = item1;
            InformAboutChange();
        }

        private void InformAboutChange()
        {
            OnInventoryUpdate?.Invoke(GetCurrentInventoryState());
        }
    }
}