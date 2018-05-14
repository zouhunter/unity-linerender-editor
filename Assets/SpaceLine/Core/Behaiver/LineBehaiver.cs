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
using System.Collections;
using System.Collections.Generic;
namespace SpaceLine
{
    /// <summary>
    /// 点
    /// <summary>
    public class LineBehaiver : ContentBehaiver
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

        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("SpaceLine_line");
            if (gameObject.layer <= 0)
            {
                Debug.LogError("layer not exist:" + "SpaceLine_line");
            }
        }
        private void Start()
        {
            if (lineRender == null)
                InitLineRenderer();
        }
        private void Update()
        {
            if (lineRender != null && Camera.main && lineRender.positionCount > 0)
            {
                currentWidth = normalWidth * Vector3.Distance(transform.position, Camera.main.transform.position) / normalDistence;
                if (Mathf.Abs(currentWidth - lineRender.widthStart) > 0.01f)
                {
                    lineRender.SetWidth(currentWidth, currentWidth);
                }
            }

        }
        private void InitLineRenderer()
        {
            var holder = new GameObject("lineRenderer");
            holder.AddComponent<MeshFilter>();
            holder.AddComponent<MeshRenderer>();
            lineRender = holder.AddComponent<VRLineRenderer>();
            lineRender.SetVertexCount(0);
            lineRender.useWorldSpace = true;
            lineRender.transform.SetParent(transform.parent);
        }

        public void OnInitialized(Line line)
        {
            this.Info = line;
            CreateCollider();
        }

        public void ShowLine(Material mat, float width)
        {
            normalWidth = width;
            lineRender.GetComponent<Renderer>().material = mat;

            var poss = new Vector3[2];
            poss[0] = transform.forward * longness * 0.5f + transform.position;
            poss[1] = -transform.forward * longness * 0.5f + transform.position;

            lineRender.SetVertexCount(2);
            lineRender.SetPositions(poss);
            lineRender.EditorCheckForUpdate();

            if (instence != null)
            {
                instence.SetActive(false);
            }
        }

        public override void ShowModel(GameObject instence)
        {
            base.ShowModel(instence);
            if (lineRender)
                lineRender.SetVertexCount(0);
        }

        internal void ReSetLength(float longness)
        {
            this.longness = longness;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, longness);
        }
        private void OnMouseUp()
        {
            if (onClicked != null && !IsMousePointOnUI() && HaveExecuteTwince(ref timer)) onClicked.Invoke(this);
        }
        private void OnMouseOver()
        {
            if (onHover != null && !IsMousePointOnUI()) onHover.Invoke(this);
        }

        public void SetSize(float r_Bar)
        {
            this.diameter = r_Bar * 2;
            transform.localScale = new Vector3(diameter, diameter, longness);
        }
    }
}