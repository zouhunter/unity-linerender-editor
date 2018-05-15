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
        SerializedProperty activeProp;
        ReorderableList reorderList;
        private void OnEnable()
        {
            pairsProp = serializedObject.FindProperty("pairs");
            activeProp = serializedObject.FindProperty("activeID");
            InitReorderList();
        }
        private void OnDisable()
        {
            if(serializedObject != null)
            {
                serializedObject.Update();
                activeProp.intValue = -1;
                serializedObject.ApplyModifiedProperties();
            }
           
        }

        private void InitReorderList()
        {
            reorderList = new ReorderableList(serializedObject, pairsProp);
            reorderList.drawHeaderCallback = DrawHead;
            reorderList.onSelectCallback = OnSelectElement;
            reorderList.drawElementCallback = DrawElement;
        }

        private void OnSelectElement(ReorderableList list)
        {
            activeProp.intValue = list.index;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            DrawIconChangeContent();
            reorderList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawIconChangeContent()
        {
            if (GUILayout.Button("group icons",EditorStyles.toolbarDropDown))
            {
                var icons = Array.ConvertAll(Enum.GetNames(typeof(SelectIcon.Icon)), x => new GUIContent(x));
                var idex = -1;
                EditorUtility.DisplayCustomMenu(new Rect(Event.current.mousePosition, Vector2.zero), icons, idex, OnSelect, null);
            }
        }

        private void OnSelect(object userData, string[] options, int selected)
        {
            var type = (SelectIcon.Icon)Enum.Parse(typeof(SelectIcon.Icon), options[selected]);
            var group = target as PairGroup;
            foreach (var item in group.pairs)
            {
                if (item.a) SelectIcon.SetIcon(item.a.gameObject, type);
                if (item.b) SelectIcon.SetIcon(item.b.gameObject, type);
            }
        }
        private void DrawHead(Rect rect)
        {
            var idRect = new Rect(rect.x, rect.y, 60, EditorGUIUtility.singleLineHeight);
            rect.x += 20;
            var typeRect = new Rect(rect.x + 20, rect.y, 60, EditorGUIUtility.singleLineHeight);
            var nameRect = new Rect(rect.x + 85, rect.y, 60, EditorGUIUtility.singleLineHeight);
            var aPropRect = new Rect(rect.x + 150, rect.y, (rect.width - 150) * 0.5f, EditorGUIUtility.singleLineHeight);
            var bPropRect = aPropRect;
            bPropRect.x = aPropRect.x + (rect.width - 150) * 0.5f;

            EditorGUI.LabelField(idRect,"index");
            EditorGUI.LabelField(typeRect, "类型");
            EditorGUI.LabelField(nameRect, "名称");
            EditorGUI.LabelField(aPropRect, "pointA");
            EditorGUI.LabelField(bPropRect, "pointB");
        }

        private void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
        {
            var prop = pairsProp.GetArrayElementAtIndex(index);
            var typeProp = prop.FindPropertyRelative("type");
            var aProp = prop.FindPropertyRelative("a");
            var bProp = prop.FindPropertyRelative("b");
            var nameProp = prop.FindPropertyRelative("_name");

            var idRect = new Rect(rect.x, rect.y, 20, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(idRect, (index + 1).ToString());
            var typeRect = new Rect(rect.x + 20, rect.y, 60, EditorGUIUtility.singleLineHeight);
            typeProp.stringValue = EditorGUI.TextField(typeRect, typeProp.stringValue);

            var nameRect = new Rect(rect.x + 85, rect.y, 60, EditorGUIUtility.singleLineHeight);
            if(string.IsNullOrEmpty(nameProp.stringValue) && aProp.objectReferenceValue && bProp.objectReferenceValue)
            {
                EditorGUI.LabelField(nameRect,aProp.objectReferenceValue.name + ":" + bProp.objectReferenceValue.name);
            }
            nameProp.stringValue = EditorGUI.TextField(nameRect, nameProp.stringValue);

            var aPropRect = new Rect(rect.x + 150, rect.y, (rect.width - 150) * 0.5f, EditorGUIUtility.singleLineHeight);
            aProp.objectReferenceValue = EditorGUI.ObjectField(aPropRect, aProp.objectReferenceValue, typeof(PointRecord), true);

            var bPropRect = aPropRect;
            bPropRect.x = aPropRect.x + (rect.width - 150) * 0.5f;
            bProp.objectReferenceValue = EditorGUI.ObjectField(bPropRect, bProp.objectReferenceValue, typeof(PointRecord), true);


        }
    }
}

