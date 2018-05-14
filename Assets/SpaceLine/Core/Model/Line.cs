#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 11:26:50
    * 说    明：       1.记录线的起点和终点
                       2.记录线的形态
* ************************************************************************************/
#endregion
namespace SpaceLine {
    using System;
    using UnityEngine;

    /// <summary>
    /// 一条线的信息
    /// <summary>
    [System.Serializable]
    public class Line {
        public string id;
        public string type;
        public string fromNodeId;
        public string toNodeId;

        public Line(string fromID, string toID, string type)
        {
            this.type = type;
            this.fromNodeId = fromID;
            this.toNodeId = toID;
            id = System.Guid.NewGuid().ToString();
        }
        public bool IsSame(Line otherLine)
        {
            if (otherLine.fromNodeId == fromNodeId && otherLine.toNodeId == toNodeId)
                return true;
            if (otherLine.fromNodeId == toNodeId && otherLine.toNodeId == fromNodeId)
                return true;
            return false;
        }
        public void ResetToNodeID(string toNodeId)
        {
            this.toNodeId = toNodeId;
        }
        public void ResetFromNodeID(string fromNodeId)
        {
            this.fromNodeId = fromNodeId;
        }
        internal Line Copy()
        {
            return new Line(fromNodeId, toNodeId, type);
        }
    }
}
