using UnityEngine;

namespace Inventory
{
    public abstract class CharacterStateModifySo : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}