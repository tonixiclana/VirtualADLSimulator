using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceOutline : PersistenceComponent<PersistenceOutline>, IPersistenceComponent<PersistenceOutline>
{
    public bool enabled;
    public int color;
    public bool eraseRenderer;
    public List<string> layers;

    public void addComponentInGameobject(GameObject gm)
    {
        if (gm.GetComponent<Outline>() == null)
        {
            gm.AddComponent<Outline>().color = color;
            gm.GetComponent<Outline>().enabled = enabled;
            gm.GetComponent<Outline>().eraseRenderer = eraseRenderer;
            gm.GetComponent<Outline>().layers = layers;
        }

    }

    public PersistenceOutline loadComponentInfo(GameObject gm)
    {
        color = gm.GetComponent<Outline>().color;
        enabled = gm.GetComponent<Outline>().enabled;
        eraseRenderer = gm.GetComponent<Outline>().eraseRenderer;
        layers = gm.GetComponent<Outline>().layers;

        return this;
    }

}
