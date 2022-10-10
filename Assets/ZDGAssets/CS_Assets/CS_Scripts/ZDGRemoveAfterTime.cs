/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System.Collections;

namespace ZombieDriveGame
{
	/// <summary>
	/// This script removes the object after some time
	/// </summary>
	public class ZDGRemoveAfterTime : MonoBehaviour 
	{
        [Tooltip("How many seconds to wait before removing this object")]
        public float removeAfterTime = 1;

		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void Start() 
		{
			// Remove this object after a delay
			Destroy( gameObject, removeAfterTime);
		}
	}
}
