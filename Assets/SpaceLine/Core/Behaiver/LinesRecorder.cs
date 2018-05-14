#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 11:35:26
    * 说    明：       1.在editor状态下使用
* ************************************************************************************/
#endregion
namespace SpaceLine
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// 快速记录线的相关信息
    /// <summary>
    public class LinesRecorder : UnityEngine.MonoBehaviour
    {
        public LinesObject linesObject;

        private void OnDrawGizmos()
        {

        }
        private void OnDrawGizmosSelected()
        {
            Debug.Log(Gizmos.color);
        }
    }
}
