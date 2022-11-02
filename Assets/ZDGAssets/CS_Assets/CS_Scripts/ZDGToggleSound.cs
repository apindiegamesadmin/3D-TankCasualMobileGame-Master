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
using UnityEngine.UI;

namespace ZombieDriveGame.Types
{
	/// <summary>
	/// Toggles a sound source when clicked on. It also records the sound state (on/off) in a PlayerPrefs. 
	/// In order to detect clicks you need to attach this script to a UI Button and set the proper OnClick() event.
	/// </summary>
	public class ZDGToggleSound : MonoBehaviour
	{
        [Tooltip("The tag of the sound object")]
        public string soundObjectTag = "Sound";

		public Sprite buttonOn;
		public Sprite buttonOff;

        [Tooltip("The source of the sound")]
        public Transform soundObject;

        [Tooltip("The PlayerPrefs name of the sound")]
        public string playerPref = "SoundVolume";
	
		// The index of the current value of the sound
		internal float currentState = 1;
		Image buttonImage;
	
		/// <summary>
		/// Awake is called when the script instance is being loaded.
		/// Awake is used to initialize any variables or game state before the game starts. Awake is called only once during the 
		/// lifetime of the script instance. Awake is called after all objects are initialized so you can safely speak to other 
		/// objects or query them using eg. GameObject.FindWithTag. Each GameObject's Awake is called in a random order between objects. 
		/// Because of this, you should use Awake to set up references between scripts, and use Start to pass any information back and forth. 
		/// Awake is always called before any Start functions. This allows you to order initialization of scripts. Awake can not act as a coroutine.
		/// </summary>
		void Awake()
		{
			buttonImage = GetComponent<Image>();
			if ( !soundObject && soundObjectTag != string.Empty )    soundObject = GameObject.FindGameObjectWithTag(soundObjectTag).transform;

			// Get the current state of the sound from PlayerPrefs
			if( soundObject )
				currentState = PlayerPrefs.GetFloat(playerPref, soundObject.GetComponent<AudioSource>().volume);
			else   
				currentState = PlayerPrefs.GetFloat(playerPref, currentState);
		
			// Set the sound in the sound source
			SetSound();
		}
	
		/// <summary>
		/// Sets the sound volume
		/// </summary>
		void SetSound()
		{
			if ( !soundObject && soundObjectTag != string.Empty )    soundObject = GameObject.FindGameObjectWithTag(soundObjectTag).transform;

			// Set the sound in the PlayerPrefs
			PlayerPrefs.SetFloat(playerPref, currentState);

			// Update the graphics of the button image to fit the sound state
			if (currentState == 1)
				buttonImage.sprite = buttonOn;
			else
				buttonImage.sprite = buttonOff;

			// Set the value of the sound state to the source object
			if( soundObject ) 
				soundObject.GetComponent<AudioSource>().volume = currentState;
		}
	
		/// <summary>
		/// Toggle the sound. Cycle through all sound modes and set the volume and icon accordingly
		/// </summary>
		public void ToggleSound()
		{
			currentState = 1 - Mathf.Round(currentState);
		
			SetSound();
		}
	
		/// <summary>
		/// Starts the sound source.
		/// </summary>
		void StartSound()
		{
			if( soundObject )
				soundObject.GetComponent<AudioSource>().Play();
		}
	}
}
