#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 01:43:33
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

namespace SpaceLine
{
    /// <summary>
    /// 点
    /// <summary>
    public abstract class ContentBehaiver : MonoBehaviour
    {
        public GameObject Body { get { return gameObject; } }
        protected PointerEventData pointData;
        protected List<RaycastResult> rayCasts = new List<RaycastResult>();
        protected float timer;
        public static ContentBehaiver hoverItem;

        protected void CreateCollider()
        {
            gameObject.AddComponent<BoxCollider>();
        }

        protected bool IsMousePointOnUI()
        {
            if (EventSystem.current != null)
            {
                if (pointData == null)
                {
                    pointData = new PointerEventData(EventSystem.current);
                }
                pointData.position = Input.mousePosition;
                EventSystem.current.RaycastAll(pointData, rayCasts);
                foreach (var item in rayCasts)
                {
                    if (item.gameObject.layer == 5)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected virtual void OnMouseOver()
        {
            hoverItem = this;
        }
    }
}