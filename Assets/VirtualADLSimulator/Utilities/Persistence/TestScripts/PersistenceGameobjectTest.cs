using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PersistenceGameobjectTest : MonoBehaviour {

    
    PersistenceInfoTest pIT = new PersistenceInfoTest();

    public int id = -1;
    //public string name;
    public PersistenceTransform persistenceTransform = new PersistenceTransform();
    public PersistenceMeshFilter persistenceMeshFilter = new PersistenceMeshFilter();
    /*
    public PersistenceMeshRenderer persistenceMeshRenderer;
    public PersistenceCollider persistenceCollider;
    public PersistenceSensFloorUnderlaySfLr persistenceSensFloorUnderlaySfLr;
    public PersistenceSensFloorUnderlayMatLr persistenceSensFloorUnderlayMatLr;
    public PersistenceCapacitiveProximitySensor persistenceCapacitiveProximitySensor;
    */

    // Use this for initialization
    void Start () {

        persistenceTransform.tag = "peasoTag";

        pIT.serializableComponents.Add(persistenceMeshFilter);
        pIT.serializableComponents.Add(persistenceTransform);

        foreach(var i in GetComponents<Component>())
        {
            Debug.Log(i);
        }

        //Debug.Log();
        /*

        string json = JsonConvert.SerializeObject(pIT);

        Debug.Log(json);
        PersistenceInfoTest pit = JsonConvert.DeserializeObject<PersistenceInfoTest>(json);
  
        foreach(JObject i in pit.serializableComponents)
            Debug.Log(i.GetValue("Type"));
            */
    }
	
}
