using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class PersistenceLight : PersistenceComponent<PersistenceLight>, IPersistenceComponent<PersistenceLight>
{
    public bool enabled;
    public int type;
    public float range;
    public float spotAngle;
    public float intensity;
    public int shadowType;
    public int shadowResolution;

    public void addComponentInGameobject(GameObject gm)
    {

        gm.AddComponent<Light>().enabled = enabled;
        gm.GetComponent<Light>().type = (LightType)type;
        gm.GetComponent<Light>().range = range;
        gm.GetComponent<Light>().spotAngle = spotAngle;
        gm.GetComponent<Light>().intensity = intensity;
        gm.GetComponent<Light>().shadows = (LightShadows)shadowType;
        gm.GetComponent<Light>().shadowResolution = (LightShadowResolution)shadowResolution;



    }

    public PersistenceLight loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<Light>() != null)
        {
            enabled = gm.GetComponent<Light>().enabled;
            type = (int)gm.GetComponent<Light>().type;
            range = gm.GetComponent<Light>().range;
            spotAngle = gm.GetComponent<Light>().spotAngle;
            intensity = gm.GetComponent<Light>().intensity;
            shadowType = (int)gm.GetComponent<Light>().shadows;
            shadowResolution = (int)gm.GetComponent<Light>().shadowResolution;
        }

        return this;
    }
}

