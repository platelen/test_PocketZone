using UnityEngine;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private PlayerDetected _playerDetected;
        [SerializeField] private float _speedMove = 5f;
        [SerializeField] private float _range = 0.5f;

        private GameObject _target;
        private bool _isAttack;
        private Vector3 _previousPosition;

        public bool IsAttack
        {
            get => _isAttack;
            set => _isAttack = value;
        }


        private void Start()
        {
            _previousPosition = transform.position;
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (_target == null)
                return;

            if (Vector3.Distance(transform.position, _target.transform.position) < _range)
            {
                _isAttack = true;
            }
            else
            {
                _isAttack = false;
            }

            if (_playerDetected.IsDetected)
                Move();
        }

        private void Move()
        {
            if (_isAttack == false)
            {
                Vector3 currentPosition = transform.position;
                Vector3 movementDirection = currentPosition - _previousPosition;

                if (movementDirection.x > 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, 0);
                }

                else if (movementDirection.x < 0)
                {
                    transform.localRotation = Quaternion.Euler(0, 180, 0);
                }

                _previousPosition = currentPosition;

                Vector3 direction = (_target.transform.position - transform.position).normalized;
                Vector3 newPosition = transform.position + direction * _speedMove * Time.deltaTime;
                transform.position = newPosition;
            }
        }
    }
}