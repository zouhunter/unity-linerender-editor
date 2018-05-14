using UnityEngine;
using System.Collections.Generic;
using System.Collections;
namespace UnityEngine.Internal
{
    public class ObjectManager : ManagerTemp<ObjectManager>
    {
        private float cachingTime = 30f;
        //创建对象池字典
        private Dictionary<string, List<GameObject>> poolObjs = new Dictionary<string, List<GameObject>>();

        private Dictionary<GameObject, float> poolObjTimes = new Dictionary<GameObject, float>();

        //private List<GameObject> currentList;
        private Transform _transform;
        private Coroutine updateCo;
        protected override void Awake()
        {
            base.Awake();
            _transform = transform;
        }

        /// <summary>
        /// 用于创建静止的物体，指定父级、坐标
        /// </summary>
        /// <returns></returns>
        public GameObject GetPoolObject(GameObject pfb, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false, bool activepfb = false)
        {
            pfb.SetActive(true);
            GameObject currGo;
            ////Debug.Log(pfb.name);
            //如果有预制体为名字的对象小池
            if (poolObjs.ContainsKey(pfb.name))
            {
                List<GameObject> currentList = null;
                currentList = poolObjs[pfb.name];
                //遍历每数组，得到一个隐藏的对象
                for (int i = 0; i < currentList.Count; i++)
                {
                    //已经被销毁
                    if (currentList[i] == null)
                    {
                        continue;
                    }
                    if (!currentList[i].activeSelf)
                    {
                        currentList[i].SetActive(true);
                        currentList[i].transform.SetParent(parent, world);
                        if (resetLocalPosition)
                        {
                            currentList[i].transform.localPosition = Vector3.zero;
                        }
                        if (resetLocalScale)
                        {
                            currentList[i].transform.localScale = Vector3.one;
                        }
                        pfb.SetActive(activepfb);
                        poolObjTimes.Remove(currentList[i]);
                        return currentList[i];
                    }
                }
                //当没有隐藏对象时，创建一个并返回
                currGo = CreateAGameObject(pfb, parent, world, resetLocalPosition, resetLocalScale);
                currentList.Add(currGo);
                pfb.SetActive(activepfb);
                return currGo;
            }
            currGo = CreateAGameObject(pfb, parent, world, resetLocalPosition, resetLocalScale);
            //如果没有对象小池
            poolObjs.Add(currGo.name, new List<GameObject>() { currGo });
            pfb.SetActive(activepfb);
            return currGo;
        }

        public GameObject GetPoolObject(string pfbName, Transform parent, bool world, bool resetLocalPosition = true, bool resetLocalScale = false)
        {
            if (poolObjs.ContainsKey(pfbName))
            {
                List<GameObject> currentList = null;
                currentList = poolObjs[pfbName];
                //遍历每数组，得到一个隐藏的对象
                for (int i = 0; i < currentList.Count; i++)
                {
                    if (!currentList[i].activeSelf)
                    {
                        currentList[i].SetActive(true);
                        currentList[i].transform.SetParent(parent, world);
                        if (resetLocalPosition)
                        {
                            currentList[i].transform.localPosition = Vector3.zero;
                        }
                        if (resetLocalScale)
                        {
                            currentList[i].transform.localScale = Vector3.one;
                        }
                        poolObjTimes.Remove(currentList[i]);
                        return currentList[i];
                    }
                }
            }

            return null;
        }

        public GameObject CreateAGameObject(GameObject pfb, Transform parent, bool world, bool resetLocalPositon = true, bool resetLocalScale = false)
        {
            GameObject currentGo = Instantiate(pfb);
            currentGo.name = pfb.name;
            currentGo.transform.SetParent(parent, world);
            if (resetLocalPositon){
                currentGo.transform.localPosition = Vector3.zero;
            }
            if (resetLocalScale) {
                currentGo.transform.localScale = Vector3.one;
            }
            return currentGo;
        }

        IEnumerator UpdatePool()
        {
            while (true)
            {
                if (poolObjTimes.Count == 0)
                {
                    updateCo = null;
                    yield break;
                }

                GameObject destroyGo = null;
                foreach (var pair in poolObjTimes)
                {
                    if (Time.time - pair.Value > cachingTime)
                    {
                        destroyGo = pair.Key;
                        break;
                    }
                }

                if (destroyGo != null)
                {
                    var currList = poolObjs[destroyGo.name];
                    currList.Remove(destroyGo);
                    if (currList.Count == 0)
                    {
                        poolObjs.Remove(destroyGo.name);
                    }
                    Destroy(destroyGo);
                    Resources.UnloadUnusedAssets();
                    poolObjTimes.Remove(destroyGo);
                }

                yield return new WaitForSeconds(0.5f);
            }
        }

        public void SavePoolObject(GameObject go, bool world = false)
        {
            if (go == null || _transform == null) return;
            if (poolObjs.ContainsKey(go.name))
            {
                var currList = poolObjs[go.name];
                if (!currList.Contains(go))
                {
                    currList.Add(go);
                }
            }
            else
            {
                poolObjs.Add(go.name, new List<GameObject> { go });
            }

            go.transform.SetParent(transform, world);
            go.SetActive(false);
            poolObjTimes.Add(go, Time.time);
            if (updateCo == null)
            {
                updateCo = StartCoroutine(UpdatePool());
            }
        }

        public void ClearAllObject()
        {
            foreach (Transform item in transform)
            {
                Destroy(item.gameObject);
            }

            if (poolObjs != null)
                poolObjs.Clear();

            Resources.UnloadUnusedAssets();
            System.GC.Collect();
        }

        public void ClearObjectByPrefab(GameObject pfb)
        {
            if (poolObjs == null)
                return;

            if (!poolObjs.ContainsKey(pfb.name))
                return;

            var currList = poolObjs[pfb.name];
            poolObjs.Remove(pfb.name);
            currList.Clear();
        }
    }
}