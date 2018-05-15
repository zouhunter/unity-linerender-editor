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
        public RuleInfoPairs defultLineRule;
        public List<RuleInfoGroup> lineRules = new List<RuleInfoGroup>();
        private Dictionary<string, RuleInfoPairs> _rulesDic;
        public Dictionary<string, RuleInfoPairs> RuleDic
        {
            get
            {
                if (_rulesDic == null)
                {
                    _rulesDic = new Dictionary<string, RuleInfoPairs>();
                    foreach (var item in lineRules)
                    {
                        if (!string.IsNullOrEmpty(item.type))
                        {
                            _rulesDic.Add(item.type, item.pairs);
                        }
                    }
                }
                return _rulesDic;
            }
        }

        public RuleInfoPairs GetRulePairsFromType(string type)
        {
            if (RuleDic.ContainsKey(type)){
                return RuleDic[type];
            }
            return defultLineRule;
        }
    }
}