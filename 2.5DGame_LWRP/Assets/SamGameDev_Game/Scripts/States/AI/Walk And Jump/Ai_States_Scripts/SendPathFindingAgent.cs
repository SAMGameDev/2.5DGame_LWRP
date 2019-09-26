using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    public enum EnemyTransitions
    {
        StartWalking,
        JumpPlatform,
        FallPlatform,
        StartRunning,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/Enemy_AI/SendPathFindingAgent")]
    public class SendPathFindingAgent : StatetData
    {

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.aiProgress.findingAgent == null)
            {
                GameObject pathfindingAgent_Prefab = Instantiate(Resources.Load("PathFindingAgent", typeof(GameObject)) as GameObject);

                control.aiProgress.findingAgent = pathfindingAgent_Prefab.GetComponent<PathFindingAgent>();
            }

            control.aiProgress.findingAgent.GetComponent<NavMeshAgent>().enabled = false;
            control.aiProgress.findingAgent.transform.position = control.transform.position;
            control.aiProgress.findingAgent.GoToTarget();
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

           if (control.aiProgress.findingAgent.StartWalk)
            {
                animator.SetBool(EnemyTransitions.StartWalking.ToString(), true);
                animator.SetBool(EnemyTransitions.StartRunning.ToString(), true);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(EnemyTransitions.StartWalking.ToString(), false);
            animator.SetBool(EnemyTransitions.StartRunning.ToString(), false);
        }
    }
}

