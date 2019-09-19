using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class ManualInput : MonoBehaviour
    {
        private CharacterControl characterControl;

        private void Awake()
        {
            characterControl = this.gameObject.GetComponent<CharacterControl>();
        }

        private void Update()
        {
            if (VirtualInputManger.Instance.Running)
            {
                characterControl.Running = true;
            }
            else
            {
                characterControl.Running = false;
            }
            if (VirtualInputManger.Instance.MoveUp)
            {
                characterControl.MoveUp = true;
            }
            else
            {
                characterControl.MoveUp = false;
            }
            if (VirtualInputManger.Instance.MoveDown)
            {
                characterControl.MoveDown = true;
            }
            else
            {
                characterControl.MoveDown = false;
            }
            if (VirtualInputManger.Instance.MoveRight)
            {
                characterControl.MoveRight = true;
            }
            else
            {
                characterControl.MoveRight = false;
            }
            if (VirtualInputManger.Instance.MoveLeft)
            {
                characterControl.MoveLeft = true;
            }
            else
            {
                characterControl.MoveLeft = false;
            }
            if (VirtualInputManger.Instance.Jump)
            {
                characterControl.Jump = true;
            }
            else
            {
                characterControl.Jump = false;
            }
            if (VirtualInputManger.Instance.Attack)
            {
                characterControl.Attack = true;
            }
            else
            {
                characterControl.Attack = false;
            }

        }
    }
}

