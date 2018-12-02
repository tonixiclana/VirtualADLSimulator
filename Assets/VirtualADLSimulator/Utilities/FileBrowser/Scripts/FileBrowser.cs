/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Provide the necesary logic for manage a filebrowser that allow read and write files
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Provide the necesary logic for manage a filebrowser that allow read and write files
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/Utility/FileBrowser")]
[System.Serializable]
public class FileBrowser : MonoBehaviour {

    /// <summary>
    /// Start path of file browsing
    /// </summary>
    [Tooltip(" Start path of file browsing")]
    public string startPath;

    /// <summary>
    /// Represent the directory icon to show in the screen
    /// </summary>
    [Tooltip("Represent the directory icon to show in the screen")]
    public FileBrowserItem dirItem;
    /// <summary>
    /// Represent the file icon to show in the screen
    /// </summary>
    [Tooltip("Represent the file icon to show in the screen")]
    public FileBrowserItem fileItem;

    /// <summary>
    /// Is the grid that contains the elements of file browser
    /// </summary>
    [Tooltip("Is the grid that contains the elements of file browser")]
    public GridLayoutGroup gridContent;

    /// <summary>
    /// Show folders
    /// </summary>
    [Tooltip("Show folder or not")]
    public bool showFolders;

    /// <summary>
    /// List of Files in browser
    /// </summary>
    private List<FileBrowserItem> listFile = new List<FileBrowserItem>();


    /// <summary>
    /// The actual Path
    /// </summary>
    [Tooltip("Is the actual path of filebrowser")]
    private string actualPath;

    private void Awake()
    {
        
        //Refresh the files list in a current directory
        if(listFile.Count == 0)
            refreshGridContent((startPath == "")?Application.persistentDataPath : Application.persistentDataPath + "/" + startPath);
    }

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public List<FileBrowserItem> getFileItems()
    {
        return listFile;
    }

    /// <summary>
    /// Refresh the gridContent of this obj with a files and directory that is in the path pass by argument
    /// </summary>
    /// <param name="path"></param>
    public void refreshGridContent(string path)
    {

        //remove the old items and take the files and directory
        gridContent.GetComponent<PopulateGrid>().removeAllElements();
        DirectoryInfo dir = new DirectoryInfo(path);
        actualPath = dir.FullName;
        DirectoryInfo[] listOfDir = dir.GetDirectories();
        FileInfo[] listOfFiles = dir.GetFiles("*.*");
        listFile.Clear();

        //Create a general fileBrowserItem
        FileBrowserItem item = dirItem;

        //add all of folders 
        if (showFolders)
        {
            //add the up directory folder in the begin of list
            
            item.GetComponent<FileBrowserItem>().text.text = "UP DIRECTORY";
            item.GetComponent<FileBrowserItem>().absolutePath = dir.Parent.FullName;
            item.GetComponent<FileBrowserItem>().isFolder = true;
            item.GetComponent<FileBrowserItem>().fileBrowser = this;
            //add at gridcontent
            gridContent.GetComponent<PopulateGrid>().addElement(Instantiate(item.gameObject, gridContent.transform));

            foreach (DirectoryInfo d in listOfDir)
            {
                item.GetComponent<FileBrowserItem>().text.text = d.Name;
                item.GetComponent<FileBrowserItem>().absolutePath = d.FullName;
                item.GetComponent<FileBrowserItem>().fileBrowser = this;
                //add at gridcontent
               
                gridContent.GetComponent<PopulateGrid>().addElement(Instantiate(item.gameObject, gridContent.transform));
            }
        }
           

        //add all of files
        item = fileItem;
        foreach (FileInfo f in listOfFiles)
        {
            item.GetComponent<FileBrowserItem>().text.text = f.Name;
            item.GetComponent<FileBrowserItem>().absolutePath = f.FullName;
            item.GetComponent<FileBrowserItem>().fileBrowser = this;
            //add at gridcontent and a list of files
            GameObject gm = Instantiate(item.gameObject, gridContent.transform);
            listFile.Add(gm.GetComponent<FileBrowserItem>());
            gridContent.GetComponent<PopulateGrid>().addElement(gm);
        }
    }


    /// <summary>
    /// Refresh the gridContent of this obj with a files and directory that actualPath
    /// </summary>
    public void refreshGridContentOfActualPath()
    {
        refreshGridContent(actualPath);
    }

    /// <summary>
    /// Allow create or add text in a file in a actual path
    /// </summary>
    /// <param name="content"></param>
    /// <param name="fileNameInput"></param>
    public void editFileInActualPath(string content, string fileNameInput)
    {
        if(fileNameInput != null && fileNameInput != "")
        {
            StreamWriter writer = new StreamWriter(actualPath + "/" + fileNameInput, true);
            writer.WriteLine(content);
            writer.Close();
        }
        
    }

    public string[] readFileInActualPath(string name)
    {
        return File.ReadAllLines(actualPath + "/" + name);
    }

    public void createFileInActualPath(string name, string text)
    {
        File.WriteAllText(actualPath + "/" + name, text);
    }

    public void deleteFileInActualPath(string name)
    {
        if (File.Exists(actualPath + "/" + name))
            File.Delete(actualPath + "/" + name);
    }

}
