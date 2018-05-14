using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using SpaceLine;
using System;

public class PoolHelper : MonoBehaviour {
    public LinesRender target;
    private UnityEngine.Internal.ObjectManager objectManager;
    private void Awake()
    {
        objectManager = UnityEngine.Internal.ObjectManager.GetInstance();
        target.GetPoolObject = GetPoolObject;
        target.SavePoolObject = SavePoolObject;
    }

    private void SavePoolObject(GameObject instence)
    {
        objectManager.SavePoolObject(instence, true);
    }

    private GameObject GetPoolObject(Transform parent,GameObject prefab)
    {
        return objectManager.GetPoolObject(prefab, parent, false);
    }
}
