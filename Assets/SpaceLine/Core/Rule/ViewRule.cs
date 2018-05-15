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
        public List<RuleInfoGroup> rules = new List<RuleInfoGroup>();
        private Dictionary<string, RuleInfoPair> _rulesDic;
        public Dictionary<string, RuleInfoPair> RuleDic
        {
            get
            {
                if (_rulesDic == null)
                {
                    _rulesDic = new Dictionary<string, RuleInfoPair>();
                    foreach (var item in rules)
                    {
                        if (!string.IsNullOrEmpty(item.type))
                        {
                            _rulesDic.Add(item.type, item.pair);
                        }
                    }
                }
                return _rulesDic;
            }
        }

        internal Material GetMaterial(string type)
        {
            var pair = GetRuleFromType(type);
            return pair.material;
        }

        internal float GetLineWidth(string type)
        {
            var pair = GetRuleFromType(type);
            return pair.linewidth;
        }
        internal Color GetColor(string type)
        {
            var pair = GetRuleFromType(type);
            return pair.linecolor;
        }

        internal float GetPointSize(string type)
        {
            var pair = GetRuleFromType( type);
            return pair.pointSize;
        }

        public RuleInfoPair GetRuleFromType(string type)
        {
            if (RuleDic.ContainsKey(type))
            {
                return RuleDic[type];
            }
            return defultRule;
        }
    }
}