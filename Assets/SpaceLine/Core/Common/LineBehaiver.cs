#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 01:43:33
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
namespace SpaceLine.Common
{
    /// <summary>
    /// 点
    /// <summary>
    public class LineBehaiver : ContentBehaiver,ILineBehaiver
    {
        private float longness = 1;//长度加权
        private float diameter = 1;
        public UnityAction<LineBehaiver> onHover { get; set; }
        public UnityAction<LineBehaiver> onClicked { get; set; }
        public Line Info { get; private set; }

        private VRLineRenderer lineRender;
        private float normalWidth;
        private const float normalDistence = 10;
        private float currentWidth;
        private RuleInfoPairs rule;
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("SpaceLine_line");
            if (lineRender == null)
                InitLineRenderer();
        }
        private void Update()
        {
            if(lineRender != null)
            {
                ShowLine();

                if (Camera.main && lineRender.positionCount > 0)
                {
                    currentWidth = normalWidth * Vector3.Distance(transform.position, Camera.main.transform.position) / normalDistence;
                    if (Mathf.Abs(currentWidth - lineRender.widthStart) > 0.01f)
                    {
                        lineRender.SetWidth(currentWidth, currentWidth);
                    }
                }
            }

        
        }
        private void InitLineRenderer()
        {
            var holder = new GameObject("lineRenderer",typeof(MeshFilter),typeof(MeshRenderer));
            holder.transform.SetParent(transform);
            lineRender = holder.AddComponent<VRLineRenderer>();
            lineRender.SetVertexCount(0);
            lineRender.useWorldSpace = true;
        }

        public void OnInitialized(Line line,RuleInfoPairs rule)
        {
            this.Info = line;
            this.rule = rule;
            SetDefultState();
            CreateCollider();
        }

        private void SetDefultState()
        {
            SetMaterial(rule.normalPair.material);
            SetLineWidth(rule.normalPair.linewidth);
            SetColor(rule.normalPair.linecolor);
        }

        private void SetHoverState()
        {
            SetMaterial(rule.hoverPair.material);
            SetLineWidth(rule.hoverPair.linewidth);
            SetColor(rule.hoverPair.linecolor);
        }

        public void SetColor(Color color)
        {
            lineRender.GetComponent<Renderer>().material.color = color;
        }
        public void SetMaterial(Material mat)
        {
            lineRender.GetComponent<Renderer>().material = mat;
        }

        public void SetLineWidth(float width)
        {
            normalWidth = width;
        }

        public void ShowLine()
        {
            var poss = new Vector3[2];
            poss[0] = transform.forward * longness * 0.5f + transform.position;
            poss[1] = -transform.forward * longness * 0.5f + transform.position;

            lineRender.SetVertexCount(2);
            lineRender.SetPositions(poss);
            lineRender.EditorCheckForUpdate();
        }
        internal void ReSetLength(float longness)
        {
            this.longness = longness;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, longness);
        }

        private void OnMouseUp()
        {
            if (!IsMousePointOnUI())
            {
                if(onClicked != null && this == hoverItem)
                {
                    onClicked.Invoke(this);
                }

            }
        }

        private void OnMouseEnter()
        {
            SetHoverState();
        }
        private void OnMouseExit()
        {
            SetDefultState();
        }

        protected override void OnMouseOver()
        {
            base.OnMouseOver();
            if (onHover != null && !IsMousePointOnUI())
                onHover.Invoke(this);
        }

        public void SetSize(float r_Bar)
        {
            this.diameter = r_Bar * 2;
            transform.localScale = new Vector3(diameter, diameter, longness);
        }
    }
}