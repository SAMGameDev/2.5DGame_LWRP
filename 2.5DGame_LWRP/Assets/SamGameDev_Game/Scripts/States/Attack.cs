using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public enum Attack_BodyParts
    {
        LEFT_HAND,
        RIGHT_HAND,
    }

    [CreateAssetMenu(fileName = "New State", menuName = "SamGame/AbilityData/Attack")]
    public class Attack : StatetData
    {
        public float StartAttackTime;
        public float EndAttackTime;
        //  public List<string> ColliderNames = new List<string>();
        public List<Attack_BodyParts> attack_BodyParts = new List<Attack_BodyParts>();
        public bool LaunchintoAir;
        public bool MustCollide;
        public bool MustFaceAttacker;
        public float LethalRange;
        public int MaxHits;

        private List<AttackInfo> FinishedAttacks = new List<AttackInfo>();

        public override void OnEnter(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            animator.SetBool(TransitionParameter.Attack.ToString(), false);

            GameObject obj = PoolManger.Instance.GetObject(PoolObjectType.ATTACKINFO);
            AttackInfo info = obj.GetComponent<AttackInfo>();

            obj.SetActive(true);
            info.ResetInfo(this, characterState.GetCharacterControl(animator));

            if (!AttackManger.Instance.CurrentAttacks.Contains(info))
            {
                AttackManger.Instance.CurrentAttacks.Add(info);
            }
        }

        public override void UpdateAbility(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            RegisterAttack(characterState, animator, stateInfo);
            DeRegisterAttack(characterState, animator, stateInfo);
            CheckCombo(characterState, animator, stateInfo);
        }

        public void RegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (StartAttackTime <= stateInfo.normalizedTime && EndAttackTime > stateInfo.normalizedTime)
            {
                foreach (AttackInfo info in AttackManger.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }

                    if (!info.isRegisterd && info.AttackAbility == this)
                    {
                        info.Register(this);
                    }
                }
            }
        }

        public void DeRegisterAttack(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= EndAttackTime)
            {
                foreach (AttackInfo info in AttackManger.Instance.CurrentAttacks)
                {
                    if (info == null)
                    {
                        continue;
                    }
                    if (info.AttackAbility == this && !info.isFinsihed)
                    {
                        info.isFinsihed = true;
                        info.GetComponent<PoolObject>().TurnOff();
                        //Destroy(info.gameObject);
                    }
                }
            }
        }

        public void CheckCombo(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            if (stateInfo.normalizedTime >= StartAttackTime + ((EndAttackTime - StartAttackTime) / 3f))
            {
                if (stateInfo.normalizedTime < EndAttackTime + ((EndAttackTime - StartAttackTime) / 2f))
                {
                    CharacterControl control = characterState.GetCharacterControl(animator);
                    if (control.Attack)
                    {
                        Debug.Log("upper cut is done");
                        animator.SetBool(TransitionParameter.Attack.ToString(), true);
                    }
                }
            }
        }


        public override void OnExit(CharacterState characterState, Animator animator, AnimatorStateInfo stateInfo)
        {
            ClearAttack();
        }

        public void ClearAttack()
        {
            FinishedAttacks.Clear();

            foreach (AttackInfo info in AttackManger.Instance.CurrentAttacks)
            {
                if (info == null || info.AttackAbility == this /*info.isFinsihed*/)
                {
                    FinishedAttacks.Add(info);
                }
            }

            foreach (AttackInfo info in FinishedAttacks)
            {
                if (AttackManger.Instance.CurrentAttacks.Contains(info))
                {
                    AttackManger.Instance.CurrentAttacks.Remove(info);
                }
            }
        }

    }

}
