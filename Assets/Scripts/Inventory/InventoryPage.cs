using System;
using System.Collections.Generic;
using Events;
using UnityEngine;

namespace Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private MouseFollower _mouseFollower;
        [SerializeField] private ItemActionPanel _actionPanel;

        private List<UIInventoryItem> _listUIItems = new List<UIInventoryItem>();

        private int _currentlyDraggedItemIndex = -1;

        public event Action<int> OnItemActionRequested, OnStartDragging, OnDescription;
        public event Action<int, int> OnSwapItem;

        private void Awake()
        {
            GlobalEvents.OnStartShowInventory.AddListener(ResetSelection);
            GlobalEvents.OnStartResetDraggetItem.AddListener(ResetDraggtedItem);
            GlobalEvents.OnStartDisableActionPanel.AddListener(DisableActionPanel);

            _mouseFollower.Toggle(false);
        }

        public void InitializeInventory(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, transform.position, transform.rotation);
                uiItem.transform.SetParent(_contentPanel, false);
                _listUIItems.Add(uiItem);

                uiItem.OnItemClicked += HandleItemSelection;
                uiItem.OnItemBeginDrag += HandleBeginDrag;
                uiItem.OnItemDroppedOn += HandleSwap;
                uiItem.OnItemEndDrag += HandleEndDeag;
                uiItem.OnRightMouseButtonClick += HandleShowItemActions;
            }
        }

        public void UpdateData(int itemIndex, Sprite itemImage, int itemQuantity)
        {
            if (_listUIItems.Count > itemIndex)
            {
                _listUIItems[itemIndex].SetData(itemImage, itemQuantity);
            }
        }

        private void HandleShowItemActions(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);

            if (index == -1)
            {
                return;
            }

            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDeag(UIInventoryItem inventoryItem)
        {
            ResetDraggtedItem();
        }

        private void HandleSwap(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);

            if (index == -1)
            {
                return;
            }

            OnSwapItem?.Invoke(_currentlyDraggedItemIndex, index);
            HandleItemSelection(inventoryItem);
        }

        private void DisableActionPanel()
        {
            _actionPanel.Toggle(false);
        }

        private void ResetDraggtedItem()
        {
            _mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);
            if (index == -1)
                return;
            _currentlyDraggedItemIndex = index;
            HandleItemSelection(inventoryItem);
            OnStartDragging?.Invoke(index);
        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(sprite, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);

            if (index == -1)
            {
                return;
            }

            OnDescription?.Invoke(index);
        }

        public void ResetSelection()
        {
            DeselectAllItems();
        }

        public void AddAction(string actionName, Action performAction)
        {
            _actionPanel.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int itemIndex)
        {
            _actionPanel.Toggle(true);
            _actionPanel.transform.position = _listUIItems[itemIndex].transform.position;
        }

        private void DeselectAllItems()
        {
            foreach (UIInventoryItem item in _listUIItems)
            {
                item.Deselect();
            }

            _actionPanel.Toggle(false);
        }

        protected virtual void OnOnItemActionRequested(int obj)
        {
            OnItemActionRequested?.Invoke(obj);
        }

        protected virtual void OnOnStartDragging(int obj)
        {
            OnStartDragging?.Invoke(obj);
        }

        protected virtual void OnOnSwapItem(int arg1, int arg2)
        {
            OnSwapItem?.Invoke(arg1, arg2);
        }

        protected virtual void OnOnDescription(int obj)
        {
            OnDescription?.Invoke(obj);
        }

        public void ResetAllItems()
        {
            foreach (var item in _listUIItems)
            {
                item.ResetData();
                item.Deselect();
            }
        }

        public void UpdateDescription(int itemIndex, Sprite itemImageItem, string itemNameItem)
        {
            DeselectAllItems();
            _listUIItems[itemIndex].Select();
        }
    }
}