using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class EnabledDisabledInventory : MonoBehaviour
    {
        [SerializeField] private GameObject _panelInventory;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
        }

        public void Show()
        {
            _panelInventory.SetActive(true);
            GlobalEvents.SendShowInventory();
            GlobalEvents.SendCurrentInventoryState();
        }

        public void Hide()
        {
            _panelInventory.SetActive(false);
            GlobalEvents.SendStartResetDraggedItem();
        }
    }
}