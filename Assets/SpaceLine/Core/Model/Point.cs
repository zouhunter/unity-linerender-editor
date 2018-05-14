#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 01:12:30
    * 说    明：       
* ************************************************************************************/
#endregion
namespace SpaceLine {
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    /// <summary>
    /// 线节点
    /// <summary>
    [System.Serializable]
    public class Point {
        public string m_id;
        public string type;
        public Vector3 position;
        public Vector3 initposition { get; private set; }
        public Point(Vector3 position, string type)
        {
            this.type = type;
            this.initposition = this.position = position;
            m_id = System.Guid.NewGuid().ToString();
        }
        internal Point Copy()
        {
            return new Point(position, type);
        }
    }
}
