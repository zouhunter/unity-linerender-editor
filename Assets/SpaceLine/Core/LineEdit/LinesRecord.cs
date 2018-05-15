#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 04:19:28
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
    /// MonoBehaiver
    /// <summary>
    public class LinesRecord : MonoBehaviour
    {
        public float mergeDistence = 0.1f;
        public LinesObject target;

        public PairGroup[] pairGroups {
            get
            {
                return GetComponentsInChildren<PairGroup>();
            }
        }
         
    }
}