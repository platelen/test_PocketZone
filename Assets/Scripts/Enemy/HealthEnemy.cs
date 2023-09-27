using UnityEngine;

namespace Enemy
{
    public class HealthEnemy : MonoBehaviour
    {
        [SerializeField] private HealthBar.HealthBar _healthBar;
        [SerializeField] private HealthData _healthData;
        [SerializeField] private int _currentHealth;

        private void Start()
        {
            _currentHealth = _healthData.MaxHealth;
            _healthBar.SetMaxHealth(_healthData.MaxHealth);
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