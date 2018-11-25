using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class PersistenceGameobject : MonoBehaviour
{
    private static int autoCounter = 0;
    public int id;
    public PersistenceInfo persistenceInfo;
    public bool loaded = false;
    
    private void Awake()
    {
        id = autoCounter;
        autoCounter++;
    }

    // Use this for initialization
    void Start()
    {
        //StartCoroutine(updatePersistenceInfo());
        if (!loaded)
        {

            updatePersistenceInfo();



        }


       
    }

    public void updatePersistenceInfo()
    {
        persistenceInfo.id = id;
        persistenceInfo.name = gameObject.name;
        persistenceInfo.persistenceTransform.layer = gameObject.layer;
        persistenceInfo.persistenceTransform.tag = gameObject.tag;

        if (transform.parent == null || transform.parent.GetComponent<PersistenceGameobject>() == null)
            persistenceInfo.persistenceTransform.parentId = -1;
        else
            persistenceInfo.persistenceTransform.parentId = transform.parent.GetComponent<PersistenceGameobject>().id;

        if (transform.parent != null)
        {
            persistenceInfo.persistenceTransform.goPosition = transform.localPosition;
            persistenceInfo.persistenceTransform.goRotation = transform.localRotation;
            persistenceInfo.persistenceTransform.goScale = transform.localScale;
        }
        else
        {
            persistenceInfo.persistenceTransform.goPosition = transform.position;
            persistenceInfo.persistenceTransform.goRotation = transform.rotation;
            persistenceInfo.persistenceTransform.goScale = transform.localScale;
        }




        if (GetComponent<MeshFilter>() != null)
            persistenceInfo.persistenceMeshFilter.meshPath = GetComponent<MeshFilter>().sharedMesh.name;


        if (GetComponent<MeshRenderer>() != null)
        {
            persistenceInfo.persistenceMeshRenderer.enabled = GetComponent<MeshRenderer>().enabled;
            if(persistenceInfo.persistenceMeshRenderer.mainMaterialPath == "")
                persistenceInfo.persistenceMeshRenderer.mainMaterialPath = GetComponent<MeshRenderer>().sharedMaterial.name;
        }


        if (GetComponent<Collider>() != null)
        {
            persistenceInfo.persistenceCollider.enabled = GetComponent<Collider>().enabled;
            persistenceInfo.persistenceCollider.typeCollider = GetComponent<Collider>().GetType().ToString();
            if (persistenceInfo.persistenceCollider.typeCollider == "UnityEngine.MeshCollider")
                persistenceInfo.persistenceCollider.meshPath = GetComponent<MeshCollider>().sharedMesh.name;

        }

        if (GetComponent<SensFloorUnderlaySfLr>() != null)
        {
            //Debug.Log(GetComponent<Sensor>().GetType());

            persistenceInfo.persistenceSensFloorUnderlaySfLr._code = GetComponent<SensFloorUnderlaySfLr>()._code;
            persistenceInfo.persistenceSensFloorUnderlaySfLr._activationThreshold = GetComponent<SensFloorUnderlaySfLr>()._activationThreshold;
            persistenceInfo.persistenceSensFloorUnderlaySfLr._debug = GetComponent<SensFloorUnderlaySfLr>()._debug;
            persistenceInfo.persistenceSensFloorUnderlaySfLr._exportData = GetComponent<SensFloorUnderlaySfLr>()._exportData;
            persistenceInfo.persistenceSensFloorUnderlaySfLr._frecuency = GetComponent<SensFloorUnderlaySfLr>()._frecuency;
            persistenceInfo.persistenceSensFloorUnderlaySfLr._exportDetailPosition = GetComponent<SensFloorUnderlaySfLr>()._exportDetailPosition;
        }

        if (GetComponent<SensFloorUnderlayMatLr>() != null)
        {
            //Debug.Log(GetComponent<Sensor>().GetType());

            persistenceInfo.persistenceSensFloorUnderlayMatLr._code = GetComponent<SensFloorUnderlayMatLr>()._code;
            persistenceInfo.persistenceSensFloorUnderlayMatLr._activationThreshold = GetComponent<SensFloorUnderlayMatLr>()._activationThreshold;
            persistenceInfo.persistenceSensFloorUnderlayMatLr._debug = GetComponent<SensFloorUnderlayMatLr>()._debug;
            persistenceInfo.persistenceSensFloorUnderlayMatLr._exportData = GetComponent<SensFloorUnderlayMatLr>()._exportData;
            persistenceInfo.persistenceSensFloorUnderlayMatLr._frecuency = GetComponent<SensFloorUnderlayMatLr>()._frecuency;

        }

        if (GetComponent<CapacitiveProximitySensor>() != null)
        {
            //Debug.Log(GetComponent<Sensor>().GetType());

            persistenceInfo.persistenceCapacitiveProximitySensor._code = GetComponent<CapacitiveProximitySensor>()._code;
            persistenceInfo.persistenceCapacitiveProximitySensor._activationThreshold = GetComponent<CapacitiveProximitySensor>()._activationThreshold;
            persistenceInfo.persistenceCapacitiveProximitySensor._debug = GetComponent<CapacitiveProximitySensor>()._debug;
            persistenceInfo.persistenceCapacitiveProximitySensor._exportData = GetComponent<CapacitiveProximitySensor>()._exportData;
            persistenceInfo.persistenceCapacitiveProximitySensor._frecuency = GetComponent<CapacitiveProximitySensor>()._frecuency;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (transform.parent != null)
        {
            var actualParent = transform.parent.GetComponent<PersistenceGameobject>();
            if (actualParent != null && transform != actualParent)
                persistenceInfo.persistenceTransform.parentId = transform.parent.GetComponent<PersistenceGameobject>().id;

            if (persistenceInfo.persistenceTransform.goPosition != transform.localPosition
                || persistenceInfo.persistenceTransform.goRotation != transform.localRotation
                || persistenceInfo.persistenceTransform.goScale != transform.localScale)
            {
                persistenceInfo.persistenceTransform.goPosition = transform.localPosition;
                persistenceInfo.persistenceTransform.goRotation = transform.localRotation;
                persistenceInfo.persistenceTransform.goScale = transform.localScale;
            }

        }
        else
        {
            if (persistenceInfo.persistenceTransform.goPosition != transform.localPosition
                || persistenceInfo.persistenceTransform.goRotation != transform.localRotation
                || persistenceInfo.persistenceTransform.goScale != transform.localScale)
            {
                persistenceInfo.persistenceTransform.parentId = -1;
                persistenceInfo.persistenceTransform.goPosition = transform.position;
                persistenceInfo.persistenceTransform.goRotation = transform.rotation;
                persistenceInfo.persistenceTransform.goScale = transform.localScale;
            }

        }
        */
        /*if(persistenceInfo.persistenceTransform.parentId != -1 && transform.parent == null)
        {
            List<PersistenceGameobject> lP = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();

            foreach (var p in lP)
            {
                if (p.id == persistenceInfo.persistenceTransform.parentId)
                    transform.parent = p.transform;
            }
        }*/

    }

    public void loadPersistenceTransform(PersistenceInfo pI)
    {
        persistenceInfo = pI;
        id = persistenceInfo.id;
        gameObject.name = persistenceInfo.name;
        gameObject.layer = persistenceInfo.persistenceTransform.layer;
        gameObject.tag = persistenceInfo.persistenceTransform.tag;

        if(persistenceInfo.persistenceTransform.parentId != -1)
        {
            List<PersistenceGameobject> lP = FindObjectOfType<PersistenceManager>().getPersistenceGameobjects();
            foreach (var p in lP)
            {
                if (p.id == persistenceInfo.persistenceTransform.parentId)
                {
                    //Debug.Log("[" + id + "] parent with id: " + persistenceInfo.persistenceTransform.parentId + " added!");
                    transform.parent = p.transform;
                }
                    
            }

             
            transform.localPosition = persistenceInfo.persistenceTransform.goPosition;
            transform.localRotation = persistenceInfo.persistenceTransform.goRotation;
            transform.localScale = persistenceInfo.persistenceTransform.goScale;
        }
        else
        {
            transform.position = persistenceInfo.persistenceTransform.goPosition;
            transform.rotation = persistenceInfo.persistenceTransform.goRotation;
            transform.localScale = persistenceInfo.persistenceTransform.goScale;
        }
    }

    
    public void loadPersistenceComponents()
    {

        if (persistenceInfo.persistenceMeshFilter.meshPath != "")
        {
            if(persistenceInfo.persistenceMeshFilter.meshPath == "Cube")
            {
                GameObject gm = GameObject.CreatePrimitive(PrimitiveType.Cube);
           
                gameObject.AddComponent<MeshFilter>().mesh = gm.GetComponent<MeshFilter>().sharedMesh;

                Destroy(gm);
            }
            else
            {
                gameObject.AddComponent<MeshFilter>().mesh = Resources.Load<Mesh>("Meshes/" + persistenceInfo.persistenceMeshFilter.meshPath);
                //gameObject.AddComponent<MeshFilter>().mesh = AssetDatabase.LoadAssetAtPath<Mesh>(persistenceInfo.persistenceMeshFilter.meshPath);
            }
            
        }

        if (persistenceInfo.persistenceMeshRenderer.mainMaterialPath != "")
        {
            gameObject.AddComponent<MeshRenderer>().material = Resources.Load<Material>("Material/" + persistenceInfo.persistenceMeshRenderer.mainMaterialPath);
            gameObject.GetComponent<MeshRenderer>().enabled = persistenceInfo.persistenceMeshRenderer.enabled;
        }


        if (persistenceInfo.persistenceCollider.typeCollider != "")
            if (persistenceInfo.persistenceCollider.typeCollider == "UnityEngine.MeshCollider")
            {
                gameObject.AddComponent<MeshCollider>().sharedMesh = Resources.Load<Mesh>("Meshes/" + persistenceInfo.persistenceCollider.meshPath);
                gameObject.GetComponent<MeshCollider>().enabled = persistenceInfo.persistenceCollider.enabled;
            }
            //gameObject.AddComponent<MeshCollider>().sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(persistenceInfo.persistenceCollider.meshPath);
            else
            if (persistenceInfo.persistenceCollider.typeCollider == "UnityEngine.BoxCollider")
                gameObject.AddComponent<BoxCollider>().enabled = persistenceInfo.persistenceCollider.enabled;

       if(persistenceInfo.persistenceSensFloorUnderlaySfLr._code != "")
        {
            gameObject.AddComponent<SensFloorUnderlaySfLr>();
            gameObject.GetComponent<SensFloorUnderlaySfLr>()._code = persistenceInfo.persistenceSensFloorUnderlaySfLr._code;
            gameObject.GetComponent<SensFloorUnderlaySfLr>()._debug = persistenceInfo.persistenceSensFloorUnderlaySfLr._debug;
            gameObject.GetComponent<SensFloorUnderlaySfLr>()._exportData = persistenceInfo.persistenceSensFloorUnderlaySfLr._exportData;
            gameObject.GetComponent<SensFloorUnderlaySfLr>()._exportDetailPosition = persistenceInfo.persistenceSensFloorUnderlaySfLr._exportDetailPosition;
            gameObject.GetComponent<SensFloorUnderlaySfLr>()._frecuency = persistenceInfo.persistenceSensFloorUnderlaySfLr._frecuency;
        }

        if (persistenceInfo.persistenceSensFloorUnderlayMatLr._code != "")
        {
            gameObject.AddComponent<SensFloorUnderlayMatLr>();
            gameObject.GetComponent<SensFloorUnderlayMatLr>()._code = persistenceInfo.persistenceSensFloorUnderlayMatLr._code;
            gameObject.GetComponent<SensFloorUnderlayMatLr>()._debug = persistenceInfo.persistenceSensFloorUnderlayMatLr._debug;
            gameObject.GetComponent<SensFloorUnderlayMatLr>()._exportData = persistenceInfo.persistenceSensFloorUnderlayMatLr._exportData;
            gameObject.GetComponent<SensFloorUnderlayMatLr>()._frecuency = persistenceInfo.persistenceSensFloorUnderlayMatLr._frecuency;
        }

        if (persistenceInfo.persistenceCapacitiveProximitySensor._code != "")
        {
            gameObject.AddComponent<CapacitiveProximitySensor>();
            gameObject.GetComponent<CapacitiveProximitySensor>()._code = persistenceInfo.persistenceCapacitiveProximitySensor._code;
            gameObject.GetComponent<CapacitiveProximitySensor>()._debug = persistenceInfo.persistenceCapacitiveProximitySensor._debug;
            gameObject.GetComponent<CapacitiveProximitySensor>()._exportData = persistenceInfo.persistenceCapacitiveProximitySensor._exportData;
            gameObject.GetComponent<CapacitiveProximitySensor>()._frecuency = persistenceInfo.persistenceCapacitiveProximitySensor._frecuency;
        }



    }


}
