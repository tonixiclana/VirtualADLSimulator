/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Falicities to select gameobject in real time with the cursor
 */

using cakeslice;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


/// <summary>
/// This class allow have a tracking of objects selected with the cursor
/// </summary>
[RequireComponent(typeof(Camera))]
[System.Serializable]
public class SelectGameobjectCursor : MonoBehaviour, IActuator
{

    /// <summary>
    /// The key that activate the action function in the gameobject
    /// </summary>
    [Tooltip("The key that activate the function in the gameobject")]
    public KeyCode triggerKey;

    /// <summary>
    /// Max distance allow to interact with the obj
    /// </summary>
    [Tooltip("Max distance allow to interact with the obj")]
    public float distMax;

    /// <summary>
    /// The gameobject which hit with the ray
    /// </summary>
    [Tooltip("The gameobject which hit with the ray")]
    public GameObject inCursorGameobject;

    /// <summary>
    /// Indicate if is allow the object selection
    /// </summary>
    [Tooltip("Indicate if is allow the object selection")]
    public bool allowSelect = true;

    /// <summary>
    /// Indicate if is allow the multiple selection
    /// </summary>
    [Tooltip("Indicate if is allow the multiple selection")]
    public bool multipleSelection;

    /// <summary>
    /// Indicate the layer that will be target
    /// </summary>
    [Tooltip("Indicate the layer that will be target")]
    public List<string> layers;

    /// <summary>
    /// the layers that apply in the filter
    /// </summary>
    [Tooltip("The layers that apply in the filter")]
    public List<string> blockLayers;

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
    /// Color in hover
    /// </summary>
    [Tooltip("The color of the highlghing in hover")]
    public int colorHover;

    /// <summary>
    /// Color in select
    /// </summary>
    [Tooltip("The color of the highlghing in select")]
    public int colorSelect;

    /// <summary>
    /// Cursor dot
    /// </summary>
    [Tooltip("The Canvas Transform of cursor dot")]
    public GameObject cursorDot;

    /// <summary>
    /// Color of cursor dot when hit with a object of 
    /// </summary>
    public Color cursorDotHoverColor;



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

    /// <summary>
    /// Store the original color of cursor dot
    /// </summary>
    private Color cursorDotOriginalColor;

    /// <summary>
    /// Indicate if blocked of layers is applied
    /// </summary>
    private bool isBlockedApplied = false;

    // Use this for initialization
    void Start () {

        blockLayers = new List<string>(layers);
        if(cursorDot != null && cursorDot.GetComponent<UnityEngine.UI.Image>() != null)
            cursorDotOriginalColor = cursorDot.GetComponent<UnityEngine.UI.Image>().color;
    }

    bool isTargetLayer(List<string> layers)
    {
        foreach (var l in layers)
            if (this.blockLayers.Contains(l))
                return true;

        return false;
    }


    void highlighingHover(bool isHit)
    {
        //Highlighing in hover
        if (!isHit)
        {
            //If previusly had a selectedGameobject hover with the target layer remove the highlighing
            if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>() != null && isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers)
                && !isBlockedApplied)
                if (!gameObjectsSelected.Contains(inCursorGameobject))
                {
                    //inCursorGameobject.GetComponent<Outline>().color = 0;
                    inCursorGameobject.GetComponent<Outline>().enabled = false ;
                }

            if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>() != null && isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers)
                && isBlockedApplied)
                if (!gameObjectsSelected.Contains(inCursorGameobject))
                {
                    inCursorGameobject.GetComponent<Outline>().color = inCursorGameobject.GetComponent<Outline>().previousColor;
                }



            if (cursorDot != null)
            {
                cursorDot.GetComponent<UnityEngine.UI.Image>().color = cursorDotOriginalColor;
            }

            //Put the selectedgameobject at null
            inCursorGameobject = null;
                                
        }
        else
        {
            //If the hit gameobject is diferent at the last selectedGameobject
            if (inCursorGameobject != hit.transform.gameObject)
            {
                //If previusly had a selectedGameobject hover with the target layer remove the highlighing
                if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>()
                    && isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers)
                     && !gameObjectsSelected.Contains(inCursorGameobject))
                {
                    if (inCursorGameobject.GetComponent<Outline>().color == inCursorGameobject.GetComponent<Outline>().previousColor)
                    {


                        inCursorGameobject.GetComponent<Outline>().enabled = false;
                    }
                    else
                        inCursorGameobject.GetComponent<Outline>().resetPreviousColor();

                    if (cursorDot != null)
                    {
                        cursorDot.GetComponent<UnityEngine.UI.Image>().color = cursorDotOriginalColor;
                    }

                }

                //set the actual selectedGameobject
                if (hit.transform.gameObject.GetComponent<Outline>() != null && isTargetLayer(hit.transform.gameObject.GetComponent<Outline>().layers))
                    inCursorGameobject = hit.transform.gameObject;
                else
                    inCursorGameobject = null;

                //If the layer is the target layer
                if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>() != null
                    && isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers)
                    && !gameObjectsSelected.Contains(inCursorGameobject))
                {

                    inCursorGameobject.GetComponent<Outline>().changeColor(colorHover);
                    inCursorGameobject.GetComponent<Outline>().enabled = true;
                    if (cursorDot != null)
                    {
                        cursorDotOriginalColor = cursorDot.GetComponent<UnityEngine.UI.Image>().color;
                        cursorDot.GetComponent<UnityEngine.UI.Image>().color = cursorDotHoverColor;
                    }

                }
            }
        }
    }

    void Update () {

        if(Cursor.lockState == CursorLockMode.Locked)
            ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        else
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        isHit = Physics.Raycast(ray, out hit, distMax);
        
        Debug.DrawRay(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f)), transform.forward * distMax, Color.green);

        ///if the timescale is 0 (pause the fixedupdate) dont work
        if (Time.timeScale != 0 )
        {
            if (!IsPointerOverUIObject())
                highlighingHover(isHit);
            else
                highlighingHover(false);


            if (isHit)
            {
                doAction();


                //Action when pulse 0 mouse button
                if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && allowSelect)
                {
                    //If the hit gameobject is diferent at the last selectedGameobject and is not null
                    if (inCursorGameobject != null && inCursorGameobject != hit.transform.gameObject)
                    {
                        //add the selectedGameobject at the list gameObjectsSelect
                        selectGameObject();
                    }
                    else
                    {
                        //If the click is over the same gameobject call selectGameObject to deselect
                        if (inCursorGameobject != null)
                        {
                            selectGameObject();
                            inCursorGameobject = null;
                        }
                    }
                }

            }
            else
                doAction();
            
        } 
    }

    /// <summary>
    /// Clear the list with the selected gameobjects, and put the original material
    /// </summary>
    public void clearSelection()
    {

        if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>() != null)
            if (inCursorGameobject.GetComponent<Outline>().previousColor == colorHover)
            {
                inCursorGameobject.GetComponent<Outline>().enabled = false;
                inCursorGameobject.GetComponent<Outline>().color = inCursorGameobject.GetComponent<Outline>().previousColor;
            }
                
            else
            {
                int c = inCursorGameobject.GetComponent<Outline>().color;
                inCursorGameobject.GetComponent<Outline>().color = inCursorGameobject.GetComponent<Outline>().previousColor;
                inCursorGameobject.GetComponent<Outline>().previousColor = c;
            }

        foreach (var gm in gameObjectsSelected)
            if (gm != null && gm.GetComponent<MeshRenderer>() != null)
                if (gm.GetComponent<Outline>().previousColor == colorHover)
                {
                    gm.GetComponent<Outline>().enabled = false;
                    gm.GetComponent<Outline>().color = gm.GetComponent<Outline>().previousColor;

                }
                else
                    gm.GetComponent<Outline>().color = gm.GetComponent<Outline>().previousColor;

        //Clear the map and the list
        gameObjectMaterialMap.Clear();
        gameObjectsSelected.Clear();
    }


    public void highlighingByTag(string tag, int color = 2)
    {
        foreach (var gm in GameObject.FindGameObjectsWithTag(tag))
            highlighingGameobject(gm, color, tag);
    }

    public void highlighingAndBlockByTag(string tag)
    {
        clearSelection();
       

        foreach (var gm in GameObject.FindGameObjectsWithTag(tag))
            highlighingGameobject(gm, 2, tag);

        blockLayer(tag);
    }

    public void highlighingAndBlockByTag(string tag, int color = 2)
    {
        clearSelection();
        
        foreach (var gm in GameObject.FindGameObjectsWithTag(tag))
            highlighingGameobject(gm, color, tag);

        blockLayer(tag);
    }

    public void clearAllHighlighing()
    {
        IEnumerable<string> diferentsLayers = layers.Except(blockLayers);
        
        if (diferentsLayers.Count() == 0)
            foreach(var i in blockLayers)
            {
                highlighingByTag(i);
            }
    }


    public void highlighingGameobject(GameObject gm, int color = 0, string layer = null)
    {

        if (gm.GetComponent<Outline>() == null)
        {
            gm.AddComponent<Outline>().color = color;
            if (layer != null)
                gm.GetComponent<Outline>().layers.Add(layer);
        }
        else
        {
            if (!gm.GetComponent<Outline>().enabled)
            {
                gm.GetComponent<Outline>().enabled = true;
                gm.GetComponent<Outline>().color = color;
            }
            else
            {
                gm.GetComponent<Outline>().enabled = false;
                gm.GetComponent<Outline>().color = colorHover;
                gm.GetComponent<Outline>().previousColor = colorHover;

                //gm.GetComponent<Outline>().color = gm.GetComponent<Outline>().previousColor;
            }

        }
    }

    public void blockByTag(string tag)
    {
        blockLayer(tag);
    }

    public void blockLayer(string layer)
    {
        //blockLayers.Clear();
       
        IEnumerable<string> diferentsLayers = layers.Except(blockLayers);

        if (diferentsLayers.Count() == 0)
            blockLayers.Clear();
     
        if (!blockLayers.Contains(layer))
        {
            blockLayers.Add(layer);
            isBlockedApplied = true;
        }
        else
        {
            blockLayers.Remove(layer);
            if(blockLayers.Count() == 0)
            {
                isBlockedApplied = false;

                foreach(var i in layers)
                    blockLayers.Add(i);
            }


        }
    }

    public void unlockAllLayers()
    {
        IEnumerable<string> diferentsLayers = layers.Except(blockLayers);

        clearSelection();
        if (diferentsLayers.Count() != 0)
        {
            for(int i = 0; i < blockLayers.Count(); i++)
                highlighingAndBlockByTag(blockLayers[i]);

            blockLayers.Clear();
            foreach (var i in layers)
                blockLayers.Add(i);
        }
    }


    public static void clearAllSelections()
    {
        foreach(var sGC in FindObjectsOfType<SelectGameobjectCursor>())
        {
            sGC.clearSelection();
        }
    }

    public void removeGameobjectOfList(GameObject gm)
    {
        if (isTargetLayer(gm.GetComponent<Outline>().layers))
        {
            //highLighingGameObjectMultiple(gm);
            if(gm.GetComponent<Outline>().color == gm.GetComponent<Outline>().previousColor)
                gm.GetComponent<Outline>().enabled = false;
            else
                gm.GetComponent<Outline>().color = gm.GetComponent<Outline>().previousColor;

            gameObjectsSelected.Remove(gm);
        }

    }

    /// <summary>
    /// Add or remove a selectedGameObject of the list of selected gameobjects
    /// </summary>
    public void selectGameObject()
    {
        //If is the target layer
        if (isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers))
        {
            //If the gameobjectsSelected not contain the gameobject
            if (!gameObjectsSelected.Contains(inCursorGameobject))
            {
                //If multiple selection is off, clear the actual selection
                if (!multipleSelection)
                    clearSelection();

                //Add gameobject at the list of gameobject
                gameObjectsSelected.Add(inCursorGameobject);

                //Highlighing the gameobject
                //highLighingGameObjectMultiple(selectedGameObject);
                int c = inCursorGameobject.GetComponent<Outline>().previousColor;
                inCursorGameobject.GetComponent<Outline>().previousColor = inCursorGameobject.GetComponent<Outline>().color;
                inCursorGameobject.GetComponent<Outline>().color = colorSelect;
                inCursorGameobject.GetComponent<Outline>().enabled = true;
            }
            else
            {
                //Quit the highlighing at gameobject and remove of the list
                //highLighingGameObjectMultiple(selectedGameObject);
                if (inCursorGameobject.GetComponent<Outline>().color == inCursorGameobject.GetComponent<Outline>().previousColor)
                    inCursorGameobject.GetComponent<Outline>().enabled = false;
                else
                {
                    int c = inCursorGameobject.GetComponent<Outline>().previousColor;
                    inCursorGameobject.GetComponent<Outline>().previousColor = inCursorGameobject.GetComponent<Outline>().color;
                    inCursorGameobject.GetComponent<Outline>().color = c;
                }


                gameObjectsSelected.Remove(inCursorGameobject);
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
        if(isTargetLayer(gm.GetComponent<Outline>().layers))
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

    public void doAction()
    {
        if (Input.GetKeyDown(triggerKey) && inCursorGameobject != null )
        {
            foreach (AudioActuator act in inCursorGameobject.GetComponents<AudioActuator>())
                inCursorGameobject.GetComponent<AudioActuator>().doAction();

            foreach (TransformActuator act in inCursorGameobject.GetComponents<TransformActuator>())
                inCursorGameobject.GetComponent<TransformActuator>().doAction();
        }

        //change the text of cursor dot

        if (inCursorGameobject != null && inCursorGameobject.GetComponent<Outline>() != null
                   && isTargetLayer(inCursorGameobject.GetComponent<Outline>().layers)
                   && !gameObjectsSelected.Contains(inCursorGameobject))
        {   
            if (cursorDot != null && cursorDot.GetComponentInChildren<TMPro.TextMeshProUGUI>() != null &&
                inCursorGameobject.GetComponent<Actuator>() != null)
                cursorDot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = inCursorGameobject.GetComponent<Actuator>().actionName;
        }
        else if (cursorDot != null && cursorDot.GetComponentInChildren<TMPro.TextMeshProUGUI>() != null)
            cursorDot.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "";



    }

}
