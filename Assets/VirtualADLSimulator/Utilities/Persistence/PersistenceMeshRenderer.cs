using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceMeshRenderer : PersistenceComponent<PersistenceMeshRenderer>, IPersistenceComponent<PersistenceMeshRenderer>
{
    public bool enabled;
    public string mainMaterialPath;

    public void addComponentInGameobject(GameObject gm)
    {

       gm.AddComponent<MeshRenderer>().material = PersistenceGameobject.findObjectInResources<Material>(mainMaterialPath, "Material");
       
        gm.GetComponent<MeshRenderer>().enabled = enabled;

    }

    public PersistenceMeshRenderer loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<MeshRenderer>() != null)
        {
            enabled = gm.GetComponent<MeshRenderer>().enabled;

            if (gm.GetComponent<MeshRenderer>().sharedMaterial.name.Contains("(Instance)"))
                mainMaterialPath = gm.GetComponent<MeshRenderer>()
                    .sharedMaterial.name
                    .Substring(0,gm. GetComponent<MeshRenderer>().sharedMaterial.name.Length - 11);
            else
                mainMaterialPath = gm.GetComponent<MeshRenderer>().sharedMaterial.name;
        }

        return this;
    }
}

