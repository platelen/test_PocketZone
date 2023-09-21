using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField] private UIInventoryItem _itemPrefab;
        [SerializeField] private RectTransform _contentPanel;

        private List<UIInventoryItem> _listUIItems = new List<UIInventoryItem>();

        public void InitializeInventory(int inventorysize)
        {
            for (int i = 0; i < inventorysize; i++)
            {
                UIInventoryItem uiItem = Instantiate(_itemPrefab, transform.position, transform.rotation);
                uiItem.transform.SetParent(_contentPanel, false);
                _listUIItems.Add(uiItem);
            }
        }
    }
}