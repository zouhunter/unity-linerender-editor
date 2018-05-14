#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 01:20:54
    * 说    明：       1.记录线
                       2.记录点
* ************************************************************************************/
#endregion
namespace SpaceLine {
    using System;
    using UnityEngine;
    using System.Collections.Generic;
    /// <summary>
    /// 一组点线信息
    /// <summary>
    [System.Serializable]
    public class Group
    {
        public List<Point> points;//节点信息
        public List<Line> lines;//连接信息
        public Group()
        {
            points = new List<Point>();
            lines = new List<Line>();
        }

        public Group Copy()
        {
            var newData = new Group();
            var idDic = new Dictionary<string, string>();
            for (int i = 0; i < points.Count; i++)
            {
                var newNode = points[i].Copy();
                newData.points.Add(newNode);
                idDic.Add(points[i].id, newNode.id);
            }
            for (int i = 0; i < lines.Count; i++)
            {
                var newBar = lines[i].Copy();
                if (idDic.ContainsKey(newBar.fromNodeId))
                {
                    newBar.ResetFromNodeID(idDic[newBar.fromNodeId]);
                }
                if (idDic.ContainsKey(newBar.toNodeId))
                {
                    newBar.ResetToNodeID(idDic[newBar.toNodeId]);
                }
                newData.lines.Add(newBar);
            }
            return newData;
        }

        internal void InsertData(Group data)
        {
            Dictionary<string, string> guidChanged = new Dictionary<string, string>();
            foreach (var item in data.points)
            {
                var same = points.Find(x => Vector3.Distance(x.position, item.position) < 0.1f);
                if (same != null)
                {
                    guidChanged.Add(item.id, same.id);
                }
                else
                {
                    points.Add(item);
                }
            }
            foreach (var item in data.lines)
            {
                bool formNodeSame = guidChanged.ContainsKey(item.fromNodeId);
                bool toNodeSame = guidChanged.ContainsKey(item.toNodeId);

                if (formNodeSame && !toNodeSame)
                {
                    var newBar = item.Copy();
                    newBar.ResetFromNodeID(guidChanged[item.fromNodeId]);
                    lines.Add(newBar);
                }
                else if (!formNodeSame && toNodeSame)
                {
                    var newBar = item.Copy();
                    newBar.ResetToNodeID(guidChanged[item.toNodeId]);
                    lines.Add(newBar);
                }
                else if (!formNodeSame && !toNodeSame)
                {
                    lines.Add(item);
                }
                else
                {
                    var newBar = item.Copy();
                    newBar.ResetFromNodeID(guidChanged[item.fromNodeId]);
                    newBar.ResetToNodeID(guidChanged[item.toNodeId]);
                    if (lines.Find(x => x.IsSame(newBar)) == null)
                    {
                        lines.Add(newBar);
                    }
                }
            }
        }

        internal void AppendRotation(Quaternion rotate)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].position = rotate * points[i].initposition;
            }
        }

        internal void AppendPosition(Vector3 pos)
        {
            for (int i = 0; i < points.Count; i++)
            {
                points[i].position += pos;
            }

        }
    }
}
