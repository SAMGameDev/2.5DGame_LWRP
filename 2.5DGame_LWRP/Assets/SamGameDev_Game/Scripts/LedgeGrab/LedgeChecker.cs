using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class LedgeChecker : MonoBehaviour
    {
        public bool isGrabbingLedge;
        public Ledge GrabbedLedge;
        Ledge Checkledge = null;
        private void OnTriggerEnter(Collider col)
        {
            Checkledge = col.gameObject.GetComponent<Ledge>();
            if(Checkledge != null)
            {
                isGrabbingLedge = true;
                GrabbedLedge = Checkledge;
            }
        }

        private void OnTriggerExit(Collider col)
        {
            Checkledge = col.gameObject.GetComponent<Ledge>();
            if (Checkledge != null)
            {
                isGrabbingLedge = false;
               // GrabbedLedge = null;
            }
        }
    }
}
