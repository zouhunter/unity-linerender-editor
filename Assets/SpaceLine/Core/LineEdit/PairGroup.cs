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
        [HideInInspector]
        public List<RecordPair> pairs = new List<RecordPair>();
        private void OnDrawGizmos()
        {
            for (int i = 0; i < pairs.Count; i++)
            {
                var pair = pairs[i];
                if(pair.a != null && pair.b != null)
                {
                    Gizmos.DrawLine(pair.a.transform.position, pair.b.transform.position);
                }
            }

           

        }

    }
}