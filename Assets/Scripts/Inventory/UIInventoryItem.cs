using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler,
        IDropHandler, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TextMeshProUGUI _quantityText;
        [SerializeField] private Image _borderImage;
        [SerializeField] private float _pressDuration = 0.5f;

        private bool isPressing = false;
        private float pressTime = 0f;


        public event Action<UIInventoryItem> OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseButtonClick;

        private bool _isEmpty = true;

        private void Awake()
        {
            ResetData();
            Deselect();
        }

        private void Update()
        {
            if (isPressing && Time.time - pressTime >= _pressDuration)
            {
                OnRightMouseButtonClick?.Invoke(this);
                isPressing = false;
            }
        }

        public void ResetData()
        {
            _itemImage.gameObject.SetActive(false);
            _isEmpty = true;
        }

        public void Deselect()
        {
            _borderImage.enabled = false;
        }

        public void SetData(Sprite sprite, int quality)
        {
            _itemImage.gameObject.SetActive(true);
            _itemImage.sprite = sprite;

            if (quality == 1)
            {
                _isEmpty = false;
                return;
            }

            _quantityText.text = quality + "";
            _isEmpty = false;
        }

        public void Select()
        {
            _borderImage.enabled = true;
        }

        public void OnPointerClick(PointerEventData pointerData)
        {
            if (pointerData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseButtonClick?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_isEmpty)
                return;
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                isPressing = true;
                pressTime = Time.time;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                isPressing = false;
            }
        }
    }
}