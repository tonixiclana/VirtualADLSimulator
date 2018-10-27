using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FileBrowserItem : MonoBehaviour {

    public Image icon;
    public TextMeshProUGUI text;
    public string absolutePath;
    public bool isFolder;
    public FileBrowser fileBrowser;

    // Use this for initialization
    void Start () {
        if (isFolder)
        {
            GetComponent<Button>().onClick.AddListener(() => {
                fileBrowser.refreshGridContent(absolutePath);
            });
        }
        else
        {
            //Action to do when click a file
            /*GetComponent<Button>().onClick.AddListener(() => {
                fileBrowser.refreshGridContent(absolutePath);
            });*/
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void setOnClick(GameObject gm)
    {

    }



}
