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
using System;
using System.Diagnostics;
using System.Linq;

/// <summary>
/// Provide the necesary logic for manage a filebrowser that allow read and write files
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/Utility/FileBrowser")]
[System.Serializable]
public class FileBrowser : MonoBehaviour {

    public ConfirmMessage confirmMessage;

    /// <summary>
    /// Start path of file browsing
    /// </summary>
    [Tooltip(" Start path of file browsing")]
    public string startPath;

    /// <summary>
    /// Start path by default documents?
    /// </summary>
    [Tooltip("Start path by default documents?")]
    public bool startPathInDocuments = false;

    /// <summary>
    /// Represent the directory or file icon to show in the screen
    /// </summary>
    [Tooltip("Represent the directory or file icon to show in the screen")]
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
    public GridLayoutGroup contentBrowser;

    /// <summary>
    /// Is the text that contains the file content
    /// </summary>
    [Tooltip("Is the text that contains the file content")]
    public TextMeshProUGUI fileContent;

    /// <summary>
    /// Show folders
    /// </summary>
    [Tooltip("Show folder or not")]
    public bool showFolders;

    /// <summary>
    /// Extension filter, separate by commas
    /// </summary>
    [Tooltip("Extension filter")]
    public string[] extensionFilter;

    /// <summary>
    /// List of Files in browser
    /// </summary>
    private List<FileBrowserItem> listFile = new List<FileBrowserItem>();

    /// <summary>
    /// The boton to open a file in system brownser
    /// </summary>
    [Tooltip("The boton to open a file in system brownser")]
    public Button openInBrowserBtn;


    /// <summary>
    /// The actual Path
    /// </summary>
    [Tooltip("Is the actual path of filebrowser")]
    private string actualPath = "";

    /// <summary>
    /// The actual File Selected
    /// </summary>
    [Tooltip("Is the actual file selected")]
    public string fileSelected;


    void Awake()
    {

    }

    private void Start()
    {
        if(openInBrowserBtn != null)
            openInBrowserBtn.onClick.AddListener(delegate {
                openFileInBrowser(fileSelected);
            });
    }


    // Update is called once per frame
    private void Update () {

            


    }

    private void OnEnable()
    {
        if (startPathInDocuments)
            if (startPath == "")
                actualPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VirtualADLSimulator";
            else
            {
                actualPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VirtualADLSimulator";

                if(existInActualPath(startPath))
                    actualPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\VirtualADLSimulator\\" + startPath;
            }
        else
            if (startPath == "")
                actualPath = Application.persistentDataPath;
            else
                actualPath = Application.persistentDataPath + "\\" + startPath;


        //UnityEngine.Debug.Log("Actual Path " + actualPath);

        //Refresh the files list in a current directory
        /*if (listFile.Count == 0)*/
            refreshGridContentOfActualPath();

        if (fileSelected != "" && fileContent != null)
        {
            StopAllCoroutines();
            StartCoroutine(openTextFile(fileSelected));
        }
            
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
        contentBrowser.GetComponent<PopulateGrid>().removeAllElements();
        DirectoryInfo dir = new DirectoryInfo(path);
        actualPath = dir.FullName;
        DirectoryInfo[] listOfDir = dir.GetDirectories();

        //Create a general fileBrowserItem
        FileBrowserItem item = dirItem;

        //add all of folders 
        if (showFolders)
        {
            //add the up directory folder in the begin of list
            
            item.GetComponent<FileBrowserItem>().text.text = "UP DIRECTORY";
            item.GetComponent<FileBrowserItem>().absolutePath = (dir.Parent != null)? dir.Parent.FullName : dir.FullName;
            item.GetComponent<FileBrowserItem>().isFolder = true;
            item.GetComponent<FileBrowserItem>().fileBrowser = this;
            //add at gridcontent
            contentBrowser.GetComponent<PopulateGrid>().addElement(Instantiate(item.gameObject, contentBrowser.transform));

            foreach (DirectoryInfo d in listOfDir)
            {
                item.GetComponent<FileBrowserItem>().text.text = d.Name;
                item.GetComponent<FileBrowserItem>().absolutePath = d.FullName;
                item.GetComponent<FileBrowserItem>().fileBrowser = this;
                //add at gridcontent
               
                contentBrowser.GetComponent<PopulateGrid>().addElement(Instantiate(item.gameObject, contentBrowser.transform));
            }
        }

        FileInfo[] listOfFiles = new FileInfo[0];

        listFile.Clear();
        if (extensionFilter.Length == 0)
        {
            listOfFiles = dir.GetFiles("*.*");
            FileBrowserItem i = fileItem;
            foreach (FileInfo f in listOfFiles)
            {
                i.GetComponent<FileBrowserItem>().text.text = f.Name;
                i.GetComponent<FileBrowserItem>().absolutePath = f.FullName;
                i.GetComponent<FileBrowserItem>().fileBrowser = this;
                //add at gridcontent and a list of files
                GameObject gm = Instantiate(i.gameObject, contentBrowser.transform);
                listFile.Add(gm.GetComponent<FileBrowserItem>());
                contentBrowser.GetComponent<PopulateGrid>().addElement(gm);
            }
        }

        foreach (var e in extensionFilter)
        {
            listOfFiles = dir.GetFiles("*." + e);
            //add all of files
            FileBrowserItem i = fileItem;
            foreach (FileInfo f in listOfFiles)
            {
                i.GetComponent<FileBrowserItem>().text.text = f.Name;
                i.GetComponent<FileBrowserItem>().absolutePath = f.FullName;
                i.GetComponent<FileBrowserItem>().fileBrowser = this;
                //add at gridcontent and a list of files
                GameObject gm = Instantiate(i.gameObject, contentBrowser.transform);
                listFile.Add(gm.GetComponent<FileBrowserItem>());
                contentBrowser.GetComponent<PopulateGrid>().addElement(gm);
            }

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


    public IEnumerator openTextFile(string name)
    {
        /*
        if (!fileContent.transform.parent.transform.parent.gameObject.activeSelf)
        {*/
            fileSelected = name;
            fileContent.transform.parent.transform.parent.gameObject.SetActive(true);
            String s = "";

            StreamReader sr = new StreamReader(new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.ReadWrite | FileShare.Delete));
            fileContent.text = sr.ReadToEnd();
            while (fileContent.transform.parent.transform.parent.gameObject.activeSelf)
            {
                while ((s = sr.ReadLine()) != null)
                {
                    fileContent.text += s + "\n";// para que funcionen los saltos de linea 

                    yield return new WaitForSecondsRealtime(0.01f);
                }
                yield return new WaitForSecondsRealtime(0.1f);
            }

            /*if (!fileContent.transform.parent.transform.parent.gameObject.activeSelf)
                fileSelected = null;*/

            sr.Close();
        //}

    } 

    public string[] readFileInActualPath(string name)
    {
        return File.ReadAllLines(actualPath + "\\" + name);
    }

    public void createFileInActualPath(string name, string text)
    {
        File.WriteAllText(actualPath + "\\" + name, text);
    }

    public bool existInActualPath(string name, string path = "")
    {
        if (path == "")
            path = actualPath;
        if (File.Exists(path + "\\" + name) || Directory.Exists(path + "\\" + name))
            return true;
        else
            return false;

    }

    public void deleteFileInActualPath(string name)
    {
        if (File.Exists(actualPath + "\\" + name))
            File.Delete(actualPath + "\\" + name);
    }


    public string getActualPath()
    {
        return actualPath;
    }
    
    public void createFolder(string name, string path = "")
    {
        if (path == "")
            path = actualPath;

        if (!Directory.Exists(path + "\\" + name))
            Directory.CreateDirectory(path + "\\" + name);
    }

    public bool removeFolder(string name, string path = "", bool recursive = false)
    {
        if (path == "")
            path = actualPath;

        try
        {
            if (Directory.Exists(path + "\\" + name))
                Directory.Delete(path + "\\" + name, recursive);

            return true;
        }catch(Exception e)
        {
            return false;
        }

    }

    public void openFileInBrowser(string path = "")
    {
        string tmp;

        if (path == "")
            tmp = fileSelected;
        else
            tmp = path;

        if (fileSelected != null)
        {
            string cmd = "explorer.exe";
            string arg = "/select, " + tmp;
            Process.Start(cmd, arg);
        }
        else
        {
            string cmd = "explorer.exe";
            string arg = actualPath;
            Process.Start(cmd, arg);
        }
    }

    public bool hasContent(string path = "")
    {
        if (path == "")
            path = actualPath;
        try
        {
            //UnityEngine.Debug.Log(Directory.GetFiles(path).Length);
            if (Directory.GetFiles(path).Length > 0)
                return true;
            else
                return false;
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public void deselectFile()
    {
        fileSelected = "";
        
    }

    public string getPathOfFileInActualPath(string fileName)
    {
        foreach(string f in Directory.GetFiles(actualPath))
        {
            if (f.Split('\\').ToList().Last() == fileName)
                return f;
        }

        return "";
    }
}
