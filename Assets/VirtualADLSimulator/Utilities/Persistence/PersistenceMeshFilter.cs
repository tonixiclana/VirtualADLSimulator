using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceMeshFilter : PersistenceComponent<PersistenceMeshFilter>, IPersistenceComponent<PersistenceMeshFilter>
{
    public string meshPath;

    public void addComponentInGameobject(GameObject gm)
    {
        if (meshPath == "Cube")
        {
            GameObject gmCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

            gm.AddComponent<MeshFilter>().mesh = gmCube.GetComponent<MeshFilter>().sharedMesh;

            MonoBehaviour.Destroy(gmCube); 
        }
        else
            gm.AddComponent<MeshFilter>().mesh = PersistenceGameobject.findObjectInResources<Mesh>(meshPath, "Meshes");
    }

    public PersistenceMeshFilter loadComponentInfo(GameObject gm)
    {
        meshPath = gm.GetComponent<MeshFilter>().sharedMesh.name;
        return this;        
    }
}

