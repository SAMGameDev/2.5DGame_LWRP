using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/Enemy_AI/StartWalking")]
    public class StartWalking : StatetData
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


            // Debug.Log(dir);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
            Vector3 dist = control.aiProgress.findingAgent.startSphere.transform.position - control.transform.position;

            //Jumping, if startsphere is on lower platform than endphere

            if (control.aiProgress.findingAgent.startSphere.transform.position.y
                    < control.aiProgress.findingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dist) < 0.01f)
                {
                    control.MoveLeft = false;
                    control.MoveRight = false;

                    animator.SetBool(EnemyTransitions.JumpPlatform.ToString(), true);
                }
            }

            //Fall, if endsphere is on lower platform than startsphere

            if (control.aiProgress.findingAgent.startSphere.transform.position.y
                   > control.aiProgress.findingAgent.endSphere.transform.position.y)
            {
                animator.SetBool(EnemyTransitions.FallPlatform.ToString(), true);
            }

            //Go straignt if Endsphere and startphare are on same platform 

            if (control.aiProgress.findingAgent.startSphere.transform.position.y
                  == control.aiProgress.findingAgent.endSphere.transform.position.y)
            {
                if (Vector3.SqrMagnitude(dist) < 0.7f)
                {
                    control.MoveLeft = false;
                    control.MoveRight = false;

                    Vector3 playerDist = control.transform.position - CharacterManager.Instance.GetPlayableCharacter().transform.position;
                    if (playerDist.sqrMagnitude > 1f)
                    {
                        animator.gameObject.SetActive(false);
                        animator.gameObject.SetActive(true);
                    }
                    //temp attack
                  /*  else
                    {
                        if(CharacterManager.Instance.GetPlayableCharacter().damageDetector.DamageTaken == 0)
                        {
                            if(control.isFacingForward())
                            {
                                control.MoveRight = false;
                                control.MoveLeft = false;
                                control.Attack = true;
                            }
                            else
                            {
                                control.MoveRight = false;
                                control.MoveLeft = false;
                                control.Attack = true;
                            }
                        }
                        else
                        {
                            control.Attack = false;
                            control.MoveLeft = false;
                            control.MoveRight = false;
                        }

                    }*/
                  
                }
            }

        }


        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(EnemyTransitions.JumpPlatform.ToString(), false);
            animator.SetBool(EnemyTransitions.FallPlatform.ToString(), false);
        }
    }
}

