using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/TelePortOnLedge")]
    public class TelePortOnLedge : StatetData
    {


        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
          
        }
        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {


        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = CharacterManager.Instance.GetCharacter(animator);

            Vector3 EndPos = control.ledgeChecker.GrabbedLedge.transform.position + control.ledgeChecker.GrabbedLedge.EndPostion;

            control.transform.position = EndPos;
            control.SkinnedMeshedAnimator.transform.position = EndPos;
            control.SkinnedMeshedAnimator.transform.parent = control.transform;

        }
    }
}

