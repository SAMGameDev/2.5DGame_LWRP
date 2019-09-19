using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/MoveForward")]

    public class MoveForward : StatetData
    {
        //for punch
        public bool Constant;

        //not for punch
        public bool LockDirection;
        public bool AllowEarlyTurn;
        public AnimationCurve speedGraph;
        public float speed;
        public float BlockDistance;

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (AllowEarlyTurn && !control.animationProgress.thisAllowEarlyTurn)
            {
                if (control.MoveLeft)
                {
                    control.FaceForward(false);
                }
                if (control.MoveRight)
                {
                    control.FaceForward(true);
                }
            }

            control.animationProgress.thisAllowEarlyTurn = false;
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            CharacterControl control = characterState.GetCharacterControl(animator);

            if (control.Jump)
            {
                animator.SetBool(TransitionParameter.Jump.ToString(), true);
            }

            //punch animation
            if (Constant)
            {
                ConstantMove(control, animator, stateInfo);
            }
            else
            {
                ControlledMove(control, animator, stateInfo);
            }

        }

        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {

        }

        private void ConstantMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (!CheckFront(control))
            {
                // control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
            }
        }

        private void ControlledMove(CharacterControl control, Animator animator, AnimatorStateInfo stateInfo)
        {

            if (control.MoveLeft && control.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            if (!control.MoveLeft && !control.MoveRight)
            {
                animator.SetBool(TransitionParameter.Move.ToString(), false);
                return;
            }
            if (control.MoveRight)
            {

                if (!CheckFront(control))
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                }
            }
            if (control.MoveLeft)
            {

                if (!CheckFront(control))
                {
                    control.MoveForward(speed, speedGraph.Evaluate(stateInfo.normalizedTime));
                    //control.transform.Translate(Vector3.forward * speed * speedGraph.Evaluate(stateInfo.normalizedTime) * Time.deltaTime);
                }
            }

            CheckTurn(control);
        }

        private void CheckTurn(CharacterControl control)
        {
            if (!LockDirection)
            {
                if (control.MoveRight)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                }

                if (control.MoveLeft)
                {
                    control.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                }
            }

        }

        bool CheckFront(CharacterControl control)
        {

            foreach (GameObject o in control.FrontSpheres)
            {
                Debug.DrawRay(o.transform.position, control.transform.forward * 0.3f, Color.red);
                RaycastHit hit;

                if (Physics.Raycast(o.transform.position, control.transform.forward, out hit, BlockDistance))
                {
                    if (!control.RagdollParts.Contains(hit.collider))
                    {
                        if (!isBodyPart(hit.collider)
                            && !Ledge.isLedge(hit.collider.gameObject)
                            && !Ledge.isLedgeChecker(hit.collider.gameObject))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        bool isBodyPart(Collider col)
        {
            CharacterControl control = col.transform.root.GetComponent<CharacterControl>();

            if (control == null)
            {
                return false;
            }

            if (control.gameObject == col.gameObject)
            {
                return false;
            }

            if (control.RagdollParts.Contains(col))
            {
                return true;
            }
            return false;
        }


    }
}

