using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
namespace SpaceLine
{
    /// <summary>
    /// 线条信息
    /// <summary>
    [System.Serializable]
    public class LineInfoPair
    {
        public string type;
        public float linewidth;
        public Color linecolor;
    }
}