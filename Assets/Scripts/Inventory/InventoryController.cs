using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPage _inventoryPage;

        public int inventorySize = 10;

        private void Start()
        {
            _inventoryPage.InitializeInventory(inventorySize);
        }
    }
}