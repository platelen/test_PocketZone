using UnityEngine;

namespace Bullet
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}