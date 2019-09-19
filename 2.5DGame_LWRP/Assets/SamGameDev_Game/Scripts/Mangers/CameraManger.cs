using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class CameraManger : Singleton<CameraManger>
    {
        private Coroutine routine;
        private CameraController cameraController;

        public CameraController Cam_Controller
        {
            get
            {
                if (cameraController == null)
                {
                    cameraController = GameObject.FindObjectOfType<CameraController>();
                }
                return cameraController;
            }
        }

       
        IEnumerator CamShake(float sec)
        {
            Cam_Controller.TriggerCamera(CameraTrigger.Shake);
            yield return new WaitForSeconds(sec);
            Cam_Controller.TriggerCamera(CameraTrigger.Default);
        }
        public void ShakeCamera(float sec)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
            }

            routine = StartCoroutine(CamShake(sec));
        }
    }

}
