#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 03:02:08
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SpaceLine.Common
{
    /// <summary>
    /// MonoBehaiver
    /// <summary>
    public sealed class CommonLinesRender : LinesRenderBase
    {
        private Queue<PointBehaiver> pointPool = new Queue<PointBehaiver>();
        private Queue<LineBehaiver> linePool = new Queue<LineBehaiver>();
        private Transform pointParent;
        private Transform lineParent;
        public UnityAction<Line> onClickLine { get; set; }
        public UnityAction<Point> onClickPoint { get; set; }

        protected override void Awake()
        {
            base.Awake();
            InitParents();
        }

        private void InitParents()
        {
            pointParent = new GameObject("Points").transform;
            lineParent = new GameObject("Lines").transform;

            pointParent.SetParent(transform);
            lineParent.SetParent(transform);

            pointParent.transform.localPosition = Vector3.zero;
            lineParent.transform.localPosition = Vector3.zero;
        }

        protected override ILineBehaiver DrawLine(Line line, ViewRule rule)
        {
            LineBehaiver behaiver = null;
            if (linePool.Count > 0)
            {
                behaiver = linePool.Dequeue();
                behaiver.Body.SetActive(true);
            }

            else
            {
                var instence = new GameObject(line.id, typeof(LineBehaiver));
                instence.transform.SetParent(lineParent);
                behaiver = instence.GetComponent<LineBehaiver>();
                behaiver.onClicked = (x) => { if (onClickLine != null) onClickLine(x.Info); Debug.Log("click:" + x.Info.name); };
            }
            var startPoint = linesObject.points.Find(x => x.id == line.fromNodeId);
            var endPoint = linesObject.points.Find(x => x.id == line.toNodeId);
            behaiver.Body.transform.localPosition = (startPoint.position + endPoint.position) * 0.5f;
            behaiver.Body.transform.forward = (endPoint.position - startPoint.position).normalized;
            behaiver.ReSetLength(Vector3.Distance(endPoint.position, startPoint.position));
            behaiver.OnInitialized(line);
            behaiver.SetMaterial(rule.GetMaterial(line.type));
            behaiver.SetLineWidth(rule.GetLineWidth(line.type));
            behaiver.SetColor(rule.GetColor(line.type));
            return behaiver;
        }

        protected override IPointBehaiver DrawPoint(Point point, ViewRule rule)
        {
            PointBehaiver behaiver = null;
            if(pointPool.Count > 0)
            {
                behaiver = pointPool.Dequeue();
                behaiver.Body.SetActive(true);
            }
            else
            {
                var instence = new GameObject(point.id, typeof(PointBehaiver));
                instence.transform.SetParent(pointParent);
                behaiver = instence.GetComponent<PointBehaiver>();
                behaiver.onClicked = (x) => { if (onClickPoint != null) onClickPoint(x.Info); Debug.Log("click:" + x); };
            }
            behaiver.OnInitialized(point);
            return behaiver;

        }

        protected override void SaveLine(ILineBehaiver line)
        {
            if(line is LineBehaiver)
            {
                line.Body.SetActive(false);
                linePool.Enqueue(line as LineBehaiver);
            }
            else
            {
                Destroy(line.Body);
            }
        }

        protected override void SavePoint(IPointBehaiver point)
        {
            if (point is PointBehaiver)
            {
                point.Body.SetActive(false);
                pointPool.Enqueue(point as PointBehaiver);
            }
            else
            {
                Destroy(point.Body);
            }
        }
    }
}