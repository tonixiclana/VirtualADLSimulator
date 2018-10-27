using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class FileBrowser : MonoBehaviour {

    public FileBrowserItem dirItem;
    public FileBrowserItem fileItem;
    public GridLayoutGroup gridContent;
    //public TMP_InputField fileContent;
    //public Button saveButton;

    public string actualPath;

	// Use this for initialization
	void Start () {
        refreshGridContent(".");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void refreshGridContent(string path)
    {
        gridContent.GetComponent<PopulateGrid>().removeAllElements();
        DirectoryInfo dir = new DirectoryInfo(path);
        actualPath = dir.FullName;
        DirectoryInfo[] listOfDir = dir.GetDirectories();
        FileInfo[] listOfFiles = dir.GetFiles("*.*"); 
        FileBrowserItem item = dirItem;

        item.GetComponent<FileBrowserItem>().text.text = "UP DIRECTORY";
        item.GetComponent<FileBrowserItem>().absolutePath = dir.Parent.FullName;
        item.GetComponent<FileBrowserItem>().isFolder = true;
        item.GetComponent<FileBrowserItem>().fileBrowser = this;
        gridContent.GetComponent<PopulateGrid>().addElement(item.gameObject);

        foreach (DirectoryInfo d in listOfDir)
        {
            item.GetComponent<FileBrowserItem>().text.text = d.Name;
            item.GetComponent<FileBrowserItem>().absolutePath = d.FullName;
            item.GetComponent<FileBrowserItem>().fileBrowser = this;
            gridContent.GetComponent<PopulateGrid>().addElement(item.gameObject);
        }

        item = fileItem;
        foreach (FileInfo f in listOfFiles)
        {
            item.GetComponent<FileBrowserItem>().text.text = f.Name;
            item.GetComponent<FileBrowserItem>().absolutePath = f.FullName;
            item.GetComponent<FileBrowserItem>().fileBrowser = this;
            gridContent.GetComponent<PopulateGrid>().addElement(item.gameObject);
        }
    }


    public void editFileInActualPath(string content, string fileNameInput)
    {
        if(fileNameInput != null && fileNameInput != "")
        {
            StreamWriter writer = new StreamWriter(actualPath + "/" + fileNameInput, true);
            writer.WriteLine(content);
            writer.Close();
        }
        
    }

}
