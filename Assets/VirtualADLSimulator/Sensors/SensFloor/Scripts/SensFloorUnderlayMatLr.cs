/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This script simulate the SENSFLOOR® UNDERLAY Mat LR of FutureShape, you can find information about this gadget in the documentation or here
 * https://data.future-shape.com/Future-Shape_CATALOG_4-2016.pdf
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// This class control the signals of 8 capacitiveProximitySensors, read the max value 
/// of sensors and set it own value y state var depending of children capacitive sensors
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/SensFloorUnderlayMatLr")]
[System.Serializable]
public class SensFloorUnderlayMatLr : Sensor{

    /// <summary>
    /// List of capacitive 8 proximity sensors that has been attached with this dispositive
    /// </summary>
    [Tooltip("List of capacitive 8 proximity sensors that has been attached with this dispositive")]
    public List<CapacitiveProximitySensor> _capacitiveProximitySensors = new List<CapacitiveProximitySensor>();

    /// <summary>
    /// Array with the values of 8 proximity Sensors
    /// </summary>
    [Tooltip("Array with the values of 8 proximity Sensors")]
    public float[] _values;

    /// <summary>
    /// Id to assign a code if the code var is empty
    /// </summary>
    private static int _id = 0;


    void Start () {

            foreach (var sensor in GetComponentsInChildren<CapacitiveProximitySensor>())
            {
                _capacitiveProximitySensors.Add(sensor);
            }

            //set the size of values array
            _values = new float[_capacitiveProximitySensors.Count];

            // If the code is empty assign automatically a code name based in the convention, for SensFloorUnderlayMatLr sensor is: R{id}
            if (code == "")
                code = gameObject.name + _id++;

            //set the children's sensor with the same debug flag that this sensor
            for (int i = 0; i < _capacitiveProximitySensors.Count; i++)
                if (_capacitiveProximitySensors[i].debug != debug)
                    _capacitiveProximitySensors[i].debug = debug;

        
    }
	
	void FixedUpdate () {
        //setting the debug flag in the childrens
        for (int i = 0; i < _capacitiveProximitySensors.Count; i++)
            if (_capacitiveProximitySensors[i].debug != debug)
                _capacitiveProximitySensors[i].debug = debug;

        

        //read the sensors attached
        readCapacitiveProximitySensors();
    
    }

    /// <summary>
    /// Read the 8 values of capacitive proximity sensors attached, and set the own value with the max values of sensors
    /// </summary>
    public void readCapacitiveProximitySensors()
    { 
            float maxValue = 0f;

            for (int i = 0; i < _capacitiveProximitySensors.Count; i++)
            {
                _values[i] = _capacitiveProximitySensors[i].Value;
                if (_capacitiveProximitySensors[i].State)
                {
                    maxValue = (_capacitiveProximitySensors[i].Value > Value) ? _capacitiveProximitySensors[i].Value : Value;
                    if (!this.State)
                    {
                        this.State = true;
                        this.NumActivations++;
                    }
                }
            }
            setSensorValue(maxValue);
    }
}
