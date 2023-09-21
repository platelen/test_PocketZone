using UnityEngine;

namespace Player
{
    public class FindEnemy : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        [SerializeField] private GameObject _rayEyeObject;
        [SerializeField] private float _toEnemyDistance;
        [SerializeField] private LayerMask _layerMask;

        private bool _IsFire;

        public bool IsFire => _IsFire;

        private void Update()
        {
            DetectedEnemy();
        }

        private void DetectedEnemy()
        {
            Vector2 rayDirection = Vector2.zero;

            if (_player.rotation.y == 0)
            {
                rayDirection = Vector2.right;
            }

            if (_player.rotation.y < 0)
            {
                rayDirection = Vector2.left;
            }

            RaycastHit2D hit = Physics2D.Raycast(_rayEyeObject.transform.position, rayDirection, _toEnemyDistance,
                _layerMask);

            if (hit.collider != null)
            {
                _IsFire = true;
                Debug.DrawRay(_rayEyeObject.transform.position, rayDirection * hit.distance, Color.red);
            }
            else
            {
                _IsFire = false;
                Debug.DrawRay(_rayEyeObject.transform.position, rayDirection * _toEnemyDistance, Color.green);
            }
        }
    }
}