using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class DamageDetector : MonoBehaviour
    {
        CharacterControl control;
        GeneralBodyParts DamagedPart;

        public int DamageTaken;

        private void Awake()
        {
            DamageTaken = 0;
            control = GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (AttackManger.Instance.CurrentAttacks.Count > 0)
            {
                CheckAttack();
            }
        }

        private void CheckAttack()
        {
            foreach (AttackInfo info in AttackManger.Instance.CurrentAttacks)
            {
                if (info == null)
                {
                    continue;
                }
                if (!info.isRegisterd)
                {
                    continue;
                }
                if (info.isFinsihed)
                {
                    continue;
                }
                if (info.CurrentHits >= info.MaxHits)
                {
                    continue;
                }
                if (info.Attacker == control)
                {
                    continue;
                }

                if (info.MustCollide)
                {
                    if (isCollided(info))
                    {

                        TakeDamage(info);
                    }
                }
            }
        }

        private bool isCollided(AttackInfo info)
        {
            foreach (TriggerDector trigger in control.GetAllTriggers())
            {
                foreach (Collider collider in trigger.CollidingParts)
                {
                    foreach (Attack_BodyParts part in info.AttackParts)
                    {
                        if (part == Attack_BodyParts.LEFT_HAND)
                        {
                            if (collider.gameObject == info.Attacker.Left_HandAttack)
                            {
                                DamagedPart = trigger.generalBodyParts;
                                return true;
                            }
                        }
                        else if (part == Attack_BodyParts.RIGHT_HAND)
                        {
                            if (collider.gameObject == info.Attacker.Right_HandAttack)
                            {
                                DamagedPart = trigger.generalBodyParts;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void TakeDamage(AttackInfo info)
        {
            if (DamageTaken > 0)
            {
                return;
            }

            CameraManger.Instance.ShakeCamera(0.4f);

            Debug.Log(info.Attacker.gameObject.name + " hits: " + this.gameObject.name);
            Debug.Log(this.gameObject.name + " hit: " + DamagedPart.ToString());


            control.SkinnedMeshedAnimator.runtimeAnimatorController = DeathAnimationManger.Instance.GetAnimator(DamagedPart, info);
            info.CurrentHits++;

            control.GetComponent<BoxCollider>().enabled = false;
            control.ledgeChecker.GetComponent<BoxCollider>().enabled = false;
            control.RIGIBODY.useGravity = false;

            DamageTaken++;

        }
    }

}