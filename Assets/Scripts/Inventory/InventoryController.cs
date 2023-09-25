using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPage _inventoryPage;
        [SerializeField] private InventorySo _inventoryData;

        public List<InventorySo.InventoryItem> initialItems = new List<InventorySo.InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            GlobalEvents.OnStartCurrentInventoryState.AddListener(CurrentInventoryState);
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdate += UpdateInventoryUI;
            foreach (InventorySo.InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                _inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventorySo.InventoryItem> inventoryState)
        {
            _inventoryPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryPage.UpdateData(item.Key, item.Value.Item.ImageItem, item.Value.Quantity);
            }
        }

        private void PrepareUI()
        {
            _inventoryPage.InitializeInventory(_inventoryData.Size);
            _inventoryPage.OnDescription += HandleDescription;
            _inventoryPage.OnSwapItem += HandleSwapItem;
            _inventoryPage.OnStartDragging += HandleDragging;
            _inventoryPage.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
        }

        private void HandleDragging(int itemIndex)
        {
            InventorySo.InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryPage.CreateDraggedItem(inventoryItem.Item.ImageItem,
                inventoryItem.Quantity);
        }

        private void HandleSwapItem(int itemIndex1, int itemIndex2)
        {
            _inventoryData.SwapItem(itemIndex1, itemIndex2);
        }

        private void HandleDescription(int itemIndex)
        {
            InventorySo.InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetSelection();
                return;
            }

            ItemSo item = inventoryItem.Item;
            _inventoryPage.UpdateDescription(itemIndex, item.ImageItem, item.NameItem);
        }

        private void CurrentInventoryState()
        {
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryPage.UpdateData(item.Key,
                    item.Value.Item.ImageItem,
                    item.Value.Quantity);
            }
        }
    }
}