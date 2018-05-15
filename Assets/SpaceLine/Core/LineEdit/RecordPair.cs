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
    [Serializable]
    public class RecordPair
    {
        public string type;
        [SerializeField]
        private string _name;
        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name)){
                    return a.name + ":" + b.name;
                }
                return _name;
            }
        }
        public PointRecord a;
        public PointRecord b;
    }
}