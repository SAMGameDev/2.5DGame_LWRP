using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/GroundDetector")]
    public class GroundDetector : StatetData
    {
        [Range(0.01f, 1f)]
        public float CheckTime;
        public float Distance;
        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (stateInfo.normalizedTime >= CheckTime)
            {
                if (isGrounded(control))
                {
                    animator.SetBool(TransitionParameter.Grounded.ToString(), true);
                }
                else
                {
                    animator.SetBool(TransitionParameter.Grounded.ToString(), false);
                }
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        bool isGrounded(CharacterControl control)
        {
            if (control.RIGIBODY.velocity.y > -0.001 && control.RIGIBODY.velocity.y <= 0f)
            {
                return true;
            }

            if (control.RIGIBODY.velocity.y < 0f)
            {
                foreach (GameObject o in control.BottomSpheres)
                {
                    Debug.DrawRay(o.transform.position, -Vector3.up * 0.7f, Color.green);
                    RaycastHit hit;

                    if (Physics.Raycast(o.transform.position, -Vector3.up, out hit, Distance))
                    {
                        if (!control.RagdollParts.Contains(hit.collider) 
                            && !Ledge.isLedge(hit.collider.gameObject)
                            && !Ledge.isLedgeChecker(hit.collider.gameObject)
                            && !Ledge.isCharacter(hit.collider.gameObject))
                        {

                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

}