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
        protected GameObject instence;
        protected PointerEventData pointData;
        protected List<RaycastResult> rayCasts = new List<RaycastResult>();
        protected float timer;

        public virtual void ShowModel(GameObject instence)
        {
            this.instence = instence;
            instence.transform.rotation = Quaternion.identity;
            instence.transform.position = Vector3.zero;
        }
        protected void CreateCollider(PrimitiveType primitiveType = PrimitiveType.Cube)
        {
            switch (primitiveType)
            {
                case PrimitiveType.Sphere:
                    break;
                case PrimitiveType.Capsule:
                    break;
                case PrimitiveType.Cylinder:
                    break;
                case PrimitiveType.Cube:
                    gameObject.AddComponent<BoxCollider>();
                    break;
                case PrimitiveType.Plane:
                    break;
                case PrimitiveType.Quad:
                    break;
                default:
                    break;
            }

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
        protected virtual bool HaveExecuteTwince(ref float timer, float time = 0.5f)
        {
            if (Time.time - timer < time)
            {
                timer = 0;
                return true;
            }
            else
            {
                timer = Time.time;
                return false;
            }
        }
    }
}