using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ListSavedGames : MonoBehaviour {

    public GridLayoutGroup content;
    public GameObject saveGameDetails;
    /// <summary>
    /// 
    /// </summary>
    [Tooltip("Savegame path relative to application data path")]
    public string savegamePath;


    // Use this for initialization
    void Start()
    {
        //savegamePath = Application.persistentDataPath;
        string str = Application.persistentDataPath;

        str += "/" + savegamePath;
        string savegamePathCopy = str;

        foreach (var filename in Directory.GetFiles(savegamePathCopy))
        {
            GameObject saveGameDetail = Instantiate(saveGameDetails, content.transform);
            SavegameInfo saveGameInfo = JsonConvert.DeserializeObject<SavegameInfo>(File.ReadAllLines(filename)[0]);
            saveGameDetail.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = saveGameInfo.name;
            saveGameDetail.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = saveGameInfo.createdTime;
            saveGameDetail.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = saveGameInfo.description;
            saveGameDetail.GetComponent<Button>().onClick.AddListener(delegate
            {
       
                    FindObjectOfType<SceneAndURLLoader>().sceneLoad("MainScene", savegamePath + "\\" + filename.Split('\\')[1]);

            });
           
        }
    }

	// Update is called once per frame
	void Update () {
		
	}
}
