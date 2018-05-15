#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 02:06:14
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using System.Collections.Generic;
using UnityEngine;
namespace SpaceLine
{
    /// <summary>
    /// 规则类
    /// <summary>
    [System.Serializable]
    public class ViewRule
    {
        public RuleInfoPair defultRule;
        public List<RuleInfoPair> rules = new List<RuleInfoPair>();

        internal Material GetMaterial(string type)
        {
            var pair = rules.Find(x => x.type == type);
            if (pair.material == null)
                return defultRule.material;
            return pair.material;
        }

        internal float GetLineWidth(string type)
        {
            var pair = rules.Find(x => x.type == type);
            if (pair.linewidth <= 0)
                return defultRule.linewidth;
            return pair.linewidth;
        }
        internal Color GetColor(string type)
        {
            var pair = rules.Find(x => x.type == type);
            if (string.IsNullOrEmpty(pair.type))
                return defultRule.linecolor;
            return pair.linecolor;
        }

    }
}