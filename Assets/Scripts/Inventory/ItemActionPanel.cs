using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _buttonPrefab;

        public void AddButton(string name, Action onClickAction)
        {
            GameObject button = Instantiate(_buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener((() => onClickAction()));
            button.GetComponentInChildren<TextMeshProUGUI>().text = name;
        }

        public void Toggle(bool val)
        {
            if (val == true)
            {
                RemoveOldButtons();
            }

            gameObject.SetActive(val);
        }

        private void RemoveOldButtons()
        {
            foreach (Transform transformChildOjects in transform)
            {
                Destroy(transformChildOjects.gameObject);
            }
        }
    }
}