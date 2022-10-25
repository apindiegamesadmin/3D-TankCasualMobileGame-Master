/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine;

namespace ZombieDriveGame
{
    /// <summary>
    /// This script defines an explosion of gibs. Each gib must have a rigidbody attached to it.
    /// </summary>
    public class ZDGGibExplode : MonoBehaviour
    {
        [Tooltip("A list of the objects that will be exploded")]
        public Rigidbody[] gibs;

        [Tooltip("The explosion power. The objects fly randomly")]
        public float explodePower = 100;

        [Tooltip("The effect that is created at the location of this object when it is touched")]
        public Transform touchEffect;

        internal int index;
        
        public void Start()
        {
            Explode();
        }

        public void Explode()
        {
            // If there is a touch effect, create it
            if (touchEffect) Instantiate(touchEffect, transform.position, transform.rotation);
            // Go through all the gibs, and throw them in a random direction
            if (gibs.Length > 0)
            {
                for (index = 0; index < gibs.Length; index++)
                {
                    // Throw the object in a random direction
                    gibs[index].AddForce(new Vector3(Random.Range(-explodePower, explodePower), Random.Range(explodePower * 2, explodePower * 3), Random.Range(-explodePower, explodePower)));

                    // Give the object a random rotation force
                    gibs[index].AddTorque(new Vector3(Random.Range(-explodePower, explodePower), Random.Range(-explodePower, explodePower), Random.Range(-explodePower, explodePower)));
                }
            }
        }
    }
}
