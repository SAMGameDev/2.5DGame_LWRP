﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class CharacterState : StateMachineBehaviour
    {

        public List<StatetData> ListOfAbilityData = new List<StatetData>();

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach(StatetData d in ListOfAbilityData)
            {
                d.OnEnter(this, animator, stateInfo);
            }
        }

        public void UpdateAll(CharacterState characterState,  Animator animator, AnimatorStateInfo stateInfo)
        {
            foreach (StatetData d in ListOfAbilityData)
            {
                d.UpdateAbility(characterState, animator, stateInfo);
            }
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateAll(this, animator, stateInfo);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            foreach (StatetData d in ListOfAbilityData)
            {
                d.OnExit(this, animator, stateInfo);
            }
        }

        private CharacterControl characterControl;
        public CharacterControl GetCharacterControl(Animator animator)
        {
            if (characterControl == null)
            {
                characterControl = animator.transform.root.GetComponent<CharacterControl>();
                //characterControl = animator.GetComponentInParent<CharacterControl>();
            }
            return characterControl;
        }
    }
}

