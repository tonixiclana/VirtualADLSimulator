using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class RegistryActivityManager : MonoBehaviour {

    public FileBrowser _fileBrowser;
    //public string _outPutFilePath = "Assets/VirtualADLSimulator/Sensors/SensorOutPut.txt";
    public Sensor[] _sensors = new Sensor[32];
    public float[] _values;
    public bool _exportData = false;
    public TMP_InputField _nameOfActivityInput;
    public TMP_InputField _descriptionOfActivityInput;
    public TMP_InputField _fileNameInput;
    public Button _startRecordButton;
    public Button _stopRecordButton;

    // Use this for initialization
    void Start () {
        _values= new float[_sensors.Length];
        for (int i = 0; i < _sensors.Length; i++)
            if (_sensors[i] != null)
                _sensors[i].RegistryActivityManager = this;

        _startRecordButton.onClick.AddListener(delegate
        {
            _exportData = true;
            _descriptionOfActivityInput.interactable = false;
            _fileNameInput.interactable = false;
            _nameOfActivityInput.interactable = false;
            _startRecordButton.gameObject.SetActive(false);
            _stopRecordButton.gameObject.SetActive(true);



        //_outPutFilePath = _fileBrowser.actualPath + "\\" + _fileBrowser.fileNameInput.text;
        _fileBrowser.editFileInActualPath(
            System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff")
            + "\t Name Of Activity: "
            + _nameOfActivityInput.text
            + "\nDescription Of Activity: \t"
            + _descriptionOfActivityInput.text
            + "Activity Start\n"
            , _fileNameInput.text

            );
        });

        _stopRecordButton.onClick.AddListener(delegate
        {
            _exportData = false;
            _descriptionOfActivityInput.interactable = true;
            _nameOfActivityInput.interactable = true;
            _fileNameInput.interactable = true;
            _startRecordButton.gameObject.SetActive(true);
            _stopRecordButton.gameObject.SetActive(false);

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

    }
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < _sensors.Length; i++)
            if(_sensors[i] != null)
            _values[i] = _sensors[i]._value;


	}

    public void notifyEvent(string eventNotification)
    {
        if (_fileBrowser != null && _exportData)
        {

            _fileBrowser.editFileInActualPath(System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff") + "\t" + eventNotification, _fileNameInput.text);
            /*
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(_outPutFilePath, true);
            writer.WriteLine();
            writer.Close();*/
        }
    }
}
