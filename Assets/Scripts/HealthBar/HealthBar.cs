using UnityEngine;
using UnityEngine.UI;

namespace HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _healthBar;

        public void SetMaxHealth(int health)
        {
            _healthBar.maxValue = health;
            _healthBar.value = health;
        }

        public void SetHealthBar(int health)
        {
            _healthBar.value = health;
        }
    }
}