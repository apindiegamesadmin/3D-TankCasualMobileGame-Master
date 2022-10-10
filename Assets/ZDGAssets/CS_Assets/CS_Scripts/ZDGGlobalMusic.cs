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
	/// handles a global music source which carries over from scene to scene without resetting the music track.
	/// You can have this script attached to a music object and include that object in each scene, and the script will keep
	/// only the oldest music source in the scene.
	/// </summary>
	public class ZDGGlobalMusic : MonoBehaviour 
	{
		//The tag of the music source
		public string musicTag = "Music";
		
		//The time this instance of the music source has been in the game
		internal float instanceTime = 0;

		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// Awake is used to initialize any variables or game state before the game starts. Awake is called only once during the 
		/// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
		/// objects or query them using eg. GameObject.FindWithTag. Each GameObject's Awake is called in a random order between objects. 
		/// Because of this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth. 
		/// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not act as a coroutine.
		/// </summary>
		void  Awake()
		{
			//Find all the music objects in the scene
			GameObject[] musicObjects = GameObject.FindGameObjectsWithTag(musicTag);
			
			//Keep only the music object which has been in the game for more than 0 seconds
			if ( musicObjects.Length > 1 )
			{
				foreach( var musicObject in musicObjects )
				{
					if ( musicObject.GetComponent<ZDGGlobalMusic>().instanceTime <= 0 )    Destroy(gameObject);
				}
			}
		}

		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void  Start()
		{
			//Don't destroy this object when loading a new scene
			DontDestroyOnLoad(transform.gameObject);
		}
		
	}
}
