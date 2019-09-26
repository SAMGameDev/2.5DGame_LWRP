using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/Enemy_AI/StartRunning")]
    public class StartRunning : StatetData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dir = control.aiProgress.findingAgent.startSphere.transform.position - control.transform.position;

            if (dir.z > 0f)
            {
                control.MoveRight = true;
                control.MoveLeft = false;
            }
            else
            {
                control.MoveLeft = true;
                control.MoveRight = false;
            }

            control.Running = true;

            // Debug.Log(dir);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            Vector3 dist = control.aiProgress.findingAgent.startSphere.transform.position - control.transform.position;

            if (Vector3.SqrMagnitude(dist) < 2f)
            {
                control.MoveLeft = false;
                control.MoveRight = false;
                control.Running = false;
            }
        }


        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }
    }
}

