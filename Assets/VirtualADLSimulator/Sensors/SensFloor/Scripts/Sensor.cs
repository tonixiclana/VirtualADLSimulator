using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Sensor {

    public string _code = "";
    public bool _state = false;
    public bool _debug = true;
    public float _value = 0;
    public float _activationThreshold = 0.1f;
    public float _frecuency = 1f;
    public int _numActivations = 0;
    public bool _exportData = false;
    public string _outPutFilePath = "Assets/VirtualADLSimulator/Sensors/SensorOutPut.txt";

    public Sensor() {

    }


    public void notifyEvent(string eventNotification)
    {
        if(_outPutFilePath != null)
        {
            //Write some text to the test.txt file
            StreamWriter writer = new StreamWriter(_outPutFilePath, true);
            writer.WriteLine(System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff") + "\t" + _code + eventNotification);
            writer.Close();
        }
    }

}
