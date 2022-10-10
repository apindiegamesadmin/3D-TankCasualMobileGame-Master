/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;
using System;

namespace ZombieDriveGame.Types
{
	/// <summary>
	/// This script defines a spawnable object, with a spawn chance.
	/// </summary>
	[Serializable]
	public class Spawn
	{
        [Tooltip("The object that will be spawned")]
        public Transform spawnObject;

        [Tooltip("The chance for it to be spawned")]
        public int spawnChance = 1;

        [Tooltip("The minimum gap that should be created between this object and the next")]
        public int spawnGap = 0;
    }
}
