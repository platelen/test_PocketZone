using TMPro;
using UnityEngine;

namespace Events
{
    public class AlertNoAmmo : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _alertNoAmmo;

        private void Awake()
        {
            GlobalEvents.OnStartAlertNoAmmo.AddListener(EnabledTextAlert);
            GlobalEvents.OnStartAmmo.AddListener(DisabledTextAlert);
        }

        private void EnabledTextAlert()
        {
            _alertNoAmmo.enabled = true;
        }

        private void DisabledTextAlert()
        {
            _alertNoAmmo.enabled = false;
        }
    }
}