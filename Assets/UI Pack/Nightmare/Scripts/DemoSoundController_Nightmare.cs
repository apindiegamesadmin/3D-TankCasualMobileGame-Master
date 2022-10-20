// UI Pack : Nightmare
// Version: 1.1.5
// Compatilble: Unity 5.5.1 or higher, see more info in Readme.txt file.
//
// Developer:			Gold Experience Team (https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:4162)
//
// Unity Asset Store:	https://www.assetstore.unity3d.com/en/#!/content/49146
//
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

using UnityEngine;
using System.Collections;

#endregion

// ######################################################################
// DemoSoundController_Nightmare class
//
// Contains AudioClips to play bg music and sounds
//
// Note this sample script is attached with "-DemoSceneController-" object in "UI Pack/Scenes/Nightmare - Demo" scene.
// ######################################################################

public class DemoSoundController_Nightmare : MonoBehaviour
{

	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Private reference which can be accessed by this class only
	private static DemoSoundController_Nightmare instance;

	// Public static reference that can be accesd from anywhere
	public static DemoSoundController_Nightmare Instance
	{
		get
		{
			// Check if instance has not been set yet and set it it is not set already
			// This takes place only on the first time usage of this reference
			if (instance == null)
			{
				instance = GameObject.FindObjectOfType<DemoSoundController_Nightmare>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	// Max number of AudioSource components
	public int m_MaxAudioSource = 8;

	// AudioClip component for music
	public AudioClip m_Music = null;

	// AudioClip component for buttons
	public AudioClip m_NextPreviousButton = null;
	public AudioClip m_ToggleDemoTypeButton = null;
	public AudioClip m_ToggleBGMusicButton = null;

	// Sound volume
	//[HideInInspector]
	public float m_SoundVolume = 0.75f;

	// Music volume
	//[HideInInspector]
	public float m_MusicVolume = 0.75f;
	
	// On/Off Music
	[HideInInspector]
	public bool m_MusicIsOn = true;

	#endregion Variables

	// ########################################
	// MonoBehaviour functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Awake is called when the script instance is being loaded.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
	void Awake()
	{
		if (instance == null)
		{
			// Make the current instance as the singleton
			instance = this;

			// Make it persistent  
			DontDestroyOnLoad(this);
		}
		else
		{
			// If more than one singleton exists in the scene find the existing reference from the scene and destroy it
			if (this != instance)
			{
				InitAudioListener();
				Destroy(this.gameObject);
			}
		}
	}

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		// Initial AudioListener
		InitAudioListener();

		// Automatically play music if it is not playing
		if (IsMusicPlaying() == false)
		{
			// Play music
			Play_Music();
		}

		SetSoundVolume(m_SoundVolume);
		SetMusicVolume(m_MusicVolume);
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{
	}

	#endregion // MonoBehaviour

	// ########################################
	// AudioListener functions
	// ########################################

	#region AudioListener

	// Initial AudioListener
	// This function remove all AudioListener in other objects then it adds new one this object.
	void InitAudioListener()
	{
		// Destroy other's AudioListener components
		AudioListener[] pAudioListenerToDestroy = GameObject.FindObjectsOfType<AudioListener>();
		foreach (AudioListener child in pAudioListenerToDestroy)
		{
			if (child.gameObject.GetComponent<DemoSoundController_Nightmare>() == null)
			{
				Destroy(child);
			}
		}

		// Adds new AudioListener to this object
		AudioListener pAudioListener = gameObject.GetComponent<AudioListener>();
		if (pAudioListener == null)
		{
			pAudioListener = gameObject.AddComponent<AudioListener>();
		}
	}

	#endregion // AudioListener

	// ########################################
	// Music functions
	// ########################################

	#region Music

	// Play music
	void PlayMusic(AudioClip pAudioClip)
	{
		// Return if the given AudioClip is null
		if (pAudioClip == null)
			return;

		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			// Look for an AudioListener component that is not playing background music or sounds.
			bool IsPlaySuccess = false;
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					// Play music
					if (pAudioSourceList[i].isPlaying == false)
					{
						pAudioSourceList[i].loop = true;
						pAudioSourceList[i].clip = pAudioClip;
						pAudioSourceList[i].ignoreListenerVolume = true;
						pAudioSourceList[i].playOnAwake = false;
						pAudioSourceList[i].Play();
						break;
					}
				}
			}

			// If there is not enough AudioListener to play AudioClip then add new one and play it
			if (IsPlaySuccess == false && pAudioSourceList.Length < 16)
			{
				AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
				pAudioSource.rolloffMode = AudioRolloffMode.Linear;
				pAudioSource.loop = true;
				pAudioSource.clip = pAudioClip;
				pAudioSource.ignoreListenerVolume = true;
				pAudioSource.playOnAwake = false;
				pAudioSource.Play();
			}
		}
	}

	// Set music volume between 0.0 to 1.0
	public void SetMusicVolume(float volume)
	{
		m_MusicVolume = volume;
		UpdateMusicVolumn(volume);
	}

	// Set music volume between 0.0 to 1.0
	public void SetMusic(bool IsOn)
	{
		m_MusicIsOn = IsOn;
		if(m_MusicIsOn==true)
			UpdateMusicVolumn(m_MusicVolume);
		else
			UpdateMusicVolumn(0.0f);
	}

	// Set music volume between 0.0 to 1.0
	public void UpdateMusicVolumn(float volume)
	{
		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].ignoreListenerVolume)
					{
						pAudioSourceList[i].volume = volume;
					}
				}
			}
		}
	}

	// If music is playing, return true.
	public bool IsMusicPlaying()
	{
		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].ignoreListenerVolume == true)
					{
						if (pAudioSourceList[i].isPlaying == true)
						{
							return true;
						}
					}
				}
			}
		}

		return false;
	}

	// Play music
	public void Play_Music()
	{
		PlayMusic(m_Music);
	}

	#endregion // Music

	// ########################################
	// Sound functions
	// ########################################

	#region Sound

	// Play sound one shot
	void PlaySoundOneShot(AudioClip pAudioClip)
	{

		// Return if the given AudioClip is null
		if (pAudioClip == null)
			return;

		// We wait for a while after scene loaded
		if (Time.timeSinceLevelLoad < 1.5f)
			return;

		// Look for an AudioListener component that is not playing background music or sounds.
		AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
		if (pAudioListener != null)
		{
			bool IsPlaySuccess = false;
			AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
			if (pAudioSourceList.Length > 0)
			{
				for (int i = 0; i < pAudioSourceList.Length; i++)
				{
					if (pAudioSourceList[i].isPlaying == false)
					{
						// Play sound
						pAudioSourceList[i].PlayOneShot(pAudioClip);
						break;
					}
				}
			}

			// If there is not enough AudioListener to play AudioClip then add new one and play it
			if (IsPlaySuccess == false && pAudioSourceList.Length < 16)
			{
				// Play sound
				AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
				pAudioSource.rolloffMode = AudioRolloffMode.Linear;
				pAudioSource.playOnAwake = false;
				pAudioSource.PlayOneShot(pAudioClip);
			}
		}
	}

	// Set sound volume between 0.0 to 1.0
	public void SetSoundVolume(float volume)
	{
		m_SoundVolume = volume;
		AudioListener.volume = volume;
	}

	#endregion // Sound

	// ########################################
	// UI Responder functions
	// ########################################

	#region UI Responder

	// Play NextPreviousButton sound
	public void Play_SoundNextPreviousButton()
	{
		PlaySoundOneShot(m_NextPreviousButton);
	}

	// Play ToggleDemoTypeButton sound
	public void Play_SoundToggleDemoTypeButton()
	{
		PlaySoundOneShot(m_ToggleDemoTypeButton);
	}

	// Play ToggleBGMusicSoundButton sound
	public void Play_SoundToggleBGMusicButton()
	{
		PlaySoundOneShot(m_ToggleBGMusicButton);
	}

	#endregion // UI Responder
}
