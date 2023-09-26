using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class AmmoItemsSo : ItemSo, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> _modifierDats = new List<ModifierData>();

        public string ActionName => "Consume";
        public AudioClip ActionSFX { get; set; }

        public bool PerformAction(GameObject character)
        {
            foreach (ModifierData data in _modifierDats)
            {
                data._stateModifier.AffectCharacter(character, data._value);
            }

            return true;
        }
    }

    public interface IDestroyableItem
    {
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip ActionSFX { get; }
        bool PerformAction(GameObject character);
    }


    [Serializable]
    public class ModifierData
    {
        public CharacterStateModifySo _stateModifier;
        public float _value;
    }
}