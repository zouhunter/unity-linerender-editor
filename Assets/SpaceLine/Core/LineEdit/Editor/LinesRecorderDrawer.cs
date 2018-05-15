#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 11:40:33
    * 说    明：       1.在editor状态下使用
* ************************************************************************************/
#endregion

namespace SpaceLine.Drawer
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;
    /// <summary>
    /// LinesRecord的配制工具
    /// <summary>
    [CustomEditor(typeof(LinesRecord))]
    public class LinesRecorderDrawer : Editor
    {
        LinesRecord linesRecord;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("AutoRecord"))
            {
                serializedObject.Update();
                RecordInfomations();
                serializedObject.ApplyModifiedProperties();
            }
           
        }

        private void RecordInfomations()
        {
            linesRecord = target as LinesRecord;
            var groups = linesRecord.pairGroups;
            var pairs = MergeGroups(groups);
            var points = GetMinPointRecords(pairs);
            RecordPoints(points, linesRecord.target);
            RecordPairs(pairs, linesRecord.target);
        }

        private List<RecordPair> MergeGroups(PairGroup[] groups)
        {
            var pairs = new List<RecordPair>();
            foreach (var group in groups)
            {
                foreach (var pair in group.pairs)
                {
                    if (pair.a != null && pair.b != null)
                    {
                        var func = new Predicate<RecordPair>(x =>
                        {
                            if (MergePoint(x.a.transform.position, pair.a.transform.position) && MergePoint(x.b.transform.position, pair.b.transform.position))
                                return true;
                            if (MergePoint(x.b.transform.position, pair.a.transform.position) && MergePoint(x.a.transform.position, pair.a.transform.position))
                                return true;
                            return false;
                        });

                        if (pairs.Find(func) == null)
                        {
                            pairs.Add(pair);
                        }
                    }
                }
            }
            return pairs;
        }

        private bool MergePoint(Vector3 pointa,Vector3 pointb)
        {
            return Vector3.Distance(pointa, pointb) <= linesRecord.mergeDistence;
        }

        private List<PointRecord> GetMinPointRecords(List<RecordPair> pairs)
        {
            var list = new List<PointRecord>();
            foreach (var pair in pairs)
            {
                var temp = list.Find(x => Vector3.Distance(pair.a.transform.position, x.transform.position) < linesRecord.mergeDistence);
                if (!temp){
                    list.Add(pair.a);
                }
                temp = list.Find(x => Vector3.Distance(pair.b.transform.position, x.transform.position) < linesRecord.mergeDistence);
                if (!temp){
                    list.Add(pair.b);
                }
            }
            return list;
        }

        private void RecordPoints(List<PointRecord> points, LinesObject linesObj)
        {
            linesObj.points.Clear();
            foreach (var item in points)
            {
                var point = new Point(item.transform.position, item.type);
                linesObj.points.Add(point);
            }
        }
        private void RecordPairs(List<RecordPair> pairs, LinesObject linesObj)
        {
            var points = linesObj.points;
            linesObj.lines.Clear();

            foreach (var pair in pairs)
            {
                var pointa = points.Find(x => MergePoint(pair.a.transform.position, x.position));
                var pointb = points.Find(x => MergePoint(pair.b.transform.position, x.position));
                if(pointa != null && pointb != null)
                {
                    var line = new Line(pointa.id, pointb.id, pair.type);
                    line.name = pair.name;
                    linesObj.lines.Add(line);
                }
            }
        }

    }
}
