using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public enum AI_Type
    {
        WALK_AND_jUMP,
        RUNNING,
    }

    public class AIController : MonoBehaviour
    {
        public List<AISubSet> AIList = new List<AISubSet>();
        public AI_Type InitialAI;

        public void Awake()
        {
            AISubSet[] arr = this.gameObject.GetComponentsInChildren<AISubSet>();

            foreach (AISubSet s in arr)
            {
                if (!AIList.Contains(s))
                {
                    AIList.Add(s);
                    s.gameObject.SetActive(false);
                }
            }
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            TriggerAI(InitialAI);
        }

        public void TriggerAI(AI_Type aiType)
        {
            AISubSet next = null;

            foreach (AISubSet s in AIList)
            {
                s.gameObject.SetActive(false);

                if (s.aI_Type == aiType)
                {
                    next = s;
                }
            }

            next.gameObject.SetActive(true);
        }
    }

}
