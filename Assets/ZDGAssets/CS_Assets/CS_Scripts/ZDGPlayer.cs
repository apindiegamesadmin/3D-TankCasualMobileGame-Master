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
    /// This script defines the player, who has health, fuel, and speed attributes. The player also has a death effect when dying.
    /// </summary>
    public class ZDGPlayer : MonoBehaviour
    {
        [Tooltip("The health of the player. If this reaches 0, the player dies")]
        public float health = 100;
        internal float healthMax;

        [Tooltip("The fuel of the player. If this reaches 0, the game ends")]
        public float fuel = 100;
        internal float fuelMax;

        [Tooltip("The speed of the player, how fast it moves player")]
        public float speed = 10;

        [Tooltip("How quickly the player changes direction from left to right and back")]
        public float turnSpeed = 100;

        [Tooltip("The maximum angle to which the player can turn. This is both for right and left directions")]
        public float turnRange = 30;

        [Tooltip("The effect that appears when this player dies")]
        public Transform deathEffect;

        /// <summary>
        /// Kill the player and create a death effect
        /// </summary>
        public void Die()
        {
            // Create a death effect at the position of the player
            if (deathEffect) Instantiate(deathEffect, transform.position, transform.rotation);

            // Remove the player from the game
            Destroy(gameObject);
        }
    }
}
