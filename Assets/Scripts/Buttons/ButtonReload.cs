using Events;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class ButtonReload : MonoBehaviour
    {
        [SerializeField] private Image _imageButton;
        private Button _button;


        private void Awake()
        {
            _button = GetComponent<Button>();

            GlobalEvents.OnStartSendToReloadGun.AddListener(EnableImageButton);
            GlobalEvents.OnStartDisableButtonReload.AddListener(DisabledImage);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(GlobalEvents.SendStartReloadGun);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(GlobalEvents.SendStartReloadGun);
        }

        private void EnableImageButton()
        {
            _imageButton.enabled = true;
        }

        private void DisabledImage()
        {
            _imageButton.enabled = false;
        }
    }
}