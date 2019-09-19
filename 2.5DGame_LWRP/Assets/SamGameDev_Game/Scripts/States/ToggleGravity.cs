using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/ToggleGravity")]
    public class ToggleGravity : StatetData
    {
        public bool on;
        public bool onStart;
        public bool OnEnd;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if(onStart)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleGrav(control);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {


        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (OnEnd)
            {
                CharacterControl control = characterState.GetCharacterControl(animator);
                ToggleGrav(control);
            }
        }

        private void ToggleGrav(CharacterControl control)
        {
           // control.SkinnedMeshedAnimator
            control.RIGIBODY.velocity = Vector3.zero;
            control.RIGIBODY.useGravity = on;
        }
    }
}

