using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using SpaceLine;

public class LinesSwitch : MonoBehaviour
{
    public LinesRenderBase linesRender;
    public List<LinesObject> lines = new List<LinesObject>();
    private int index;
    private void OnGUI()
    {
        if (GUILayout.Button("Next"))
        {
            if (index >= lines.Count || index < 0){
                index = 0;
            }

            if (index >= 0 && lines.Count > 0)
            {
                var obj = lines[index++];
                linesRender.linesObject = obj;
            }
        }
    }
}
