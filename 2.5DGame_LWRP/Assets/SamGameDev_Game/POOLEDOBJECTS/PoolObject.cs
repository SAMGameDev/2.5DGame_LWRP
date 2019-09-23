using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class PoolObject : MonoBehaviour
    {
        public PoolObjectType poolObjectType_Var;
        public float ScheduledOffTime;
        private Coroutine offRoutine;

        private void OnEnable()
        {
            if (offRoutine != null)
            {
                StopCoroutine(offRoutine);
            }
            if (ScheduledOffTime > 0)
            {
                offRoutine = StartCoroutine(_ScheduledOff());
            }
        }
        public void TurnOff()
        {
            PoolManger.Instance.AddObject(this);
        }

        IEnumerator _ScheduledOff()
        {
            yield return new WaitForSeconds(ScheduledOffTime);

            if (!PoolManger.Instance.poolDictionary[poolObjectType_Var].Contains(this.gameObject))
            {

                TurnOff();
            }
        }
    }
}

