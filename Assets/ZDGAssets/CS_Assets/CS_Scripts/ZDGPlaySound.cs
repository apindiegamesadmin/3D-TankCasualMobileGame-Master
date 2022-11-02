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
	/// Plays a sound from an audio source.
	/// </summary>
	public class ZDGPlaySound : MonoBehaviour
	{
        [Tooltip("The sound to play")]
        public AudioClip sound;

        [Tooltip("The tag of the sound source")]
        public string soundSourceTag = "Sound";

        [Tooltip("Play the sound immediately when the object is activated")]
        public bool playOnStart = true;
        
		/// <summary>
		/// Start is only called once in the lifetime of the behaviour.
		/// The difference between Awake and Start is that Start is only called if the script instance is enabled.
		/// This allows you to delay any initialization code, until it is really needed.
		/// Awake is always called before any Start functions.
		/// This allows you to order initialization of scripts
		/// </summary>
		void Start()
		{
			if( playOnStart == true )    PlaySound();
		}
	
        /// <summary>
		/// Plays the sound
		/// </summary>
		public void PlaySound()
        {
            // If there is a sound source tag and audio to play, play the sound from the audio source based on its tag
            if (soundSourceTag != string.Empty && sound)
            {
                // Play the sound
                GameObject.FindGameObjectWithTag(soundSourceTag).GetComponent<AudioSource>().PlayOneShot(sound);
            }
        }
    }
}
