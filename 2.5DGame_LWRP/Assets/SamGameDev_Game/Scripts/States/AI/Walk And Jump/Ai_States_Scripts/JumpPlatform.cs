using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/Enemy_AI/JumpPlatform")]
    public class JumpPlatform : StatetData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            control.Jump = true;
            control.MoveUp = true;
            
           if (control.aiProgress.findingAgent.startSphere.transform.position.z 
                 <   control.aiProgress.findingAgent.endSphere.transform.position.z)
            {
                control.FaceForward(true);
            }
           else
            {
                control.FaceForward(false);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            float topDist = control.aiProgress.findingAgent.endSphere.transform.position.y
                - control.FrontSpheres[1].transform.position.y;

            float bottomDist = control.aiProgress.findingAgent.endSphere.transform.position.y
                - control.FrontSpheres[0].transform.position.y;

            if (topDist < 1.7f && bottomDist > 0.5f)
            {
                if(control.isFacingForward())
                {
                    control.MoveRight = true;
                    control.MoveLeft = false;
                }
                else
                {
                    control.MoveRight = false;
                    control.MoveLeft = true;
                }
            }

            if(bottomDist < 0.5f)
            {
                control.Jump = false;
                control.MoveUp = false;
                control.MoveRight = false;
                control.MoveLeft = false;

                animator.gameObject.SetActive(false);
                animator.gameObject.SetActive(true);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            
        }
    }
}

