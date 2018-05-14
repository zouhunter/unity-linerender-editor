#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 04:17:48
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
    /// 一对节点
    /// <summary>
    [System.Serializable]
    public class RecordPair
    {
        public string type;
        public PointRecord a;
        public PointRecord b;
    }
}