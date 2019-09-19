using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{

    public class LayerChanger : MonoBehaviour
    {
        public SGD_Layers layertype;
        public bool ChangeAllChildren;

        public void ChangeLayer(Dictionary<string, int> layerDic)
        {
            if (!ChangeAllChildren)
            {
                Debug.Log(gameObject.name + "Changed layer" + layertype.ToString());
                gameObject.layer = layerDic[layertype.ToString()];
            }
            else
            {
                Transform[] arr = this.gameObject.GetComponentsInChildren<Transform>();

                foreach (Transform o in arr)
                {
                    Debug.Log(o.gameObject.name + "Changed layer" + layertype.ToString());
                    o.gameObject.layer = layerDic[layertype.ToString()];
                }
            }
        }
    }

}