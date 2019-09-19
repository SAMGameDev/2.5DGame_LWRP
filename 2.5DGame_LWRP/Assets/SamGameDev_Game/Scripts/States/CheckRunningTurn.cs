using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/CheckRunningTurn")]
    public class CheckRunningTurn : StatetData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.isFacingForward())
            {
                if (control.MoveLeft)
                {
                    animator.SetBool(TransitionParameter.RunningTurn180.ToString(), true);
                }
            }

            if (!control.isFacingForward())
            {
                if (control.MoveRight)
                {
                    animator.SetBool(TransitionParameter.RunningTurn180.ToString(), true);
                }
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.RunningTurn180.ToString(), false);
        }
    }
}

