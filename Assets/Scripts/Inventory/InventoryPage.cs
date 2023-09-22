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

        private List<UIInventoryItem> _listUIItems = new List<UIInventoryItem>();

        public Sprite image, image2;
        public int quantity;
        public string title;

        private int _currentlyDraggedItemIndex = -1;

        private void Awake()
        {
            GlobalEvents.OnStartShowInventory.AddListener(SetDataInList);
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

        private void HandleShowItemActions(UIInventoryItem inventoryItem)
        {
        }

        private void HandleEndDeag(UIInventoryItem inventoryItem)
        {
            _mouseFollower.Toggle(false);
        }

        private void HandleSwap(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);

            if (index == -1)
            {
                _mouseFollower.Toggle(false);
                _currentlyDraggedItemIndex = -1;
            }

            _listUIItems[_currentlyDraggedItemIndex].SetData(
                index == 0 ? image : image2, quantity);
            _listUIItems[index].SetData(
                _currentlyDraggedItemIndex == 0 ? image : image2, quantity);
            _mouseFollower.Toggle(false);
            _currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(UIInventoryItem inventoryItem)
        {
            int index = _listUIItems.IndexOf(inventoryItem);
            if (index == -1)
                return;
            _currentlyDraggedItemIndex = index;
            _mouseFollower.Toggle(true);
            _mouseFollower.SetData(index == 0 ? image : image2, quantity);
        }

        private void HandleItemSelection(UIInventoryItem inventoryItem)
        {
            _listUIItems[0].Select();
        }

        private void SetDataInList()
        {
            _listUIItems[0].SetData(image, quantity);
            _listUIItems[1].SetData(image2, quantity);
        }
    }
}