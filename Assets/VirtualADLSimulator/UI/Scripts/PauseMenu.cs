/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Manage the show of a pause menu
 */

using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manage the show of a pause menu
/// </summary>
[RequireComponent(typeof(Toggle))]
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// The menu toogle
    /// </summary>
    private Toggle m_MenuToggle;

    /// <summary>
    /// 
    /// </summary>
	private float m_TimeScaleRef = 1f;

    /// <summary>
    /// 
    /// </summary>
    private float m_VolumeRef = 1f;

    /// <summary>
    /// 
    /// </summary>
    private bool m_Paused;


    void Awake()
    {
        //Assign the toggle component
        m_MenuToggle = GetComponent <Toggle> ();
	}

	void Update()
	{
        if (Input.GetButtonUp("Cancel"))
		{
		    m_MenuToggle.isOn = !m_MenuToggle.isOn;
            //Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it
            if (m_MenuToggle.isOn)
            {
                MenuOn();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                MenuOff();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
	}


    /// <summary>
    /// Turn on the menu, that implies stop the time (Affect at FixedUpdate)
    /// and volume
    /// </summary>
    private void MenuOn()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;
        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;

        m_Paused = true;
    }


    /// <summary>
    /// Turn off the menu
    /// </summary>
    public void MenuOff()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        m_Paused = false;
    }

    /// <summary>
    /// Set the toggle in on or of 
    /// </summary>
    public void OnMenuStatusChange()
    {
        if (m_MenuToggle.isOn && !m_Paused)
        {
            MenuOn();
        }
        else if (!m_MenuToggle.isOn && m_Paused)
        {
            MenuOff();
        }
    }

    /// <summary>
    /// Quit app and go to desktop
    /// </summary>
    public void QuitApp()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
		        Application.Quit();
        #endif
    }


}
