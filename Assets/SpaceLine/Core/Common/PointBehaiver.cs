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
namespace SpaceLine.Common
{
    /// <summary>
    /// 点
    /// <summary>
    public sealed class PointBehaiver : ContentBehaiver,IPointBehaiver
    {
        private float diameter = 1;
        public Point Info { get; private set; }
        public UnityAction<PointBehaiver> onHover { get; set; }
        public UnityAction<PointBehaiver> onClicked { get; set; }
        
        private void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer("SpaceLine_point");
        }
        internal void OnInitialized(Point node)
        {
            this.Info = node;
            transform.localPosition = Info.position;
            CreateCollider();
        }
        private void OnMouseUp()
        {
            if (onClicked != null && !IsMousePointOnUI() && HaveExecuteTwince(ref timer)) onClicked.Invoke(this);
        }

        private void OnMouseOver()
        {
            if (onHover != null && !IsMousePointOnUI())
                onHover.Invoke(this);
        }

        public void SetSize(float r_node)
        {
            diameter = 2 * r_node;
            transform.localScale = Vector3.one * diameter;
        }
    }
}