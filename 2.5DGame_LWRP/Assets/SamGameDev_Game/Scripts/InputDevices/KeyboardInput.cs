using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class KeyboardInput : MonoBehaviour
    {
        void Update()
        {
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                VirtualInputManger.Instance.MoveRight = true;
            }
            else
            {
                VirtualInputManger.Instance.MoveRight = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                VirtualInputManger.Instance.Running = true;
            }
            else
            {
                VirtualInputManger.Instance.Running = false;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                VirtualInputManger.Instance.MoveUp = true;
            }
            else
            {
                VirtualInputManger.Instance.MoveUp = false;
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                VirtualInputManger.Instance.MoveDown = true;
            }
            else
            {
                VirtualInputManger.Instance.MoveDown = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                VirtualInputManger.Instance.MoveLeft = true;
            }
            else
            {
                VirtualInputManger.Instance.MoveLeft = false;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                VirtualInputManger.Instance.Jump = true;
            }
            else
            {
                VirtualInputManger.Instance.Jump = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                VirtualInputManger.Instance.Attack = true;
            }
            else
            {
                VirtualInputManger.Instance.Attack = false;
            }

        }
    }
}

