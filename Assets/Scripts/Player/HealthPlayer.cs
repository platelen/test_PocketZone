using UnityEngine;

namespace Player
{
    public class HealthPlayer : MonoBehaviour
    {
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private int _maxHealth = 100;
        [SerializeField] private int _currentHealth;

        private void Start()
        {
            _currentHealth = _maxHealth;
            _healthBar.SetMaxHealth(_maxHealth);
        }

        private void Update()
        {
            if (_currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _healthBar.SetHealthBar(_currentHealth);
        }
    }
}