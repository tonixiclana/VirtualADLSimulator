using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Sensor : MonoBehaviour, ISensor{

    private RegistryActivityManager registryActivityManager;
    public string _code = "";
    public bool _state = false;
    public bool _debug = true;
    public float _value = 0;
    public float _activationThreshold = 0.1f;
    public float _frecuency = 1f;
    public int _numActivations = 0;
    public bool _exportData = true;


    public RegistryActivityManager RegistryActivityManager
    {
        get
        {
            return registryActivityManager;
        }

        set
        {
            registryActivityManager = value;
        }
    }

    //public string _outPutFilePath = "Assets/VirtualADLSimulator/Sensors/SensorOutPut.txt";

    private void Start()
    {
    }

    public void notifyEvent(string eventNotification)
    {
        if(_exportData == true && registryActivityManager != null)
        {
            registryActivityManager.notifyEvent(_code + eventNotification);
            
        }
    }
}
