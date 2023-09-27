using Player;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu]
    public class CharacterStateAmmoModify : CharacterStateModifySo
    {
        public override void AffectCharacter(GameObject character, float val)
        {
            PlayerShooting playerShooting = character.GetComponent<PlayerShooting>();
            if (playerShooting != null)
            {
                playerShooting.AddAllMagazine((int)val);
            }
        }
    }
}