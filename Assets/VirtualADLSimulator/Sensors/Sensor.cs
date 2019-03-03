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
using System.ComponentModel;
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
    /// events of this gameobject and process the messages
    /// </summary>
    [Tooltip("The registry activity manager associated at this sensor, this object catch the notify events of this gameobject and process the message")]
    public RegistryActivityManager registryActivityManager;

    /// <summary>
    /// The identification code for this sensor
    /// </summary>

    public string code;

    /// <summary>
    /// The debug flag for this sensor
    /// </summary>
    [Tooltip("The debug flag for this sensor")]
    
    public bool debug = true;

    /// <summary>
    /// The activation Threshold stablish the min value needed to artivate the sensor
    /// </summary>
    [Tooltip("The activation Threshold stablish the min value needed to activate the sensor")]

    public float activationThreshold = 0.1f;

    /// <summary>
    /// The frecuency velocity of read
    /// </summary>
    [Tooltip("The frecuency velocity of read")]
   
    public float frecuency = 1f;



    /// <summary>
    /// Flag to control the export data to registryActivityManager
    /// </summary>
    [Tooltip("Flag to control the export data to registryActivityManager")]

    public bool exportData = true;


    /// <summary>
    /// List of tag gameobject that could attach this sensor
    /// </summary>
    [Tooltip("List of tag gameobject that could attach this sensor")]
    public List<string> tagsAvailable = new List<string>();

    /// <summary>
    /// The state flag value of this sensor
    /// </summary>
    [Tooltip("the state flag value of this sensor")]
    private bool state = false;

    /// <summary>
    /// The number of activation of this sensor
    /// </summary>
    [Tooltip("The number of activation of this sensor")]
    private int numActivations = 0;

    /// <summary>
    /// The float value for this sensor
    /// </summary>
    [Tooltip("The float value for this sensor")]
    private float value = 0;

    /// <summary>
    /// Previous value of sensor to determine if the value of sensor changed
    /// </summary>
    [Tooltip("Previous value of sensor to determine if the value of sensor changed")]
    private float prevValue = 0f;

    /// <summary>
    /// Previous state of the sensor
    /// </summary>
    [Tooltip("Previous state of the sensor")]
    private bool prevState = false;

    /// <summary>
    /// The time in seconds since the last read of sensor
    /// </summary>
    [Tooltip("The time in seconds since the last read of sensor")]
    private float timeLastRead = 0f;

    [Description("The code that identify the sensor")]
    public string Code
    {
        get
        {
            return this.code;
        }
        set
        {
            this.code = value;
        }
    }

    [Description("To activate the debug tests")]
    public bool Debug
    {
        get
        {
            return this.debug;
        }
        set
        {
            this.debug = value;
        }
    }

    [Description("The minimal signal to activate the sensor")]
    public float ActivationThreshold
    {
        get
        {
            return this.activationThreshold;
        }
        set
        {
            this.activationThreshold = value;
        }
    }

    [Description("The frecuency velocity of read")]
    public float Frecuency
    {
        get
        {
            return this.frecuency;
        }
        set
        {
            this.frecuency = value;
        }
    }

    [Description("Control if this sensor export output data")]
    public bool ExportData
    {
        get
        {
            return this.exportData;
        }
        set
        {
            this.exportData = value;
        }
    }

    public float Value
    {
        get
        {
            return this.value;
        }
        set
        {
            this.value = value;
        }
    }

    public bool State
    {
        get
        {
            return this.state;
        }
        set
        {
            this.state = value;
        }
    }

    public int NumActivations
    {
        get
        {
            return this.numActivations;
        }
        set
        {
            this.numActivations = value;
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
        if(exportData == true && registryActivityManager != null)
        {
            //If is active the export of data y this sensor has attached with a registry activity manager
            registryActivityManager.notifyEvent(code + eventNotification);
            
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
        if (Time.time - timeLastRead > 1 / this.frecuency)
        {

            //If exist a change of value
            if (value != prevValue)
            {
                //set the value
                this.value = value;

                //notify the on, change or off event
                if (this.value != 0f && !prevState && value >= activationThreshold)
                {
                    state = true;
                    numActivations++;
                    notifyEvent("\tON\t" + this.value);
                }
                else
                if (this.value != 0f && prevState && value >= activationThreshold)
                    notifyEvent("\tCH\t" + this.value);
                else
                {
                    state = false;
                    notifyEvent("\tOFF\t");
                }

                //Asign previous values
                prevState = state;
                prevValue = this.value;

                return true;
            }
            timeLastRead = Time.time;
            return false;
        }else
            return false;
        
    }
}
