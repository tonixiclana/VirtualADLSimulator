/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description  This script have functions to populate a grid
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class have functions to populate a grid
/// </summary>
[RequireComponent(typeof(GridLayoutGroup))]
[RequireComponent(typeof(ContentSizeFitter))]
[AddComponentMenu("ADLVirtualSimulator/Utility/PopulateGrid")]
[System.Serializable]
public class PopulateGrid : MonoBehaviour
{
    /// <summary>
    /// List of elements that have the grid
    /// </summary>
    [Tooltip("List of elements that have the grid")]
    public List<GameObject> elements = new List<GameObject>();

    void Start()
    {
    }

    void Update()
    {

    }

    /// <summary>
    /// add a gameobject in the grid
    /// </summary>
    /// <param name="gameObject"></param>
    public void addElement(GameObject gameObject)
    {
        elements.Add((GameObject)Instantiate(gameObject, transform));
    }

    /// <summary>
    /// Remove all elements of grid
    /// </summary>
    public void removeAllElements()
    {
        foreach(GameObject g in elements)
        {
            Destroy(g);
        }
    }
}
