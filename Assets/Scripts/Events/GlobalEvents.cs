using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class GlobalEvents : MonoBehaviour
    {
        public static readonly UnityEvent OnStartAlertNoAmmo = new UnityEvent();
        public static readonly UnityEvent OnStartShowInventory = new UnityEvent();
        public static readonly UnityEvent OnStartResetDraggetItem = new UnityEvent();
        public static readonly UnityEvent OnStartAmmo = new UnityEvent();
        public static readonly UnityEvent OnStartSendToReloadGun = new UnityEvent();
        public static readonly UnityEvent OnStartReloadGun = new UnityEvent();
        public static readonly UnityEvent OnStartDisableButtonReload = new UnityEvent();

        public static void SendAlertNoAmmo()
        {
            OnStartAlertNoAmmo.Invoke();
        }
        public static void SendStartResetDraggedItem()
        {
            OnStartResetDraggetItem.Invoke();
        }

        public static void SendShowInventory()
        {
            OnStartShowInventory.Invoke();
        }

        public static void SendStartAmmo()
        {
            OnStartAmmo.Invoke();
        }

        public static void SendStartSendToReloadGun()
        {
            OnStartSendToReloadGun.Invoke();
        }

        public static void SendStartReloadGun()
        {
            OnStartReloadGun.Invoke();
        }

        public static void SendDisableButtonReload()
        {
            OnStartDisableButtonReload.Invoke();
        }
    }
}