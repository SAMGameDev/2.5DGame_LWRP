using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class Ledge : MonoBehaviour
    {
        public Vector3 offset;
        public Vector3 EndPostion;

        public static bool isLedge(GameObject obj)
        {
            if (obj.GetComponent<Ledge>() == null)
            {
                return false;
            }

            return true;
        }

        public static bool isLedgeChecker(GameObject obj)
        {
            if (obj.GetComponent<LedgeChecker>() == null)
            {
                return false;
            }
            return true;
        }

        public static bool isCharacter(GameObject obj)
        {
            if (obj.transform.root.GetComponent<CharacterControl>() == null)
            {
                return false;
            }
            return true;
        }
    }
}
