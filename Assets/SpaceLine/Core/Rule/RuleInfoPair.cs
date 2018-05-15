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
    public class RuleInfoPair
    {
        public Material material;
        public float linewidth = 1;
        public Color linecolor = Color.white;
    }

    [System.Serializable]
    public class RuleInfoGroup
    {
        public string type;
        public RuleInfoPairs pairs;
    }

    [System.Serializable]
    public class RuleInfoPairs
    {
        public RuleInfoPair normalPair;
        public RuleInfoPair hoverPair;
    }
}