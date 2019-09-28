using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/Enemy_AI/AITransitionCondition")]
    public class AITransitionCondition : StatetData
    {

        public enum AI_TransitionType
        {
            RUN_TO_WALK,
            WALK_TO_RUN,
        }

        public AI_TransitionType aITransition;
        public AI_Type nextAI;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (TransitionTONextAI(control) == true)
            {
                control.AiController.TriggerAI(nextAI);
            }
        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool TransitionTONextAI(CharacterControl characterControl)
        {
            if (aITransition == AI_TransitionType.RUN_TO_WALK)
            {
                Vector3 dist = characterControl.aiProgress.findingAgent.startSphere.transform.position - characterControl.transform.position;

                if(Vector3.SqrMagnitude(dist) < 2f)
                {
                    return true;
                }
            }
            else if(aITransition == AI_TransitionType.WALK_TO_RUN)
            {
                Vector3 dist = characterControl.aiProgress.findingAgent.startSphere.transform.position - characterControl.transform.position;

                if (Vector3.SqrMagnitude(dist) > 2f)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

