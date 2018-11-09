/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Provide functions to load a scene or url
 */

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provide functions to load a scene or url
/// </summary>
[RequireComponent(typeof(PauseMenu))]
public class SceneAndURLLoader : MonoBehaviour
{
    PauseMenu pauseMenu;

    private void Awake ()
    {
        pauseMenu = GetComponent<PauseMenu>();
    }

    /// <summary>
    /// Load a scene by its name
    /// </summary>
    /// <param name="sceneName"></param>
    public void sceneLoad(string sceneName)
	{
        //Put the time defaults values
        Time.timeScale = 1;
        AudioListener.volume = 1;

        SceneManager.LoadScene(sceneName);
	}

    /// <summary>
    /// Load a url with the default browser of system
    /// </summary>
    /// <param name="url"></param>
	public void loadURL(string url)
	{
		Application.OpenURL(url);
	}
}

