﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyGame
{
    public class PoolManger : Singleton<PoolManger>
    {
        public Dictionary<PoolObjectType, List<GameObject>> poolDictionary = new Dictionary<PoolObjectType, List<GameObject>>();

        public void SetUpDictionary()
        {
            PoolObjectType[] arr = System.Enum.GetValues(typeof(PoolObjectType)) as PoolObjectType[];

            foreach (PoolObjectType p in arr)
            {
                if (!poolDictionary.ContainsKey(p))
                {
                    poolDictionary.Add(p, new List<GameObject>());
                }
            }
        }

        public GameObject GetObject(PoolObjectType objType)
        {
            if (poolDictionary.Count == 0)
            {
                SetUpDictionary();
            }

            List<GameObject> list = poolDictionary[objType];
            GameObject obj = null;

            if (list.Count > 0)
            {
                obj = list[0];
                list.Remove(obj);
            }
            else
            {
                obj = PoolObjectLoader.InstantiatePrefab(objType).gameObject;
            }

            return obj;
        }

        public void AddObject(PoolObject obj)
        {
            List<GameObject> list = poolDictionary[obj.poolObjectType_Var];
            list.Add(obj.gameObject);
            obj.gameObject.SetActive(false);
        }
    }
}
