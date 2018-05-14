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
    public abstract class LinesRenderBase : UnityEngine.MonoBehaviour
    {
        [SerializeField]
        private LinesObject _linesObject;
        public bool loadOnEnable;
        public LinesObject linesObject
        {
            get
            {
                return _linesObject;
            }
            set
            {
                _linesObject = value;
                DrawLinesObject();
            }
        }
        protected List<IPointBehaiver> createdPoints = new List<IPointBehaiver>();
        protected List<ILineBehaiver> createdLines = new List<ILineBehaiver>();
        protected virtual void Awake() { }
        protected virtual void OnEnable()
        {
            if (linesObject){
                DrawLinesObject();
            }
        }
        /// <summary>
        /// 节点
        /// </summary>
        /// <param name="point"></param>
        protected abstract IPointBehaiver DrawPoint(Point point,ViewRule rule);
        /// <summary>
        /// 线
        /// </summary>
        /// <param name="line"></param>
        protected abstract ILineBehaiver DrawLine(Line line, ViewRule rule);

        /// <summary>
        /// 回收point
        /// </summary>
        /// <param name="point"></param>
        protected abstract void SavePoint(IPointBehaiver point);

        /// <summary>
        /// 回收线
        /// </summary>
        /// <param name="line"></param>
        protected abstract void SaveLine(ILineBehaiver line);

        /// <summary>
        /// 清除已经创建的对象
        /// </summary>
        protected void ClearCreated()
        {
            foreach (var item in createdPoints)
            {
                SavePoint(item);
            }

            foreach (var item in createdLines)
            {
                SaveLine(item);
            }

            createdLines.Clear();
            createdPoints.Clear();
        }
        /// <summary>
        /// 创建线框体系
        /// </summary>
        protected void DrawLinesObject()
        {
            var rule = linesObject.rule;
            foreach (var item in linesObject.points)
            {
               var point = DrawPoint(item, rule);
                createdPoints.Add(point);
            }
            foreach (var item in linesObject.lines)
            {
                var line = DrawLine(item, rule);
                createdLines.Add(line);
            }
        }

    }
}
