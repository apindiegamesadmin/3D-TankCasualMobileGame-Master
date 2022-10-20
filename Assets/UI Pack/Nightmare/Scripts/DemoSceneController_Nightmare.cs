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

using System.Collections.Generic;
using UnityEngine.UI;
#endregion

// ########################################
// DemoSceneController_Nightmare class
//
// Show/hide objects and respond to user inputs.
//
// Note this sample script is attached with "-DemoSceneController-" object in "UI Pack/Scenes/Nightmare - Demo" scene.
// ########################################

public class DemoSceneController_Nightmare : MonoBehaviour
{
	// ########################################
	// Variables
	// ########################################

	#region Variables

	// Store Layouts/Prefabs types
	public enum UIToggle
	{
		Layouts,
		Prefabs
	}

	// Text
	public Text m_Title;
	public Text m_Order;

	// Background
	public GameObject m_Backgrounds = null;

	// Layouts
	public GameObject m_Layouts;
	public Button m_LayoutButton;
	public int m_LayoutIndex = 0;

	// Prefabs
	public GameObject m_Prefabs;
	public Button m_PrefabButton;
	public int m_PrefabIndex = 0;

	// Type of displayed object.
	public UIToggle m_UIToggle = UIToggle.Layouts;

	GameObject m_ControlPanel = null;

	#endregion // Variables

	// ########################################
	// MonoBehaviour Functions
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.html
	// ########################################

	#region MonoBehaviour

	// Start is called on the frame when a script is enabled just before any of the Update methods is called the first time.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
	void Start()
	{
		UpdateLayoutAndPrefabButtons();
		UpdateDisplay();
	}

	// Update is called every frame, if the MonoBehaviour is enabled.
	// http://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
	void Update()
	{

#if UNITY_EDITOR || UNITY_STANDALONE
		// Press C key to show/hide Control panel
		if (Input.GetKeyUp(KeyCode.C))
		{
			if (m_ControlPanel == null)
				m_ControlPanel = GameObject.Find("Control Panel");
			if (m_ControlPanel)
			{
				if (m_ControlPanel.activeSelf)
				{
					m_ControlPanel.SetActive(false);
					UpdateLayoutAndPrefabButtons();
					UpdateDisplay();
				}
				else
				{
					m_ControlPanel.SetActive(true);
				}
			}
		}
		// Press Up/Down arrow keys to switch demo type
		if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
		{
			OnToggleButton();
			DemoSoundController_Nightmare.Instance.Play_SoundToggleDemoTypeButton();
		}
		// Press Left arrow key to show previous page
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			OnPreviousButton();
			DemoSoundController_Nightmare.Instance.Play_SoundNextPreviousButton();
		}
		// Press Right arrow key to show next page
		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			OnNextButton();
			DemoSoundController_Nightmare.Instance.Play_SoundNextPreviousButton();
		}
		// Press B key to show/hide Background
		if (Input.GetKeyUp(KeyCode.B))
		{
			OnToggleBackground();
		}
		// Press M key to on/off Music
		if (Input.GetKeyUp(KeyCode.M))
		{
			OnToggleBGMusic();
		}
#endif

	}

	#endregion // MonoBehaviour

	// ########################################
	// UI Responder functions
	// ########################################

	#region UI Responder

	// User presses Previous Button
	public void OnPreviousButton()
	{
		if (m_UIToggle == UIToggle.Layouts)
		{
			m_LayoutIndex--;
			if (m_LayoutIndex < 0)
				m_LayoutIndex = m_Layouts.transform.childCount - 1;

			//Debug.Log("m_LayoutIndex=" + m_LayoutIndex);
		}
		else
		{
			m_PrefabIndex--;
			if (m_PrefabIndex < 0)
				m_PrefabIndex = m_Prefabs.transform.childCount - 1;

			//Debug.Log("m_PrefabIndex=" + m_PrefabIndex);
		}

		ShowCurrentDisplayedObjects();
	}

	// User presses Next Button
	public void OnNextButton()
	{
		if (m_UIToggle == UIToggle.Layouts)
		{
			m_LayoutIndex++;
			if (m_LayoutIndex >= m_Layouts.transform.childCount)
				m_LayoutIndex = 0;

			//Debug.Log("m_LayoutIndex=" + m_LayoutIndex);
		}
		else
		{
			m_PrefabIndex++;
			if (m_PrefabIndex >= m_Prefabs.transform.childCount)
				m_PrefabIndex = 0;

			//Debug.Log("m_PrefabIndex=" + m_PrefabIndex);
		}

		ShowCurrentDisplayedObjects();
	}

	// User presses Toggle display type button
	public void OnToggleButton()
	{
		if (m_UIToggle == UIToggle.Layouts)
		{
			m_UIToggle = UIToggle.Prefabs;
		}
		else
		{
			m_UIToggle = UIToggle.Layouts;
		}

		UpdateLayoutAndPrefabButtons();
		UpdateDisplay();
	}

	public void OnToggleBackground()
	{
		if (m_Backgrounds)
		{
			if (m_Backgrounds.activeSelf)
			{
				m_Backgrounds.SetActive(false);
			}
			else
			{
				m_Backgrounds.SetActive(true);
			}
		}
	}

	// User presses Toggle On/Off Music button
	public void OnToggleBGMusic()
	{
		if (DemoSoundController_Nightmare.Instance.m_MusicIsOn == false)
			DemoSoundController_Nightmare.Instance.SetMusic(true);
		else
			DemoSoundController_Nightmare.Instance.SetMusic(false);
	}

	// Toggle Layouts/Prefabs objects
	void UpdateLayoutAndPrefabButtons()
	{
		if (m_UIToggle == UIToggle.Layouts)
		{
			m_LayoutButton.interactable = false;
			m_LayoutButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			m_PrefabButton.interactable = true;
			m_PrefabButton.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			m_Title.color = new Color(0.3f, 0.6f, 1.0f, 1.0f);
		}
		else
		{
			m_LayoutButton.interactable = true;
			m_LayoutButton.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			m_PrefabButton.interactable = false;
			m_PrefabButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
			m_Title.color = new Color(0.6f, 1.0f, 0.6f, 1.0f);
		}
	}

	// Show/Hide Layouts and Prefabs objects
	void UpdateDisplay()
	{
		if (m_UIToggle == UIToggle.Layouts)
		{
			m_Layouts.SetActive(true);
			m_Prefabs.SetActive(false);
		}
		else
		{
			m_Layouts.SetActive(false);
			m_Prefabs.SetActive(true);
		}
		ShowCurrentDisplayedObjects();
	}

	#endregion // UI Responder

	// ########################################
	// Show/hide object functions
	// ########################################

	#region Show/hide object

	// Show the children objects of Layouts/Prefabs object according to m_PrefabButton/m_LayoutButton.
	void ShowCurrentDisplayedObjects()
	{
		int index = 0;
		Transform trans;
		if (m_UIToggle == UIToggle.Layouts)
		{
			index = m_LayoutIndex;
			trans = m_Layouts.transform;
		}
		else
		{
			index = m_PrefabIndex;
			trans = m_Prefabs.transform;
		}

		int i = 0;
		foreach (Transform child in trans)
		{
			if (i == index)
			{
				child.gameObject.SetActive(true);
				m_Title.text = child.name;
				m_Order.text = (index + 1).ToString() + "/" + trans.childCount.ToString();
			}
			else
			{
				child.gameObject.SetActive(false);
			}

			i++;
		}
	}

	#endregion // Show/hide object
}
