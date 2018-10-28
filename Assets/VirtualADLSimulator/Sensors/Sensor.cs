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
public class Sensor : MonoBehaviour, ISensor{

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
    /// The state flag value of this sensor
    /// </summary>
    [Tooltip("the state flag value of this sensor")]
    public bool _state = false;

    /// <summary>
    /// The debug flag for this sensor
    /// </summary>
    [Tooltip("The debug flag for this sensor")]
    public bool _debug = true;

    /// <summary>
    /// The float value for this sensor
    /// </summary>
    [Tooltip("The float value for this sensor")]
    public float _value = 0;

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
    /// The number of activation of this sensor
    /// </summary>
    [Tooltip("The number of activation of this sensor")]
    public int _numActivations = 0;

    /// <summary>
    /// Flag to control the export data to registryActivityManager
    /// </summary>
    [Tooltip("Flag to control the export data to registryActivityManager")]
    public bool _exportData = true;

    /// <summary>
    /// Previous value of sensor to determine if the value of sensor changed
    /// </summary>
    [Tooltip("Previous value of sensor to determine if the value of sensor changed")]
    public float _prevValue = 0f;

    /// <summary>
    /// Previous state of the sensor
    /// </summary>
    [Tooltip("Previous state of the sensor")]
    public bool _prevState = false;

    /// <summary>
    /// The time in seconds since the last read of sensor
    /// </summary>
    [Tooltip("The time in seconds since the last read of sensor")]
    public float _timeLastRead = 0f;

    private void Start()
    {

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
