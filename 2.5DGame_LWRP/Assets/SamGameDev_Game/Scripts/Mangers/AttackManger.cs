using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class AttackManger : Singleton<AttackManger>
    {
        public List<AttackInfo> CurrentAttacks = new List<AttackInfo>();
    }

}