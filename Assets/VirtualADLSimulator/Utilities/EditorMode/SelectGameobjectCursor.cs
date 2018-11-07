/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

[System.Serializable]
public class SelectGameobjectCursor : MonoBehaviour {

    public bool multipleSelection;
    public int layer;

    public GameObject selectedGameObject;
    private Material originalMaterialSelectedGameObject;
    public Material highLighingMaterial;

    private Dictionary<GameObject, Material> gameObjectMaterialMap = new Dictionary<GameObject, Material>();

    public List<GameObject> gameObjectsSelected = new List<GameObject>();

    private RaycastHit hit;

    private Ray ray;

    private bool isHit;

	// Use this for initialization
	void Start () {

	}
	
	void Update () {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isHit = Physics.Raycast(ray, out hit, 100f);


        if (!isHit)
        {
            if(selectedGameObject != null && selectedGameObject.layer == layer)
                selectedGameObject.GetComponent<MeshRenderer>().material = originalMaterialSelectedGameObject;
            selectedGameObject = null;
        }
        else
        {
            if(selectedGameObject != hit.transform.gameObject)
            {
                if (selectedGameObject != null && selectedGameObject.layer == layer)
                    selectedGameObject.GetComponent<MeshRenderer>().material = originalMaterialSelectedGameObject;
                
                selectedGameObject = hit.transform.gameObject;

                if(selectedGameObject.layer == layer)
                {
                    Material material = new Material(highLighingMaterial);
                    material.mainTexture = selectedGameObject.GetComponent<MeshRenderer>().material.mainTexture;
                    originalMaterialSelectedGameObject = selectedGameObject.GetComponent<MeshRenderer>().material;
                    selectedGameObject.GetComponent<MeshRenderer>().material = material;
                }

            }

        }

        if (Input.GetMouseButtonDown(0))
            if (selectedGameObject != GetComponent<SelectGameobjectCursor>().selectedGameObject && selectedGameObject != null)
            {
                    selectGameObject();
            }
            else
            {
                if(selectedGameObject != null)
                {
                    selectGameObject();
                    selectedGameObject = null;
                }
            }
    }

    public void clearSelection()
    {
        foreach(var pair in gameObjectMaterialMap)
        {
                pair.Key.GetComponent<MeshRenderer>().material = pair.Value;
        }
        gameObjectMaterialMap.Clear();
        gameObjectsSelected.Clear();
    }

    public void selectGameObject()
    {
        if (selectedGameObject.layer == layer)
        {
            if (!gameObjectsSelected.Contains(selectedGameObject))
            {
                if (!multipleSelection)
                    clearSelection();

                gameObjectsSelected.Add(selectedGameObject);
                highLighingGameObjectMultiple(selectedGameObject);
            }
            else
            {
                highLighingGameObjectMultiple(selectedGameObject);
                gameObjectsSelected.Remove(selectedGameObject);
            }
        }
    }

    public void highLighingGameObjectMultiple(GameObject gm)
    {
        if(gm.layer == layer)
        {
            Material material;
            if (!gameObjectMaterialMap.ContainsKey(gm))
            {
                gameObjectMaterialMap.Add(gm, originalMaterialSelectedGameObject);
                material = new Material(highLighingMaterial);
                material.mainTexture = originalMaterialSelectedGameObject.mainTexture;
                gm.GetComponent<MeshRenderer>().material = material;
            }
            else
            {
                if (gameObjectMaterialMap.TryGetValue(gm, out material))
                {
                    gm.GetComponent<MeshRenderer>().material = material;
                    gameObjectMaterialMap.Remove(gm);
                }

            }
        }
        
    }



    /*
    public void highLighingGameObject(GameObject gm, bool state)
    {
        if (gm.layer == 10)
        {
            Material material;
            if (!gameObjectMaterialMap.ContainsKey(gm) && state)
            {
                gameObjectMaterialMap.Add(gm, gm.GetComponent<MeshRenderer>().material);
                material = new Material(highLighingMaterial);
                material.mainTexture = gm.GetComponent<MeshRenderer>().material.mainTexture;
                gm.GetComponent<MeshRenderer>().material = material;
            }
            else
            if (gameObjectMaterialMap.ContainsKey(gm) && !state)
            {
                if (gameObjectMaterialMap.TryGetValue(gm, out material))
                {
                    gm.GetComponent<MeshRenderer>().material = material;
                    gameObjectMaterialMap.Remove(gm);
                }

            }
        }
    }*/

}
