/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This script manage the sensors activities and send the sensors signal to a file
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This script manage the sensors activities and send the sensors signal to a file
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/RegistryActivityManager")]
[System.Serializable]
public class RegistryActivityManager : MonoBehaviour {

    /// <summary>
    /// The filebrowser object relational with this registryactivitymanager
    /// </summary>
    [Tooltip("The filebrowser object relational with this registryactivitymanager")]
    public FileBrowser _fileBrowser;

    /// <summary>
    /// The list of listened sensors
    /// </summary>
    [Tooltip("The list of listened sensors")]
    public List<Sensor> _sensors = new List<Sensor>();


    /// <summary>
    /// The flag that indicate if export data or not
    /// </summary>
    [Tooltip("The flag that indicate if export data or not")]
    public bool _exportData = false;

    /// <summary>
    /// TMP_InputField to write the name of activity to record
    /// </summary>
    [Tooltip("TMP_InputField to write the name of activity to record")]
    public TMP_InputField _nameOfActivityInput;

    /// <summary>
    /// TMP_InputField to write the description of activity to record
    /// </summary>
    [Tooltip("TMP_InputField to write the description of activity to record")]
    public TMP_InputField _descriptionOfActivityInput;

    /// <summary>
    /// TMP_InputField to write the name of file to record
    /// </summary>
    [Tooltip("TMP_InputField to write the name of file to record")]
    public TMP_InputField _fileNameInput;

    /// <summary>
    /// Button to start the record
    /// </summary>
    [Tooltip("Button to start the record")]
    public Button _startRecordButton;

    /// <summary>
    /// Button to stop the record
    /// </summary>
    [Tooltip("Button to stop the record")]
    public Button _stopRecordButton;

    /// <summary>
    /// Button to create new experiment
    /// </summary>
    [Tooltip("Button to create new experiment")]
    public Button _newExperimentButton;

    private void Awake()
    {
        if(FindObjectOfType<SceneAndURLLoader>() != null)
            _fileBrowser.startPath = FindObjectOfType<SceneAndURLLoader>().loadedSavegame.Split('\\').ToList().Last().Replace(".txt", "");
        //select startpath depending of the savegame is saved
        //if (_fileBrowser.existInActualPath(FindObjectOfType<SceneAndURLLoader>().loadedSavegame.Split('\\').ToList().Last().Replace(".txt", "")))
 
    }

    

    private void OnEnable()
    {
        validateInputs();

    }


    // Use this for initialization
    void Start() {


        //Block the shortcut keys
        _nameOfActivityInput.GetComponent<TMP_InputField>().onSelect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = false;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = false;
            else
                FindObjectOfType<PlayerController>().enabled = false;
        });

        _nameOfActivityInput.GetComponent<TMP_InputField>().onDeselect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = true;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = true;
            else
                FindObjectOfType<PlayerController>().enabled = true;
        });

        _nameOfActivityInput.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate
        {
            validateInputs();
        });

        _descriptionOfActivityInput.GetComponent<TMP_InputField>().onSelect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = false;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = false;
            else
                FindObjectOfType<PlayerController>().enabled = false;
        });

        _descriptionOfActivityInput.GetComponent<TMP_InputField>().onDeselect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = true;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = true;
            else
                FindObjectOfType<PlayerController>().enabled = true;
        });

        _descriptionOfActivityInput.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate
        {
            validateInputs();
        });

        _fileNameInput.GetComponent<TMP_InputField>().onSelect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = false;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = false;
            else
                FindObjectOfType<PlayerController>().enabled = false;
        });

        _fileNameInput.GetComponent<TMP_InputField>().onDeselect.AddListener(delegate
        {
            foreach (var shortCut in FindObjectsOfType<ShortCutKeysEditMode>())
                shortCut.enabled = true;
            if (FindObjectOfType<FreeCamera>() != null)
                FindObjectOfType<FreeCamera>().enabled = true;
            else
                FindObjectOfType<PlayerController>().enabled = true;
        });

        _fileNameInput.GetComponent<TMP_InputField>().onValueChanged.AddListener(delegate
        {
            validateInputs();
        });

        //Initialize the values vector with the size of list of sensors
        //_values= new List<float>();

        //Attach this registry activity manager in all sensors slots not empty
        for (int i = 0; i < _sensors.Count; i++)
            if (_sensors[i] != null)
                _sensors[i].registryActivityManager = this;

        //Add listener to start button
        _startRecordButton.onClick.AddListener(delegate
        {
        //Enter on record state
        _exportData = true;
        _descriptionOfActivityInput.interactable = false;
        _fileNameInput.interactable = false;
        _nameOfActivityInput.interactable = false;
        _startRecordButton.gameObject.GetComponent<Button>().interactable = false;
        _stopRecordButton.gameObject.GetComponent<Button>().interactable = true;
        _newExperimentButton.interactable = false;
        _nameOfActivityInput.interactable = false;
        _descriptionOfActivityInput.interactable = false;
        //Create or append in a file add the header with the information of this activity
        _fileBrowser.editFileInActualPath(
           "Name Of Activity:\t"
            + _nameOfActivityInput.text
            + "\nDescription Of Activity:\t"
            + _descriptionOfActivityInput.text
            + "\n" + FindObjectOfType<DayNight>().currentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\tActivity Start\n"
            , _fileNameInput.text + ".hst"

        );

            _fileBrowser.fileContent.gameObject.SetActive(true) ;
            //StopAllCoroutines();
            //StartCoroutine(_fileBrowser.openTextFile(_fileBrowser.getActualPath() +  @"\\" + _fileNameInput.text + ".hst"));
        });

        //Add listener to stop button
        _stopRecordButton.onClick.AddListener(delegate
        {
            // add the footer with the information of this activity ended
            _fileBrowser.editFileInActualPath(
            "\n" + FindObjectOfType<DayNight>().currentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\tActivity ended\n"
            , _fileNameInput.text + ".hst"
            );

            //Enter on initial state
            _exportData = false;
            _descriptionOfActivityInput.interactable = true;
            _nameOfActivityInput.interactable = true;
            _fileNameInput.interactable = true;
            _startRecordButton.GetComponent<Button>().interactable = true;
            _stopRecordButton.GetComponent<Button>().interactable = false;
            _newExperimentButton.interactable = true;
            _nameOfActivityInput.interactable = true;
            _descriptionOfActivityInput.interactable = true;

            _fileBrowser.refreshGridContentOfActualPath();
            updateFileBrowserItemListeners();

        });

        _newExperimentButton.onClick.AddListener(delegate
        {

            if(_fileBrowser.fileSelected != "")
            {
                _fileBrowser.deselectFile();
                _newExperimentButton.GetComponentInChildren<TextMeshProUGUI>().text = "Save Experiment";
                _fileBrowser.fileContent.transform.parent.parent.gameObject.SetActive(false);
                _startRecordButton.interactable = false;
                _nameOfActivityInput.interactable = false;
                _descriptionOfActivityInput.interactable = false;


            }
            else
            {
                if(_fileBrowser.existInActualPath(_fileNameInput.text + ".hst"))
                {
                    _fileBrowser.confirmMessage.showMessage("The experiment: " + _fileNameInput.text + " already exists");
                }
                else
                {

                    _fileBrowser.createFileInActualPath(_fileNameInput.text + ".hst", "");
                    _newExperimentButton.GetComponentInChildren<TextMeshProUGUI>().text = "New Experiment";
                    _fileBrowser.fileSelected = _fileBrowser.getPathOfFileInActualPath(_fileNameInput.text + ".hst");
                    _fileBrowser.refreshGridContentOfActualPath();
                    updateFileBrowserItemListeners();
                    _fileNameInput.interactable = false;
                    _startRecordButton.interactable = true;

                    _nameOfActivityInput.interactable = true;
                    _descriptionOfActivityInput.interactable = true;
                }

            }

            

        });

        //StartCoroutine(addSensor());

    }


    IEnumerator addSensor()
    {
        while (true)
        {
            foreach (var sensor in FindObjectsOfType<Sensor>())
            {
                if (!_sensors.Contains(sensor))
                {
                    addSensor(sensor);
                }
            }

            yield return new WaitForSeconds(1);
        }

    }

    // Update is called once per frame
    void Update () {

        updateFileBrowserItemListeners();
        
        
        if (_fileBrowser.fileSelected == "")
        {
            //_newExperimentButton.interactable = false;
            _fileNameInput.interactable = true;
        }
        else
        {
            //_newExperimentButton.interactable = true;
            _fileNameInput.interactable = false;
        }
        


        //update the values vector with the values of sensors
        /* for (int i = 0; i < _sensors.Count; i++)
             if(_sensors[i] != null)
                 _values[i] = _sensors[i].Value;*/
    }

    /// <summary>
    /// Add sensor in the list of sensors listener
    /// </summary>
    /// <param name="sensor"></param>
    public void addSensor(Sensor sensor)
    {
        sensor.registryActivityManager = this;
        _sensors.Add(sensor);

    }

    /// <summary>
    /// Create a new line in a file with a time info and write the event notification string that contain the event information
    /// </summary>
    /// <param name="eventNotification"></param>
    public void notifyEvent(string eventNotification)
    {
        if (_fileBrowser != null && _exportData)
        {
            //Debug.Log("Hello: " + eventNotification + " : " + _fileNameInput.text );
            _fileBrowser.editFileInActualPath(FindObjectOfType<DayNight>().currentDateTime.ToString("yyyy-MM-dd HH:mm:ss:fffff") + "\t" + eventNotification, _fileNameInput.text + ".hst");

        }
    }

    public void validateInputs()
    {
        if(_fileBrowser.fileSelected != "")
        {
            _nameOfActivityInput.interactable = true;
            _descriptionOfActivityInput.interactable = true;

            if (_nameOfActivityInput.text == "" || _descriptionOfActivityInput.text == "" || _fileNameInput.text == "")
            {
                _startRecordButton.interactable = false;
            }
            else
                _startRecordButton.interactable = true;
        }
        else
        {
            _nameOfActivityInput.interactable = false;
            _descriptionOfActivityInput.interactable = false;
            _startRecordButton.interactable = false;
        }

        if (_fileNameInput.text == "")
        {
            _newExperimentButton.interactable = false;
        }
        else
        {
            if (_fileBrowser.fileSelected == "")
            {
                _newExperimentButton.interactable = true;
                _newExperimentButton.GetComponentInChildren<TextMeshProUGUI>().text = "Save Experiment";
            }
            else
            {
                _newExperimentButton.interactable = true;
                _newExperimentButton.GetComponentInChildren<TextMeshProUGUI>().text = "New Experiment";
            }

        }
    }

    //Refresh the listener of the files in filebrowser
    public void updateFileBrowserItemListeners()
    {
        /*if(actualPath != _fileBrowser.getActualPath())
        {
            actualPath = _fileBrowser.getActualPath();
            */
        foreach (FileBrowserItem item in _fileBrowser.getFileItems())
        {

            item.GetComponent<Button>().onClick.AddListener(delegate
            {

                StopAllCoroutines(); 
                StartCoroutine(_fileBrowser.openTextFile(item.absolutePath));
                    

                /*
                if (_fileBrowser.fileSelected.Split('\\').ToList().Last().Replace(".txt", "") != _fileNameInput.text)
                    _fileNameInput.text = _fileBrowser.fileSelected.Split('\\').ToList().Last().Replace(".hst", "");
                    */

                _fileNameInput.text = item.text.text.Replace(".hst", "");
            });
        }
        //}
        

    }
}
