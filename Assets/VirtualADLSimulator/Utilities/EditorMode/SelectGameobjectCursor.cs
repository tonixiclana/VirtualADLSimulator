/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time with the cursor
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// This class allow have a tracking of objects selected with the cursor
/// </summary>
[RequireComponent(typeof(Camera))]
[System.Serializable]
public class SelectGameobjectCursor : MonoBehaviour {

    /// <summary>
    /// Indicate if is allow the multiple selection
    /// </summary>
    [Tooltip("Indicate if is allow the multiple selection")]
    public bool multipleSelection;

    /// <summary>
    /// Indicate the layer that will be target
    /// </summary>
    [Tooltip("Indicate the layer that will be target")]
    public int layer;

    /// <summary>
    /// The material that contains the highLighing, that allow change the main texture
    /// </summary>
    [Tooltip("The material that contains the highLighing, that allow change the main texture")]
    public Material highLighingMaterial;

    /// <summary>
    /// The list with the gameobjects selected with click over the gameobject
    /// </summary>
    [Tooltip("The list with the gameobjects selected with click over the gameobject")]
    public List<GameObject> gameObjectsSelected = new List<GameObject>();

    /// <summary>
    /// The gameobject which hit with the ray
    /// </summary>
    private GameObject selectedGameObject;


    /// <summary>
    /// The original material of selected gameobject, needed to remove the highlighing
    /// </summary>
    private Material originalMaterialSelectedGameObject;

    /// <summary>
    /// A Dictionary that associate gameobjects and the original material, used to multiple selection
    /// </summary>
    private Dictionary<GameObject, Material> gameObjectMaterialMap = new Dictionary<GameObject, Material>();

    /// <summary>
    /// Contain the hit information
    /// </summary>
    private RaycastHit hit;

    /// <summary>
    /// The ray used to select the gameobject that it aim the cursor
    /// </summary>
    private Ray ray;

    /// <summary>
    /// Bool that indicate if the ray hit with another gameobject
    /// </summary>
    private bool isHit;

	// Use this for initialization
	void Start () {

	}

    void Update () {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isHit = Physics.Raycast(ray, out hit, 100f);

        ///if the timescale is 0 (pause the fixedupdate) dont work
        if(Time.timeScale != 0)
        {
            //Highlighing in hover
            if (!isHit)
            {
                //If previusly had a selectedGameobject hover with the target layer remove the highlighing
                if (selectedGameObject != null && selectedGameObject.layer == layer)
                    selectedGameObject.GetComponent<MeshRenderer>().material = originalMaterialSelectedGameObject;

                //Put the selectedgameobject at null
                selectedGameObject = null;
            }
            else
            {
                //If the hit gameobject is diferent at the last selectedGameobject
                if (selectedGameObject != hit.transform.gameObject)
                {
                    //If previusly had a selectedGameobject hover with the target layer remove the highlighing
                    if (selectedGameObject != null && selectedGameObject.layer == layer)
                        selectedGameObject.GetComponent<MeshRenderer>().material = originalMaterialSelectedGameObject;

                    //set the actual selectedGameobject
                    selectedGameObject = hit.transform.gameObject;

                    //If the layer is the target layer
                    if (selectedGameObject.layer == layer)
                    {
                        //Create a material with the highlighing
                        Material material = new Material(highLighingMaterial);
                        //Set the main texture with the selected gameobject main texture
                        material.mainTexture = selectedGameObject.GetComponent<MeshRenderer>().material.mainTexture;
                        //Store the original material
                        originalMaterialSelectedGameObject = selectedGameObject.GetComponent<MeshRenderer>().material;
                        //Set the highlighing material
                        selectedGameObject.GetComponent<MeshRenderer>().material = material;
                    }
                }
            }

            //Action when pulse 0 mouse button
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
                //If the hit gameobject is diferent at the last selectedGameobject and is not null
                if (selectedGameObject != null && selectedGameObject != hit.transform.gameObject)
                {
                    //add the selectedGameobject at the list gameObjectsSelect
                    selectGameObject();
                }
                else
                {
                    //If the click is over the same gameobject call selectGameObject to deselect
                    if (selectedGameObject != null)
                    {
                        selectGameObject();
                        selectedGameObject = null;
                    }
                }
        } 
    }

    /// <summary>
    /// Clear the list with the selected gameobjects, and put the original material
    /// </summary>
    public void clearSelection()
    {
        //Each pair of map put the original material
        foreach(var pair in gameObjectMaterialMap)
                pair.Key.GetComponent<MeshRenderer>().material = pair.Value;

        //Clear the map and the list
        gameObjectMaterialMap.Clear();
        gameObjectsSelected.Clear();
    }

    /// <summary>
    /// Add or remove a selectedGameObject of the list of selected gameobjects
    /// </summary>
    public void selectGameObject()
    {
        //If is the target layer
        if (selectedGameObject.layer == layer)
        {
            //If the gameobjectsSelected not contain the gameobject
            if (!gameObjectsSelected.Contains(selectedGameObject))
            {
                //If multiple selection is off, clear the actual selection
                if (!multipleSelection)
                    clearSelection();

                //Add gameobject at the list of gameobject
                gameObjectsSelected.Add(selectedGameObject);
                //Highlighing the gameobject
                highLighingGameObjectMultiple(selectedGameObject);
            }
            else
            {
                //Quit the highlighing at gameobject and remove of the list
                highLighingGameObjectMultiple(selectedGameObject);
                gameObjectsSelected.Remove(selectedGameObject);
            }
        }
    }

    /// <summary>
    /// Use the map of object - originalmaterial to manage multiples highlighing of gameobjects , based 
    /// in it material of MeshRender. If the gameobject is included in the map, is removed and put the original material
    /// </summary>
    /// <param name="gm"></param>
    public void highLighingGameObjectMultiple(GameObject gm)
    {
        //Is the target layer?
        if(gm.layer == layer)
        {
            Material material;
            //If the gm is in map
            if (!gameObjectMaterialMap.ContainsKey(gm))
            {
                //Add the original material and gameobject in a map
                gameObjectMaterialMap.Add(gm, originalMaterialSelectedGameObject);

                //Create and put the highlighing material at gm
                material = new Material(highLighingMaterial);
                material.mainTexture = originalMaterialSelectedGameObject.mainTexture;
                gm.GetComponent<MeshRenderer>().material = material;
            }
            else
            {
                //If the gm is in map, put the original material and remove of list
                if (gameObjectMaterialMap.TryGetValue(gm, out material))
                {
                    gm.GetComponent<MeshRenderer>().material = material;
                    gameObjectMaterialMap.Remove(gm);
                }
            }
        }
    }

    /// <summary>
    /// Detect when the pointer is over UI Object
    /// </summary>
    /// <returns></returns>
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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
