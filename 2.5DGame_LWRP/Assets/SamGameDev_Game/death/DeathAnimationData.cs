using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New ScriptableObject", menuName = "SamGame/Death/DeathAnimationData")]
    public class DeathAnimationData : ScriptableObject
    {
       public List<GeneralBodyParts> GeneralBodyParts = new List<GeneralBodyParts>();
       public RuntimeAnimatorController Animator;
        public bool LaunchintoAir;
       public bool IsFacingAttacker;
    }

}
