using Enemy;
using UnityEngine;

namespace Bullet
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private int _damage = 10;
        [SerializeField] private float _speed;

        private Rigidbody2D _rb;
        private readonly float _delayDestroy = 3f;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = transform.right * _speed;
        }

        private void Update()
        {
            Destroy(gameObject, _delayDestroy);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<HealthEnemy>().TakeDamage(_damage);
                GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (col.gameObject.CompareTag("Decor"))
            {
                GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}