/*
http://www.cgsoso.com/forum-211-1.html

CG搜搜 Unity3d 每日Unity3d插件免费更新 更有VIP资源！

CGSOSO 主打游戏开发，影视设计等CG资源素材。

插件如若商用，请务必官网购买！

daily assets update for try.

U should buy the asset from home store if u use it in your project!
*/

using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;

namespace ZombieDriveGame
{
	/// <summary>
	/// Includes functions for loading levels and URLs. It's intended for use with UI Buttons
	/// </summary>
	public class ZDGLoadLevel : MonoBehaviour
	{
        [Tooltip("How many seconds to wait before loading a level or URL")]
        public float loadDelay = 0;

        [Tooltip("The name of the URL to be loaded")]
        public string urlName = "";

        [Tooltip("The name of the level to be loaded")]
        public string levelName = "";

        [Tooltip("The sound that plays when loading/restarting/etc")]
        public AudioClip soundLoad;

        [Tooltip("The tag of the source object from which sounds play")]
        public string soundSourceTag = "GameController";

        [Tooltip("The source object from which sounds play. You can assign this from the scene")]
        public GameObject soundSource;

		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void Start()
		{
		    // If there is no sound source assigned, try to assign it from the tag name
			if ( !soundSource && GameObject.FindGameObjectWithTag(soundSourceTag) )    soundSource = GameObject.FindGameObjectWithTag(soundSourceTag);
		}

		/// <summary>
		/// Loads the URL.
		/// </summary>
		/// <param name="urlName">URL/URI</param>
		public void LoadURL()
		{
            Time.timeScale = 1;

			// If there is a sound, play it from the source
			if ( soundSource && soundLoad )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundLoad);

			// Execute the function after a delay
			Invoke("ExecuteLoadURL", loadDelay);
		}

		/// <summary>
		/// Executes the load URL function
		/// </summary>
		void ExecuteLoadURL()
		{
			Application.OpenURL(urlName);
		}
	
		/// <summary>
		/// Loads the level.
		/// </summary>
		/// <param name="levelName">Level name.</param>
		public void LoadLevel(int index)
		{
			Time.timeScale = 1;

			// If there is a sound, play it from the source
			if ( soundSource && soundLoad )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundLoad);

			if (SceneManager.GetActiveScene().buildIndex != 0)
			{
				AdManager.instance.ShowInterstitial();
			}

			switch (index)
            {
				case 0:
					// Execute the function after a delay
					Invoke("MainMenu", loadDelay);
					break;
				case 1:
					// Execute the function after a delay
					Invoke("GameLevel", loadDelay);
					break;
            }
		}

		/// <summary>
		/// Executes the Load Level function
		/// </summary>
		void GameLevel()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene(1);
		}
		void MainMenu()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene(0);
		}

		/// <summary>
		/// Restarts the current level.
		/// </summary>
		public void RestartLevel()
		{
			Time.timeScale = 1;

			AdManager.instance.ShowInterstitial();

			// If there is a sound, play it from the source
			if ( soundSource && soundLoad )    soundSource.GetComponent<AudioSource>().PlayOneShot(soundLoad);

			// Execute the function after a delay
			Invoke("ExecuteRestartLevel", loadDelay);
		}
		
		/// <summary>
		/// Executes the Load Level function
		/// </summary>
		void ExecuteRestartLevel()
		{
			Time.timeScale = 1;
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
	}
}
