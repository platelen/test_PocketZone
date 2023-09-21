using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [SerializeField] private Transform _startBullet;
        [SerializeField] private GameObject _bullet;

        private void Update()
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            GameObject bullet = Instantiate(_bullet, _startBullet.position, _startBullet.rotation);
        }
    }
}