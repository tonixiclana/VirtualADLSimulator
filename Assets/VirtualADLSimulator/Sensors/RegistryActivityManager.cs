/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This script manage the sensors activities and send the sensors signal to a file
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
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
    /// The vector with the values of sensors listened
    /// </summary>
    [Tooltip("The vector with the values of sensors listened")]
    public List<float> _values;

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
            _startRecordButton.gameObject.SetActive(false);
            _stopRecordButton.gameObject.SetActive(true);

            //Create or append in a file add the header with the information of this activity
            _fileBrowser.editFileInActualPath(
                System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff")
                + "\t Name Of Activity: "
                + _nameOfActivityInput.text
                + "\nDescription Of Activity: \t"
                + _descriptionOfActivityInput.text
                + "\nActivity Start\n"
                , _fileNameInput.text

            );
        });

        //Add listener to stop button
        _stopRecordButton.onClick.AddListener(delegate
        {
            //Enter on initial state
            _exportData = false;
            _descriptionOfActivityInput.interactable = true;
            _nameOfActivityInput.interactable = true;
            _fileNameInput.interactable = true;
            _startRecordButton.gameObject.SetActive(true);
            _stopRecordButton.gameObject.SetActive(false);

            // add the footer with the information of this activity
            _fileBrowser.editFileInActualPath(
                 "\n" 
                + System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff")
                + "\t Name Of Activity: "
                + _nameOfActivityInput.text
                + "\nActivity ended"
                + "\n"
                , _fileNameInput.text
            );
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


    private void FixedUpdate()
    {
        
    }

    // Update is called once per frame
    void Update () {
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

            _fileBrowser.editFileInActualPath(System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff") + "\t" + eventNotification, _fileNameInput.text);

        }
    }

    public void validateInputs()
    {
        if(_nameOfActivityInput.text == "" || !_fileNameInput.text.Contains(".txt"))
        
            _startRecordButton.interactable = false;
        
        else
            _startRecordButton.interactable = true;
    }
}
