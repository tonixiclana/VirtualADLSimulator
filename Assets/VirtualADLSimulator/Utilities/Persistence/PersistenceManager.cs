using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PersistenceManager : MonoBehaviour {

    static public List<PersistenceGameobject> persistenceGameobjects;

	// Use this for initialization
	void Start () {



        /*foreach (var obj in Resources.LoadAll<Mesh>("Meshes"))
        {
                Debug.Log(obj.name);
        }*/
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //persistenceGameobjects = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();
	}

    public static PersistenceGameobject GetPersistenceGameobject(int id)
    {
        var persistenceGameobjects = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();
        var targetGameObject = persistenceGameobjects.Find(delegate (PersistenceGameobject pGm) { return pGm.id == id; });
        return targetGameObject ;

        
    }


    public List<PersistenceGameobject> getPersistenceGameobjects()
    {
        persistenceGameobjects = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();
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
        if(!Directory.Exists(Application.persistentDataPath + "/savegames"))
            Directory.CreateDirectory(Application.persistentDataPath + "/savegames");

        var appPath = Application.persistentDataPath + "/savegames/" + path;

        if (File.Exists(appPath))
            File.Delete(appPath);

        string ret = "";

        foreach (PersistenceGameobject persistanceGameObject in getPersistenceGameobjects())
        {
            persistanceGameObject.updatePersistenceInfo();
            var json = JsonUtility.ToJson(persistanceGameObject.persistenceInfo);
            
            Debug.Log(json);
            
            

            /*if (!File.Exists(path))
                ret = json;

            else*/
                ret +=  json + "\n";
             
        }
        File.WriteAllText(appPath, ret);
    }

    public void loadPersistenceGameobjects(string path)
    {
        var appPath = Application.persistentDataPath + "/savegames/" + path;
        string[] file = File.ReadAllLines(appPath);
        List<GameObject> gmL = new List<GameObject>();
        
        foreach (var line in file)
        {

            PersistenceInfo persistenceInfo = JsonUtility.FromJson<PersistenceInfo>(line);
            if(persistenceInfo != null)
            {
                GameObject gm = new GameObject();
                gm.AddComponent<PersistenceGameobject>().loaded = true;
                gmL.Add(gm);
            }

            

        }

        int i = 0;
        foreach (var line in file)
        {

            PersistenceInfo persistenceInfo = JsonUtility.FromJson<PersistenceInfo>(line);
            //gmL[i].GetComponent<PersistenceGameobject>().persistenceInfo = persistenceInfo;
            gmL[i].GetComponent<PersistenceGameobject>().loadPersistenceTransform(persistenceInfo);
            gmL[i].GetComponent<PersistenceGameobject>().loadPersistenceComponents();
            //if(gmL[i].GetComponent<MeshRenderer>() != null) gmL[i].GetComponent<MeshRenderer>().enabled = false;
            //Debug.Log("[" + persistenceInfo.id + "] Persistence Loaded!");
            i++;
        }


    }


}
