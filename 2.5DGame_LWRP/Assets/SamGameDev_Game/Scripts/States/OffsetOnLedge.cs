using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/OffsetOnLedge")]
    public class OffsetOnLedge : StatetData
    {
       

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            GameObject anim = control.SkinnedMeshedAnimator.gameObject;
            anim.transform.parent = control.ledgeChecker.GrabbedLedge.transform;
            anim.transform.localPosition = control.ledgeChecker.GrabbedLedge.offset;

            control.RIGIBODY.velocity = Vector3.zero;
            
           // control.RIGIBODY.useGravity = false;
            
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}

