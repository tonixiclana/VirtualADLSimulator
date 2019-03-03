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
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Provide functions to load a scene or url
/// </summary>
[RequireComponent(typeof(PauseMenu))]
public class SceneAndURLLoader : MonoBehaviour
{
    PauseMenu pauseMenu;
    public RectTransform layoutLoadingScene;

    private void Awake ()
    {
        DontDestroyOnLoad(this.gameObject);
        pauseMenu = GetComponent<PauseMenu>();

        // Create the templates directory and load the example gameplay if the folder dont exists
        if (!Directory.Exists(Application.persistentDataPath + "/templates"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/templates");
            string str = Resources.Load<TextAsset>("DefaultSavegames/CEATIC_Default").text;
            File.WriteAllText(Application.persistentDataPath + "/templates/CEATIC_default.txt", str);

        }
        else
        {
            string str = Resources.Load<TextAsset>("DefaultSavegames/CEATIC_Default").text;
            File.WriteAllText(Application.persistentDataPath + "/templates/CEATIC_Default.txt", str);

        }

        // Create the savegame directory and load the example gameplay if the folder dont exists
        if (!Directory.Exists(Application.persistentDataPath + "/savegames"))
            Directory.CreateDirectory(Application.persistentDataPath + "/savegames");

    }
    
    /// <summary>
    /// Load a scene by its name
    /// </summary>
    /// <param name="sceneName"></param>
    public void sceneLoad(string sceneName)
	{
        sceneLoad(sceneName, null);
	}
    
    /// <summary>
    /// Load a scena and preload a set of persistance gameobjects
    /// </summary>
    /// <param name="sceneName">The name of scene to load</param>
    /// <param name="saveGameName">The save game file that contains the persistence gameobjects</param>
    public void sceneLoad(string sceneName, string saveGameName)
    {
        //Put the time defaults values
        Time.timeScale = 1;
        AudioListener.volume = 1;

        StartCoroutine(asyncSceneLoad(sceneName, saveGameName));
    }

    IEnumerator asyncSceneLoad(string sceneName, string saveGameName)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName);
        if(layoutLoadingScene != null)
            layoutLoadingScene.gameObject.SetActive(true);
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading Scene");
            yield return new WaitForSeconds(0.1f);
        }
        if(saveGameName != null)
        {
            FindObjectOfType<PersistenceManager>().loadPersistenceGameobjects(saveGameName);
            Debug.Log("Objects Loaded");
        }
            

        if (layoutLoadingScene != null)
            layoutLoadingScene.gameObject.SetActive(false);
        yield return null;
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

