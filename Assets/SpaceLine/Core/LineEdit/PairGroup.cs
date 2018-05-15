#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 04:23:38
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
namespace SpaceLine
{
    /// <summary>
    /// 一组
    /// <summary>
    public class PairGroup : UnityEngine.MonoBehaviour
    {
        public Color lineColor = Color.white;
        [HideInInspector]
        public List<RecordPair> pairs = new List<RecordPair>();
        [HideInInspector]
        public int activeID = -1;
        [Range(0.5f,5)]
        public float gizmosSize = 1;

        private void OnDrawGizmos()
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                if (pair.a != null && pair.b != null)
                {
                    if (activeID == i)
                    {
                        var posa = pair.a.transform.position;
                        var posb = pair.b.transform.position;
                       
                        var pos = (posa + posb) * 0.5f;
                        Gizmos.matrix = Matrix4x4.TRS(pos,Quaternion.FromToRotation(Vector3.right,posb - posa),Vector3.one);
                        Gizmos.color = Color.green;
                        Gizmos.DrawCube(Vector3.zero,new Vector3(Vector3.Distance(posa,posb), gizmosSize, gizmosSize));
                        Gizmos.matrix = Matrix4x4.identity;

                        Gizmos.color = Color.red;
                        Gizmos.DrawSphere(posa, 1);
                        Gizmos.DrawSphere(posb, 1);
                    }
                    else
                    {
                        Debug.DrawLine(pair.a.transform.position, pair.b.transform.position, lineColor);
                    }
                }
            }
        }
    }
}