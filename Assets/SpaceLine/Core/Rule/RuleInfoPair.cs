#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 02:06:45
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
namespace SpaceLine
{
    /// <summary>
    /// 材质信息
    /// <summary>
    [System.Serializable]
    public struct RuleInfoPair
    {
        
        public Material material;
        public float linewidth;
        public Color linecolor;
        public float pointSize;
    }

    [System.Serializable]
    public class RuleInfoGroup
    {
        public string type;
        public RuleInfoPair pair;
    }
}