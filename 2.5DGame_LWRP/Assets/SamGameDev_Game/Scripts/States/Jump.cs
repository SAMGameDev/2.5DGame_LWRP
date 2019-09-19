using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/Jump")]
    public class Jump : StatetData
    {
        [Range(0f, 100f)]
        public float JumpTiming;
        public float JumpForce;
        //public AnimationCurve Gravity;
        public AnimationCurve Pull;
        // private bool isJumped;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (JumpTiming == 0f)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);

                characterState.GetCharacterControl(animator).RIGIBODY.AddForce(Vector3.up * JumpForce);
                control.animationProgress.Jumped = true;
            }
            animator.SetBool(TransitionParameter.Grounded.ToString(), false);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            // control.gravityMultiplier = Gravity.Evaluate(stateInfo.normalizedTime);
            control.pullMultiplier = Pull.Evaluate(stateInfo.normalizedTime);

            if (!control.animationProgress.Jumped && stateInfo.normalizedTime >= JumpTiming)
            {
                characterState.GetCharacterControl(animator).RIGIBODY.AddForce(Vector3.up * JumpForce);
                control.animationProgress.Jumped = true;
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            control.pullMultiplier = 0f;
            control.animationProgress.Jumped = false;

        }
    }
}

