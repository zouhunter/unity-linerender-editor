#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 11:11:06
    * 说    明：       
* ************************************************************************************/
#endregion

namespace SpaceLine
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    /// <summary>
    /// 记录了所有线的起点终点，线的类型等
    /// （类似于一个Mesh）
    /// <summary>
    public class LinesObject : UnityEngine.ScriptableObject
    {
        public ViewRule rule;
        public List<Point> points = new List<Point>();
        public List<Line> lines = new List<Line>();

        public Group GetEditAbleGroup()
        {
            var group = new Group();
            group.points = points;
            group.lines = lines;
            return group;
        }

        public void AddLineGroup(Group group)
        {
            var tempGroup = GetEditAbleGroup();
            tempGroup.InsertData(group);
            points = tempGroup.points;
            lines = tempGroup.lines;
        }

        public void Clear()
        {
            points.Clear();
            lines.Clear();
        }
    }

}