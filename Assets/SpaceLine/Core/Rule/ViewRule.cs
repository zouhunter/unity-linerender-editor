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
        public List<MaterialPair> materials = new List<MaterialPair>();
        public List<LineInfoPair> lineinfos = new List<LineInfoPair>();

        internal Material GetMaterial(string type)
        {
            var pair = materials.Find(x => x.type == type);
            return pair.material;
        }

        internal float GetLineWidth(string type)
        {
            var pair = lineinfos.Find(x => x.type == type);
            return pair.linewidth;
        }
        internal Color GetColor(string type)
        {
            var pair = lineinfos.Find(x => x.type == type);
            return pair.linecolor;
        }
        
    }
}