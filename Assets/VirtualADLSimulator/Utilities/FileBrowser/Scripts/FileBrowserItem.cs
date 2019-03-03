/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This class Represent a one file browser item that can be file or directory
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class Represent a one file browser item that can be file or directory
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/Utility/FileBrowserItem")]
[RequireComponent(typeof(Button))]
[System.Serializable]
public class FileBrowserItem : MonoBehaviour {

    /// <summary>
    /// The icon of item
    /// </summary>
    [Tooltip("The icon of item")]
    public Image icon;




    /// <summary>
    /// The below text of the icon
    /// </summary>
    [Tooltip("The below text of the icon")]
    public TextMeshProUGUI text;

    /// <summary>
    /// The absolute path of this browser element
    /// </summary>
    [Tooltip("The absolute path of this browser element")]
    public string absolutePath;

    /// <summary>
    /// Indicate is this element is a folder
    /// </summary>
    [Tooltip("Indicate is this element is a folder")]
    public bool isFolder;

    /// <summary>
    /// The filebrowser that manage this filebrowseritem
    /// </summary>
    [Tooltip("The filebrowser that manage this filebrowseritem")]
    public FileBrowser fileBrowser;




    // Use this for initialization
    void Start () {
        
        if (isFolder)
        {

            //If crick in the folder refresh the grid content
            GetComponent<Button>().onClick.AddListener(() => {
                fileBrowser.refreshGridContent(absolutePath);
            });
        }
        else
        {
            //Action to do when click a file
            /*GetComponent<Button>().onClick.AddListener(() => {
                Process.Start(@absolutePath);
            });*/
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
