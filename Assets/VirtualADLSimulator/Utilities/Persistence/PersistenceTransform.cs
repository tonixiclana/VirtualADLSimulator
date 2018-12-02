using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceTransform : PersistenceComponent<PersistenceTransform>, IPersistenceComponent<PersistenceTransform>  {

    public int parentId = -1;
    public int layer;
    public string tag;
    public Vector3 goPosition;
    public Vector4 goRotation;
    public Vector3 goScale;

    public void addComponentInGameobject(GameObject gm)
    {
        gm.layer = layer;
        gm.tag = tag;


        if (parentId != -1)
        {
            List<PersistenceGameobject> lP = GameObject.FindObjectOfType<PersistenceManager>().getPersistenceGameobjects();

            foreach (var p in lP)
            {
                if (p.id == parentId)
                {
                    gm.transform.parent = p.transform;
                }

            }


            gm.transform.localPosition = goPosition;
            Quaternion quaternion = new Quaternion(goRotation.x,
                goRotation.y,
                goRotation.z,
                goRotation.w);

            gm.transform.localRotation = quaternion;

            gm.transform.localScale = goScale;
        }
        else
        {
            gm.transform.position = goPosition;
            Quaternion quaternion = new Quaternion(goRotation.x,
               goRotation.y,
               goRotation.z,
               goRotation.w);

            gm.transform.rotation = quaternion;
            gm.transform.localScale = goScale;
        }
    }

    public PersistenceTransform loadComponentInfo(GameObject gm)
    {
        layer = gm.layer;
        tag = gm.tag;

        if (gm.transform.parent == null || gm.transform.parent.GetComponent<PersistenceGameobject>() == null)
            parentId = -1;
        else
            parentId = gm.transform.parent.GetComponent<PersistenceGameobject>().id;

        if (gm.transform.parent != null)
        {
            goPosition = gm.transform.localPosition;
            goRotation.w = gm.transform.localRotation.w;
            goRotation.x = gm.transform.localRotation.x;
            goRotation.y = gm.transform.localRotation.y;
            goRotation.z = gm.transform.localRotation.z;
            goScale = gm.transform.localScale;
        }
        else
        {
            goPosition = gm.transform.position;
            goRotation.w = gm.transform.rotation.w;
            goRotation.x = gm.transform.rotation.x;
            goRotation.y = gm.transform.rotation.y;
            goRotation.z = gm.transform.rotation.z;
            goScale = gm.transform.localScale;
        }

        return this;
    }
}
