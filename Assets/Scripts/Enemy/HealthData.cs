using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class HealthData:ScriptableObject
    {
        [SerializeField] private int _maxHealth = 100;

        public int MaxHealth => _maxHealth;
    }
}