using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPage _inventoryPage;
        [SerializeField] private InventorySo _inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            GlobalEvents.OnStartCurrentInventoryState.AddListener(CurrentInventoryState);
        }

        private void PrepareInventoryData()
        {
            _inventoryData.Initialize();
            _inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                _inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryPage.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryPage.UpdateData(item.Key, item.Value.item.ImageItem, 
                    item.Value.quantity);
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
        {InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            _inventoryPage.CreateDraggedItem(inventoryItem.item.ImageItem, inventoryItem.quantity);
        }

        private void HandleSwapItem(int itemIndex1, int itemIndex2)
        {
            _inventoryData.SwapItems(itemIndex1, itemIndex2);
        }

        private void HandleDescription(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryPage.ResetSelection();
                return;
            }

            ItemSo item = inventoryItem.item;
            _inventoryPage.UpdateDescription(itemIndex, item.ImageItem, item.NameItem);
        }

        private void CurrentInventoryState()
        {
            foreach (var item in _inventoryData.GetCurrentInventoryState())
            {
                _inventoryPage.UpdateData(item.Key,
                    item.Value.item.ImageItem,
                    item.Value.quantity);
            }
        }
    }
}