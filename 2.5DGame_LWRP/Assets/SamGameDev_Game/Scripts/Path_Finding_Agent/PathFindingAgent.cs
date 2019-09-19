using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace MyGame
{
    public class PathFindingAgent : MonoBehaviour
    {
        public bool TargetPlayableCharacter;
        public GameObject target;
        public NavMeshAgent navMeshAgent;

        // public Vector3 StartPos;
        //  public Vector3 EndPos;
        List<Coroutine> MoveRoutines = new List<Coroutine>();

        public GameObject startSphere;
        public GameObject endSphere;

        public bool StartWalk;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void GoToTarget()
        {
            navMeshAgent.enabled = true;
            startSphere.transform.parent = null;
            endSphere.transform.parent = null;
            StartWalk = false;

            navMeshAgent.isStopped = false;

            if (TargetPlayableCharacter)
            {
                target = CharacterManager.Instance.GetPlayableCharacter().gameObject;
            }
            navMeshAgent.SetDestination(target.transform.position);

            if (MoveRoutines.Count != 0)
            {
                if (MoveRoutines[0] != null)
                {
                    StopCoroutine(MoveRoutines[0]);
                }
                MoveRoutines.RemoveAt(0);
            }

            MoveRoutines.Add(StartCoroutine(_Move()));
        }

        IEnumerator _Move()
        {
            while (true)
            {
                if (navMeshAgent.isOnOffMeshLink)
                {

                    startSphere.transform.position = navMeshAgent.currentOffMeshLinkData.startPos;
                    endSphere.transform.position = navMeshAgent.currentOffMeshLinkData.endPos;

                    navMeshAgent.CompleteOffMeshLink();

                    navMeshAgent.isStopped = true;
                    StartWalk = true;
                    yield break;
                }

                Vector3 dist = transform.position - navMeshAgent.destination;
                if (Vector3.SqrMagnitude(dist) < 0.5f)
                {
                    startSphere.transform.position = navMeshAgent.destination;        // transform.position;

                    endSphere.transform.position = navMeshAgent.destination;                                                     // transform.position;
                    navMeshAgent.isStopped = true;
                    StartWalk = true;
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }
}

