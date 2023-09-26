using System.Collections;
using Inventory.Model;
using UnityEngine;

namespace Inventory.PickUpSystem
{
    public class Item : MonoBehaviour
    {
        [field: SerializeField] public ItemSo InventoryItem { get; set; }
        [field: SerializeField] public int Quantity { get; set; } = 1;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _duration = 0.3f;

        private void Start()
        {
            GetComponent<SpriteRenderer>().sprite = InventoryItem.ImageItem;
        }

        public void DestroyItem()
        {
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine(AnimateItemPickup());
        }

        private IEnumerator AnimateItemPickup()
        {
            _audioSource.Play();
            Vector3 startScale = transform.localScale;
            Vector3 endScale = Vector3.zero;
            float currentTime = 0;
            while (currentTime < _duration)
            {
                currentTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / _duration);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}