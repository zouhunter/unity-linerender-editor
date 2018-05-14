#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 11:17:23
    * 说    明：       1.类似于MeshRender
* ************************************************************************************/
#endregion

namespace SpaceLine
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 将一组线绘制到场景中
    /// <summary>
    public class LinesRender : UnityEngine.MonoBehaviour
    {
        [SerializeField]
        private LinesObject _linesObject;
        public bool loadOnEnable;
        protected Dictionary<string, Transform> pointDic = new Dictionary<string, Transform>();
        protected Dictionary<string, Transform> lineDic = new Dictionary<string, Transform>();
        protected List<GameObject> objectPool = new List<GameObject>();
        public LinesObject linesObject
        {
            get
            {
                return _linesObject;
            }
            set
            {
                _linesObject = value;
                UpdateLinesObject();
            }
        }
        public Func<Transform, GameObject, GameObject> GetPoolObject { get; set; }
        public Action<GameObject> SavePoolObject { get; set; }

        protected virtual void OnEnable()
        {
            VerifyFunction();
            if (linesObject)
            {
                UpdateLinesObject();
            }
        }
        /// <summary>
        /// 节点绘制
        /// </summary>
        /// <param name="point"></param>
        protected virtual void DrawPoint(Point point) { }
        /// <summary>
        /// 线绘制
        /// </summary>
        /// <param name="line"></param>
        protected virtual void DrawLine(Line line) { }

        /// <summary>
        /// 创建线框体系
        /// </summary>
        protected void UpdateLinesObject()
        {

        }

        protected void VerifyFunction()
        {
            if (GetPoolObject == null)
            {
                GetPoolObject = (parent, prefab) =>
                {
                    var instence = Instantiate(prefab);
                    instence.transform.SetParent(parent);
                    return instence;
                };
            }

            if (SavePoolObject == null)
            {
                SavePoolObject = (instence) =>
                {
                    Destroy(instence);
                };
            }
        }
    }
}
