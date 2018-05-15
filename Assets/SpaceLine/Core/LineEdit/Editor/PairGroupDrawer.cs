#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-15 09:40:24
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;

namespace SpaceLine
{
    [CustomEditor(typeof(PairGroup))]
    public class PairGroupDrawer : Editor
    {
        SerializedProperty pairsProp;
        ReorderableList reorderList;
        private void OnEnable()
        {
            pairsProp = serializedObject.FindProperty("pairs");
            InitReorderList();
        }

        private void InitReorderList()
        {
            reorderList = new ReorderableList(serializedObject, pairsProp);
            reorderList.drawHeaderCallback = (rect) => { EditorGUI.LabelField(rect,"连接列表"); };
            reorderList.drawElementCallback = DrawElement;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            reorderList.DoLayoutList();
        }
        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var prop = pairsProp.GetArrayElementAtIndex(index);
            var typeProp = prop.FindPropertyRelative("type");
            var aProp = prop.FindPropertyRelative("a");
            var bProp = prop.FindPropertyRelative("b");

            var idRect = new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(idRect,(index + 1).ToString());
            var typeRect = new Rect(rect.x + 20, rect.y, 60, EditorGUIUtility.singleLineHeight);
            typeProp.stringValue =  EditorGUI.TextField(typeRect, typeProp.stringValue);

            var aPropRect = new Rect(rect.x + 90, rect.y, (rect.width - 90) * 0.4f, EditorGUIUtility.singleLineHeight);
            aProp.objectReferenceValue = EditorGUI.ObjectField(aPropRect, aProp.objectReferenceValue,typeof(RecordPair), false);


            var bPropRect = aPropRect;
            bPropRect.x = aPropRect.x + (rect.width - 90) * 0.5f;
            bProp.objectReferenceValue = EditorGUI.ObjectField(bPropRect, bProp.objectReferenceValue, typeof(RecordPair), false);
        }
    }
}

