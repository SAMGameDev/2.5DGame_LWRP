using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class VirtualInputManger : Singleton<VirtualInputManger>
    {
        public bool MoveUp;
        public bool MoveDown;
        public bool MoveRight;
        public bool MoveLeft;
        public bool Jump;
        public bool Attack;
        public bool Running;

    }
}

