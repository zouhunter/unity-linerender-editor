using System;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngineInternal;
using UnityEngine;
namespace UnityEngine.Internal
{
    public abstract class ManagerTemp<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance = default(T);
        private static object lockHelper = new object();
        private static bool isQuit = false;
        public static T Instance
        {
            get { return GetInstance(); }
        }
        public static T GetInstance()
        {
            if (instance == null)
            {
                lock (lockHelper)
                {
                    if (instance == null && !isQuit)
                    {
                        GameObject go = new GameObject(typeof(T).ToString());
                        instance = go.AddComponent<T>();
                        Debug.Log("[启动]" + go.name);
                    }
                }
            }
            return instance;
        }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = GetComponent<T>();
            }
            if (!DestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
        protected virtual bool DestroyOnLoad { get { return true; } }

        void OnApplicationQuit()
        {
            isQuit = true;
        }
        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
            }
        }
    }
}