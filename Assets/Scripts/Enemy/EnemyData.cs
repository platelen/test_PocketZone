using UnityEngine;

namespace Enemy
{
    [CreateAssetMenu]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _damageInterval = 2;

        public int Damage => _damage;

        public float DamageInterval => _damageInterval;
    }
}
