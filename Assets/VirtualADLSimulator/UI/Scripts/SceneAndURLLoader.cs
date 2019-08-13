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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Provide functions to load a scene or url
/// </summary>
public class SceneAndURLLoader : MonoBehaviour
{
    /// <summary>
    /// The actual loaded savegame
    /// </summary>
    [Tooltip("The actual loaded savegame")]
    public string loadedSavegame = "";


    private SceneAndURLLoader instance = null;

    public RectTransform layoutLoadingScene;

    private void Update()
    {
        
    }

    private void Awake ()
    {
        
        // Keep the component in all scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            string str;
            // Create the templates directory and load the example gameplay if the folder dont exists
            if (!Directory.Exists(Application.persistentDataPath + "/templates"))
            {
                Directory.CreateDirectory(Application.persistentDataPath + "/templates");
                str = Resources.Load<TextAsset>("DefaultTemplates/CEATIC_Default").text;
                File.WriteAllText(Application.persistentDataPath + "/templates/CEATIC_Default.txt", str);

            }
            else
            {
                str = Resources.Load<TextAsset>("DefaultTemplates/CEATIC_Default").text;
                File.WriteAllText(Application.persistentDataPath + "/templates/CEATIC_Default.txt", str);

            }

            // Create the savegame directory and load the example gameplay if the folder dont exists
            if (!Directory.Exists(Application.persistentDataPath + "/savegames"))
                Directory.CreateDirectory(Application.persistentDataPath + "/savegames");

            // Load the defaults savegames
            if (!File.Exists(Application.persistentDataPath + "/savegames/Default - Test Sensores 0.txt"))
            {
                //Debug.Log("Entra");
                str = Resources.Load<TextAsset>("DefaultSavegames/CEATIC_Default").text;
                File.WriteAllText(Application.persistentDataPath + "/savegames/CEATIC_Default.txt", str);
            }

            // Create the registers directory
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VirtualADLSimulator"))
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VirtualADLSimulator");

            foreach (string f in Directory.GetFiles(Application.persistentDataPath + "/savegames"))
            {
                if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VirtualADLSimulator\" + f.Replace(".txt", "")))
                    Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VirtualADLSimulator\" + f.Split('\\').ToList().Last().Replace(".txt", ""));
            }
        } 
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

        if(saveGameName != null)
            loadedSavegame = saveGameName;

        StartCoroutine(asyncSceneLoad(sceneName, saveGameName));
    }

    IEnumerator asyncSceneLoad(string sceneName, string saveGameName)
    {
        AsyncOperation asyncLoadLevel = SceneManager.LoadSceneAsync(sceneName);
        if(layoutLoadingScene != null)
            layoutLoadingScene.gameObject.SetActive(true);
        while (!asyncLoadLevel.isDone)
        {
            Debug.Log("Loading Scene...");
            yield return new WaitForSeconds(0.5f);
        }

        if (saveGameName != null)
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

