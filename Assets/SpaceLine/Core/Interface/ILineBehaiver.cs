#region statement
/*************************************************************************************   
    * 作    者：       zouhunter
    * 时    间：       2018-05-14 03:05:53
    * 说    明：       
* ************************************************************************************/
#endregion
using System;
using UnityEngine;
namespace SpaceLine
{
    /// <summary>
    /// 接口
    /// <summary>
    public interface ILineBehaiver
    {
        GameObject Body { get; }
        Line Info { get; }
    }
}