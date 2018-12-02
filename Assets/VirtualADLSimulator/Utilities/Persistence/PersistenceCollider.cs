using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceCollider : PersistenceComponent<PersistenceCollider>, IPersistenceComponent<PersistenceCollider>
{

    public bool enabled;
    public string typeCollider;
    public string meshPath;
    public bool convex;

    public void addComponentInGameobject(GameObject gm)
    {

            if (typeCollider == "UnityEngine.MeshCollider")
            {
                gm.AddComponent<MeshCollider>().sharedMesh = PersistenceGameobject.findObjectInResources<Mesh>(meshPath, "Meshes");
                gm.GetComponent<MeshCollider>().enabled = enabled;
                gm.GetComponent<MeshCollider>().convex = convex;
            }
            else
            if (typeCollider == "UnityEngine.BoxCollider")
                gm.AddComponent<BoxCollider>().enabled = enabled;

            

    }

    public PersistenceCollider loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<Collider>() != null)
        {
            
            enabled = gm.GetComponent<Collider>().enabled;
            typeCollider = gm.GetComponent<Collider>().GetType().ToString();
            if (typeCollider == typeof(MeshCollider).ToString())
            {
                meshPath = gm.GetComponent<MeshCollider>().sharedMesh.name;
                convex = gm.GetComponent<MeshCollider>().convex;
            }
        }

        return this;
    }
}
