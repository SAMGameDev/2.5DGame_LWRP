using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class DeathAnimationManger : Singleton<DeathAnimationManger>
    {
        DeathAnimationLoader deathAnimationLoader;
        List<RuntimeAnimatorController> candidates = new List<RuntimeAnimatorController>();

        void SetUpDeathAnimatorLoader()
        {
            if (deathAnimationLoader == null)
            {
                GameObject obj = Instantiate(Resources.Load("DeathAnimationLoader", typeof(GameObject)) as GameObject);
                DeathAnimationLoader loader = obj.GetComponent<DeathAnimationLoader>();

                deathAnimationLoader = loader;
            }
        }

        public RuntimeAnimatorController GetAnimator(GeneralBodyParts generalBodyParts, AttackInfo info)
        {
            SetUpDeathAnimatorLoader();
            candidates.Clear();

            foreach (DeathAnimationData data in deathAnimationLoader.deathAnimationDataList)
            {
                if (info.LaunchintoAir)
                {
                   if(data.LaunchintoAir)
                    {
                        candidates.Add(data.Animator);
                    }
                }
                else
                {
                    foreach (GeneralBodyParts parts in data.GeneralBodyParts)
                    {
                        if (parts == generalBodyParts)
                        {
                            candidates.Add(data.Animator);
                            break;
                        }
                    }
                }


            }
            return candidates[Random.Range(0, candidates.Count)];
        }
    }
}

