using UnityEngine;

namespace Inventory
{
    public class ItemParameterSo : ScriptableObject
    {
        [CreateAssetMenu]
        public class ItemParameterSO : ScriptableObject
        {
            [field: SerializeField] public string ParameterName { get; private set; }
        }
    }
}