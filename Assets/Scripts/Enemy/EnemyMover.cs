using UnityEngine;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speedMove = 5f;

        private GameObject _target;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            //События детектеда и двигаемся к игроку.  
            Move();
        }

        private void Move()
        {
            if (transform.position.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

            if (transform.position.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }

            Vector3 direction = (_target.transform.position - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * _speedMove * Time.deltaTime;
            transform.position = newPosition;
        }
    }
}