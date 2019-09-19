using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/CheckRunning")]
    public class CheckRunning : StatetData
    {
        public bool MustRequireMovement;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Running)
            {
                if (MustRequireMovement)
                {
                    if (control.MoveRight || control.MoveLeft)
                    {
                        animator.SetBool(TransitionParameter.Running.ToString(), true);
                    }
                    else
                    {
                        animator.SetBool(TransitionParameter.Running.ToString(), false);
                    }
                }
                else
                {
                    animator.SetBool(TransitionParameter.Running.ToString(), true);
                }

            }
            else
            {
                animator.SetBool(TransitionParameter.Running.ToString(), false);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }

}

