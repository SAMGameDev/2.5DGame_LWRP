﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class CameraState : StateMachineBehaviour
    {
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            CameraTrigger[] arr = System.Enum.GetValues(typeof(CameraTrigger)) as CameraTrigger[];

            foreach(CameraTrigger t in arr)
            {
                CameraManger.Instance.Cam_Controller.ANIMATOR.ResetTrigger(t.ToString());
            }
        }
    }
}

