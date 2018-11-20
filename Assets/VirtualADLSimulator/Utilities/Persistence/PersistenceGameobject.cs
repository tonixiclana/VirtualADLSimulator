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

        if (!loaded)
        {

            persistenceInfo.id = id;
            persistenceInfo.name = gameObject.name;
            persistenceInfo.persistenceTransform.layer = gameObject.layer;
            persistenceInfo.persistenceTransform.tag = gameObject.tag;

            if(transform.parent == null || transform.parent.GetComponent<PersistenceGameobject>() == null)
                persistenceInfo.persistenceTransform.parentId = -1;
            else
                persistenceInfo.persistenceTransform.parentId = transform.parent.GetComponent<PersistenceGameobject>().id;

            if(transform.parent != null)
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
                persistenceInfo.persistenceMeshRenderer.mainMaterialPath = GetComponent<MeshRenderer>().sharedMaterial.name;
            }


            if (GetComponent<Collider>() != null)
            {
                persistenceInfo.persistenceCollider.enabled = GetComponent<Collider>().enabled;
                persistenceInfo.persistenceCollider.typeCollider = GetComponent<Collider>().GetType().ToString();
                if (persistenceInfo.persistenceCollider.typeCollider == "UnityEngine.MeshCollider")
                    persistenceInfo.persistenceCollider.meshPath = GetComponent<MeshCollider>().sharedMesh.name;

            }

            persistenceInfo.persistenceScripts = gameObject.GetComponents<MonoBehaviour>().Cast<MonoBehaviour>().ToList();

        }


       
    }




    // Update is called once per frame
    void FixedUpdate()
    {

        /*if(persistenceInfo.persistenceTransform.parentId != -1 && transform.parent == null)
        {
            List<PersistenceGameobject> lP = GameObject.FindObjectsOfType<PersistenceGameobject>().Cast<PersistenceGameobject>().ToList();

            foreach (var p in lP)
            {
                if (p.id == persistenceInfo.persistenceTransform.parentId)
                    transform.parent = p.transform;
            }
        }*/




        if (transform.parent != null)
        {
            if(transform.parent.GetComponent<PersistenceGameobject>() != null)
                persistenceInfo.persistenceTransform.parentId = transform.parent.GetComponent<PersistenceGameobject>().id;

            persistenceInfo.persistenceTransform.parentId = transform.parent.GetComponent<PersistenceGameobject>().id;
            persistenceInfo.persistenceTransform.goPosition = transform.localPosition;
            persistenceInfo.persistenceTransform.goRotation = transform.localRotation;
            persistenceInfo.persistenceTransform.goScale = transform.localScale;
        }
        else
        {
            persistenceInfo.persistenceTransform.parentId = -1;
            persistenceInfo.persistenceTransform.goPosition = transform.position;
            persistenceInfo.persistenceTransform.goRotation = transform.rotation;
            persistenceInfo.persistenceTransform.goScale = transform.localScale;
        }
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

       

        

    }


}
