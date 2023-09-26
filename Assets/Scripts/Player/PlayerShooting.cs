using Events;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Ammo player")] [SerializeField]
        private TextMeshProUGUI _textCurrentBullet;

        [SerializeField] private TextMeshProUGUI _textAllMagazinesBullet;
        [SerializeField] private int _allMagazines = 3;
        [SerializeField] private int _bulletInMagazine = 10;

        [Header("Shooting")] [SerializeField] private Transform _startBullet;
        [SerializeField] private GameObject _bullet;
        [SerializeField] private FindEnemy _findEnemy;
        [SerializeField] private float _startTimeBtwShots;

        private int _bulletsCurrent;
        private bool _isNoAmmo;
        private float _timeBtwShots;

        public void AddAllMagazine(int value)
        {
            _allMagazines += value;
        }
        
        private void Awake()
        {
            GlobalEvents.OnStartReloadGun.AddListener(Reload);
        }

        private void Start()
        {
            _isNoAmmo = false;
            _bulletsCurrent = _bulletInMagazine;
        }


        private void Update()
        {
            if (_timeBtwShots <= 0)
            {
                if (_findEnemy.IsFire && _bulletsCurrent > 0)
                {
                    Shoot();
                }
            }
            else
            {
                _timeBtwShots -= Time.deltaTime;
            }


            CheckedBulletsAndMagazine();

            _textCurrentBullet.text = _bulletsCurrent.ToString();
            _textAllMagazinesBullet.text = _allMagazines.ToString();
        }

        private void Shoot()
        {
            _bulletsCurrent--;
            GameObject bullet = Instantiate(_bullet, _startBullet.position, _startBullet.rotation);
            _timeBtwShots = _startTimeBtwShots;
        }

        private void Reload()
        {
            if (_bulletsCurrent == 0 && _isNoAmmo == false)
            {
                _bulletsCurrent = _bulletInMagazine;
                _allMagazines--;
            }
        }

        private void CheckedBulletsAndMagazine()
        {
            if (_bulletsCurrent > 0)
            {
                GlobalEvents.SendDisableButtonReload();
            }

            if (_bulletsCurrent == 0)
            {
                GlobalEvents.SendStartSendToReloadGun();
            }

            if (_allMagazines <= 0)
            {
                _isNoAmmo = true;
                GlobalEvents.SendAlertNoAmmo();
            }
            else
            {
                _isNoAmmo = false;
                GlobalEvents.SendStartAmmo();
            }
        }
    }
}