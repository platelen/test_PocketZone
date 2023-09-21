using UnityEngine;

namespace Enemy
{
    public class PlayerDetected : MonoBehaviour
    {
        [SerializeField] private float _rangeDetected;
        private GameObject _target;
        private bool _isDetected;

        public bool IsDetected => _isDetected;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player");
        }

        private void Update()
        {
            if (_target == null)
                return;

            if (Vector3.Distance(transform.position, _target.transform.position) < _rangeDetected)
            {
                Debug.Log("Player detected");
                _isDetected = true;
            }
            else
            {
                _isDetected = false;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(gameObject.transform.position, _rangeDetected);
        }
    }
}