using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class PersistenceManager : MonoBehaviour {

    public List<PersistenceGameobject> persistenceGameobjects;

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
        if (File.Exists(path))
            File.Delete(path);

        string ret = "";

        foreach (PersistenceGameobject gameObject in getPersistenceGameobjects())
        {
            var json = JsonUtility.ToJson(gameObject.persistenceInfo);
            
            Debug.Log(json);
            
            

            /*if (!File.Exists(path))
                ret = json;

            else*/
                ret +=  json + "\n";
             
        }
        File.WriteAllText(path, ret);
    }

    public void loadPersistenceGameobjects(string path)
    {
        
        string[] file = File.ReadAllLines(path);
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
