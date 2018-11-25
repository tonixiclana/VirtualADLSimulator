using UnityEngine;
using System.Collections;

[System.Serializable]
public class PersistenceSensor
{
    /// <summary>
    /// The identification code for this sensor
    /// </summary>
    public string _code;

    /// <summary>
    /// The debug flag for this sensor
    /// </summary>
    public bool _debug;

    /// <summary>
    /// The activation Threshold stablish the min value needed to artivate the sensor
    /// </summary>
    public float _activationThreshold ;

    /// <summary>
    /// The frecuency velocity of read
    /// </summary>
    public float _frecuency;

    /// <summary>
    /// Flag to control the export data to registryActivityManager
    /// </summary>
    public bool _exportData;

}
