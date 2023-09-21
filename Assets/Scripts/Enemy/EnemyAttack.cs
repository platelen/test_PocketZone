using System.Collections;
using Player;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private EnemyMover _enemyMover;
        [SerializeField] private int _damage;
        [SerializeField] private float _damageInterval = 2;
        [SerializeField] private Transform _attackPos;
        [SerializeField] private LayerMask _playerMask;
        [SerializeField] private float _radiusAttack;

        private float _timeBtwShots;
        private bool _isTrigger;

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && !_enemyMover.IsAttack && !_isTrigger)
            {
                _isTrigger = true;
                StartCoroutine(AttackEnum());
            }
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackPos.position, _radiusAttack);
        }

        private void OnAttack()
        {
            Collider2D[] playerColliders = Physics2D.OverlapCircleAll(_attackPos.position, _radiusAttack, _playerMask);
            for (int i = 0; i < playerColliders.Length; i++)
            {
                //GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                //Destroy(effect);
                playerColliders[i].GetComponent<HealthPlayer>().TakeDamage(_damage);
            }
        }

        private IEnumerator AttackEnum()
        {
            while (_isTrigger)
            {
                OnAttack();
                yield return new WaitForSeconds(_damageInterval);
            }
        }
    }
}