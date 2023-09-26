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
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                _inventoryPage.ShowItemAction(itemIndex);
                _inventoryPage.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryPage.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        private void DropItem(int itemIndex, int quantity)
        {
            _inventoryData.RemoveItem(itemIndex, quantity);
            _inventoryPage.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
                return;
            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                _inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject);
                if (_inventoryData.GetItemAt(itemIndex).IsEmpty)
                {
                    _inventoryPage.ResetSelection();
                }
            }
        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = _inventoryData.GetItemAt(itemIndex);
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