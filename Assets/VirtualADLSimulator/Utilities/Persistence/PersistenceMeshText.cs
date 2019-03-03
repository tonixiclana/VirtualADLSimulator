using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceTextMesh: PersistenceComponent<PersistenceTextMesh>, IPersistenceComponent<PersistenceTextMesh>
{
    public string text;
    public float offsetZ;
    public float characterSize;
    public float lineSpacing;
    public int anchor;
    public int alignment;
    public float tabSize;
    public int fontSize;
    

    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<TextMesh>().text = text;
        gm.GetComponent<TextMesh>().offsetZ = offsetZ;
        gm.GetComponent<TextMesh>().characterSize = characterSize;
        gm.GetComponent<TextMesh>().lineSpacing = lineSpacing;
        gm.GetComponent<TextMesh>().anchor = (TextAnchor)anchor;
        gm.GetComponent<TextMesh>().alignment = (TextAlignment)alignment;
        gm.GetComponent<TextMesh>().tabSize = tabSize;
        gm.GetComponent<TextMesh>().fontSize = fontSize;
    }

    public PersistenceTextMesh loadComponentInfo(GameObject gm)
    {

        text = gm.GetComponent<TextMesh>().text;
        offsetZ = gm.GetComponent<TextMesh>().offsetZ;
        characterSize = gm.GetComponent<TextMesh>().characterSize;
        lineSpacing = gm.GetComponent<TextMesh>().lineSpacing;
        anchor = (int)gm.GetComponent<TextMesh>().anchor;
        alignment = (int)gm.GetComponent<TextMesh>().alignment;
        tabSize = gm.GetComponent<TextMesh>().tabSize;
        fontSize = gm.GetComponent<TextMesh>().fontSize;

        return this;
    }
}

