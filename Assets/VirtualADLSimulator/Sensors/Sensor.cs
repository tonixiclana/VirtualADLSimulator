/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description Implement of Interface ISensor, this class provide the essential attributes
 * and functions to controller a multipurpose sensor activity
 */

using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


/// <summary>
/// Implement of Interface ISensor, this class provide the essential attributes
/// and functions to controller a multipurpose sensor activity
/// </summary>
[System.Serializable]
public class Sensor : ISensor{

    /// <summary>
    /// The registry activity manager associated at this sensor, this object catch the notify 
    /// events of this gameobject and process the message
    /// </summary>
    [Tooltip("The registry activity manager associated at this sensor, this object catch the notify events of this gameobject and process the message")]
    public RegistryActivityManager registryActivityManager;

    /// <summary>
    /// The identification code for this sensor
    /// </summary>
    [Tooltip("The identification code for this sensor")]
    public string _code = "";

    /// <summary>
    /// The debug flag for this sensor
    /// </summary>
    [Tooltip("The debug flag for this sensor")]
    public bool _debug = true;

    /// <summary>
    /// The activation Threshold stablish the min value needed to artivate the sensor
    /// </summary>
    [Tooltip("The activation Threshold stablish the min value needed to artivate the sensor")]
    public float _activationThreshold = 0.1f;

    /// <summary>
    /// The frecuency velocity of read
    /// </summary>
    [Tooltip("The frecuency velocity of read")]
    public float _frecuency = 1f;

    /// <summary>
    /// Flag to control the export data to registryActivityManager
    /// </summary>
    [Tooltip("Flag to control the export data to registryActivityManager")]
    public bool _exportData = true;

    /// <summary>
    /// The state flag value of this sensor
    /// </summary>
    [Tooltip("the state flag value of this sensor")]
    private bool _state = false;

    /// <summary>
    /// The number of activation of this sensor
    /// </summary>
    [Tooltip("The number of activation of this sensor")]
    private int _numActivations = 0;

    /// <summary>
    /// The float value for this sensor
    /// </summary>
    [Tooltip("The float value for this sensor")]
    private float _value = 0;

    /// <summary>
    /// Previous value of sensor to determine if the value of sensor changed
    /// </summary>
    [Tooltip("Previous value of sensor to determine if the value of sensor changed")]
    private float _prevValue = 0f;

    /// <summary>
    /// Previous state of the sensor
    /// </summary>
    [Tooltip("Previous state of the sensor")]
    private bool _prevState = false;

    /// <summary>
    /// The time in seconds since the last read of sensor
    /// </summary>
    [Tooltip("The time in seconds since the last read of sensor")]
    private float _timeLastRead = 0f;


    public string Code
    {
        get
        {
            return this._code;
        }
        set
        {
            this._code = value;
        }
    }

    public bool Debug
    {
        get
        {
            return this._debug;
        }
        set
        {
            this._debug = value;
        }
    }

    public float ActivationThreshold
    {
        get
        {
            return this._activationThreshold;
        }
        set
        {
            this._activationThreshold = value;
        }
    }

    public float Frecuency
    {
        get
        {
            return this._frecuency;
        }
        set
        {
            this._frecuency = value;
        }
    }

    public bool ExportData
    {
        get
        {
            return this._exportData;
        }
        set
        {
            this._exportData = value;
        }
    }

    public float Value
    {
        get
        {
            return this._value;
        }
        set
        {
            this._value = value;
        }
    }

    public bool State
    {
        get
        {
            return this._state;
        }
        set
        {
            this._state = value;
        }
    }

    public int NumActivations
    {
        get
        {
            return this._numActivations;
        }
        set
        {
            this._numActivations = value;
        }
    }

    /// <summary>
    /// Implementation of notify event function, this function implement the way to send a signal 
    /// to RegistryActivityManager that catch the notifications
    /// </summary>
    /// <param name="eventNotification"></param>
    public void notifyEvent(string eventNotification)
    {
        //Only export if the flag is true and the this sensor have a registryActivityManager attached
        if(_exportData == true && registryActivityManager != null)
        {
            //If is active the export of data y this sensor has attached with a registry activity manager
            registryActivityManager.notifyEvent(_code + eventNotification);
            
        }
    }

    /// <summary>
    /// Change the sensor value, only when detect changes, the velocity of execution of 
    /// setSensorValue depending of frecuency of sensor
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Return true if the sensor value has been changed, false if not was changed</returns>
    public bool setSensorValue(float value)
    {
        //If pass enough time depend of the frecuency and the last time that this function worked then read the sensor
        if (Time.time - _timeLastRead > 1 / this._frecuency)
        {

            //If exist a change of value
            if (value != _prevValue)
            {
                //set the value
                _value = value;

                //notify the on, change or off event
                if (_value != 0f && !_prevState && value >= _activationThreshold)
                {
                    _state = true;
                    _numActivations++;
                    notifyEvent("\tON\t" + _value);
                }
                else
                if (_value != 0f && _prevState && value >= _activationThreshold)
                    notifyEvent("\tCH\t" + _value);
                else
                {
                    _state = false;
                    notifyEvent("\tOFF\t");
                }

                //Asign previous values
                _prevState = _state;
                _prevValue = _value;

                return true;
            }
            _timeLastRead = Time.time;
            return false;
        }else
            return false;
        
    }
}
