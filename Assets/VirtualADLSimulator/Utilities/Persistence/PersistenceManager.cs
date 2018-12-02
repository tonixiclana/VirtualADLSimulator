using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class PersistenceManager : MonoBehaviour {


     public List<PersistenceGameobject> persistenceGameobjects;
    public List<GameObject> gmL = new List<GameObject>();

    /// <summary>
    /// The filebrowser object relational with this registryactivitymanager
    /// </summary>
    [Tooltip("The filebrowser object relational with this registryactivitymanager")]
    public FileBrowser _fileBrowser;

    /// <summary>
    /// TMP_InputField to write the name of savegame to store
    /// </summary>
    [Tooltip("TMP_InputField to write the name of savegame to store")]
    public TMP_InputField _nameOfSavegameInput;

    /// <summary>
    /// TMP_InputField to write the description of Savegame to store
    /// </summary>
    [Tooltip("TMP_InputField to write the description of Savegame to store")]
    public TMP_InputField _descriptionOfSavegameInput;

    /// <summary>
    /// Button to start the record
    /// </summary>
    [Tooltip("Button to save the game")]
    public Button _savegameButton;

    /// <summary>
    /// Button to stop the record
    /// </summary>
    [Tooltip("Button to remove game")]
    public Button _removegameButton;

    /// <summary>
    /// Button to load the game
    /// </summary>
    [Tooltip("Button to remove game")]
    public Button _loadgameButton;

    // Use this for initialization
    void Start () {

        // Create the savegame directory and load the example gameplay if the folder dont exists
        if (!Directory.Exists(Application.persistentDataPath + "/savegames"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/savegames");
            string str = Resources.Load<TextAsset>("DefaultSavegames/CEATIC_Default").text;
            File.WriteAllText(Application.persistentDataPath + "/savegames/CEATIC_default.txt", str);

        }
        else
        {
            string str = Resources.Load<TextAsset>("DefaultSavegames/CEATIC_Default").text;
            File.WriteAllText(Application.persistentDataPath + "/savegames/CEATIC_Default.txt", str);

        }

        //Add listener to savegame button
        _savegameButton.onClick.AddListener(delegate
        {

            _fileBrowser.deleteFileInActualPath(_nameOfSavegameInput.text + ".txt");

            //Create or append in a file add the header with the information of this activity

            SavegameInfo savegameInfo = new SavegameInfo();

            savegameInfo.createdTime = System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss");
            savegameInfo.name = (_nameOfSavegameInput.text != "")? _nameOfSavegameInput.text : savegameInfo.createdTime;
            savegameInfo.description = _descriptionOfSavegameInput.text;


            _fileBrowser.editFileInActualPath(
                JsonUtility.ToJson(savegameInfo)
                , _nameOfSavegameInput.text + ".txt"

            );

            saveGameObjectsInJSON(_nameOfSavegameInput.text + ".txt");
            _fileBrowser.refreshGridContentOfActualPath();
            updateFileBrowserItemListeners();
        });

        //Add listener to removegame button
        _removegameButton.onClick.AddListener(delegate
        {

            _fileBrowser.deleteFileInActualPath(_nameOfSavegameInput.text + ".txt");

            _nameOfSavegameInput.text = "";
            _descriptionOfSavegameInput.text = "";
            _fileBrowser.refreshGridContentOfActualPath();
            updateFileBrowserItemListeners();
        });


        //Add listener to removegame button
        _loadgameButton.onClick.AddListener(delegate
        {

            //loadPersistenceGameobjects(_nameOfSavegameInput.text + ".txt");
            loadPersistenceGameobjects(_nameOfSavegameInput.text + ".txt");
        });


        _fileBrowser.refreshGridContent(Application.persistentDataPath + "/savegames");
        updateFileBrowserItemListeners();


    }
	
    public void updateFileBrowserItemListeners()
    {
        foreach (FileBrowserItem item in _fileBrowser.getFileItems())
        {
            
            item.GetComponent<Button>().onClick.AddListener(delegate
            {
                _nameOfSavegameInput.text = JsonUtility.FromJson<SavegameInfo>(_fileBrowser.readFileInActualPath(item.GetComponentInChildren<TextMeshProUGUI>().text)[0]).name;
                _descriptionOfSavegameInput.text = JsonUtility.FromJson<SavegameInfo>(_fileBrowser.readFileInActualPath(item.GetComponentInChildren<TextMeshProUGUI>().text)[0]).description;
                _loadgameButton.gameObject.SetActive(true);
                _removegameButton.gameObject.SetActive(true);
            });
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        //persistenceGameobjects = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();
	}
    /*
    public static PersistenceGameobject GetPersistenceGameobject(int id)
    {
        var persistenceGameobjects = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();
        var targetGameObject = persistenceGameobjects.Find(delegate (PersistenceGameobject pGm) { return pGm.id == id; });
        return targetGameObject ;

        
    }

*/
    public List<PersistenceGameobject> getPersistenceGameobjects()
    {
        persistenceGameobjects.Clear();

        foreach(var gm in gmL)
        {
            persistenceGameobjects.Add(gm.GetComponent<PersistenceGameobject>());
        }

        
        persistenceGameobjects.Sort(
            delegate(PersistenceGameobject c1, PersistenceGameobject c2) 
            {
                return c1.id.CompareTo(c2.id);
            }
        );
        
        return persistenceGameobjects;
    }

    public void saveGameObjectsInJSON(string path)
    {
        gmL.Clear();

        foreach(var gm in FindObjectsOfType<PersistenceGameobject>())
        {
            gmL.Add(gm.gameObject);
        }
   

        foreach (PersistenceGameobject persistanceGameObject in getPersistenceGameobjects())
        {
            persistanceGameObject.updatePersistenceInfo();
            //var json = JsonUtility.ToJson(persistanceGameObject.persistenceInfo);
            var json = JsonConvert.SerializeObject(persistanceGameObject.persistenceInfo);
            //Debug.Log(json);



            _fileBrowser.editFileInActualPath(json, path);
        }

    }


    public void resetScene()
    {
        foreach (var pGm in gmL)
            Destroy(pGm);

        gmL.Clear();
        PersistenceGameobject.ResetIdCounter();
        if (persistenceGameobjects != null)
            persistenceGameobjects.Clear();
    }

    public void loadPersistenceGameobjects(string path)
    {
        var appPath = Application.persistentDataPath + "/savegames/" + path;
        string[] file = File.ReadAllLines(appPath);

        resetScene();

        foreach (var line in file)
        {
            if(line != "")
            {

                PersistenceInfo persistenceInfo = JsonConvert.DeserializeObject<PersistenceInfo>(line);

                

                //PersistenceInfo persistenceInfo = JsonUtility.FromJson<PersistenceInfo>(line);
                if (persistenceInfo.id != -1)
                {

                    GameObject gm = new GameObject();
                    gm.AddComponent<PersistenceGameobject>().persistenceInfo = persistenceInfo;
                    gm.GetComponent<PersistenceGameobject>().loaded = true;
                    gmL.Add(gm);

                }
            }
 

        }

        int i = 0;
        foreach (var line in file)
        {
            if (line != "")
            {
                PersistenceInfo persistenceInfo = JsonConvert.DeserializeObject<PersistenceInfo>(line);
                if (persistenceInfo.id != -1)
                {

                    gmL[i].GetComponent<PersistenceGameobject>().id = persistenceInfo.id;
                    gmL[i].GetComponent<PersistenceGameobject>().name = persistenceInfo.name;

                    gmL[i].GetComponent<PersistenceGameobject>().loadPersistenceTransform();
                    gmL[i].GetComponent<PersistenceGameobject>().loadPersistenceComponents();
                   
                    i++;
                }
            }

           
        }


    }


}
