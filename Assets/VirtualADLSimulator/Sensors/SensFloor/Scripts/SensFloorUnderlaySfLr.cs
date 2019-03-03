/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This script simulate the SENSFLOOR® UNDERLAY SF LR of FutureShape, you can find information about
 * this gadget in the documentation or here
 * https://data.future-shape.com/Future-Shape_CATALOG_4-2016.pdf
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;

/// <summary>
/// This class control the signals of 16 capacitiveProximitySensors associated, read the max value 
/// of sensors and set it own value y state var depending of children capacitive sensors.
/// This class is composed by 2 sensFloorUnderlayMatLr.
/// </summary>
[AddComponentMenu("ADLVirtualSimulator/SensFloorUnderlaySfLr")]
[System.Serializable]
[Description("Simulate the SENSFLOOR® UNDERLAY SF LR of FutureShape, " +
    "is a floor sensor that detect the footsteps through of Capacitive Proximity Sensor and provide 16 zones of detection in a square meter that can be enable or disable with option")]
public class SensFloorUnderlaySfLr : Sensor {

    /// <summary>
    /// The flag to allow the depthest notification events
    /// </summary>
    [Tooltip("The flag to allow the depthest notification events")]
    public bool exportDetailPosition = true;

    /// <summary>
    /// The firts sensFloorUnderlayMatLr element
    /// </summary>
    [Tooltip("The firts sensFloorUnderlayMatLr element")]
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr0;

    /// <summary>
    /// The second sensFloorUnderlayMatLr element
    /// </summary>
    [Tooltip("The second sensFloorUnderlayMatLr element")]
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr1;


    /// <summary>
    /// A matrix with the values of 16 sensors, distribuited in 2 row an 8 columns
    /// </summary>
    [Tooltip("A matrix with the values of 16 sensors, distribuited in 2 row an 8 columns")]
    private float[,] _values = new float[2, 8];

    /// <summary>
    /// Id to assign a code if the code var is empty
    /// </summary>
    private static int _id = 0;

    [Description("Allow export a detail output of this sensow, using all the capacitive sensors")]
    public bool ExportDetailPosition
    {
        get
        {
            return this.exportDetailPosition;
        }
        set
        {
            this.exportDetailPosition = value;
        }
    }
 
    void Awake()
    {

        // If the code is empty assign automatically a code name based in the convention, for SensFloorUnderlaySfLr sensor is: FL{id}
        if (code == "")
            code = "FL" + _id++;
        
    }


    void Start()
    {

        FindObjectOfType<RegistryActivityManager>().addSensor(this);

        while(_sensFloorUnderlayMatLr0 == null || _sensFloorUnderlayMatLr1 == null){
            loadSensorsChildren();
        }
      }


    public void loadSensorsChildren()
    {
        _sensFloorUnderlayMatLr0 = GetComponentsInChildren<SensFloorUnderlayMatLr>()[0];
        _sensFloorUnderlayMatLr1 = GetComponentsInChildren<SensFloorUnderlayMatLr>()[1];

    }



    // Update is called once per frame
    void FixedUpdate()
    {

        // Set the same debug flag in childrens
        if (_sensFloorUnderlayMatLr0.debug != debug)
            _sensFloorUnderlayMatLr0.debug = debug;
        if (_sensFloorUnderlayMatLr1.debug != debug)
            _sensFloorUnderlayMatLr1.debug = debug;

        //set the same export data flag in childrens
        if (_sensFloorUnderlayMatLr0.exportData != exportDetailPosition)
        {
            _sensFloorUnderlayMatLr0.exportData = exportDetailPosition;
            foreach(var sensor in _sensFloorUnderlayMatLr0._capacitiveProximitySensors)
                sensor.exportData = exportDetailPosition;
        }
        if (_sensFloorUnderlayMatLr1.exportData != exportDetailPosition)
        {
            _sensFloorUnderlayMatLr1.exportData = exportDetailPosition;
            foreach (var sensor in _sensFloorUnderlayMatLr1._capacitiveProximitySensors)
                sensor.exportData = exportDetailPosition;
        }


        // Set the max value of childrens
        if (_sensFloorUnderlayMatLr0.Value >= _sensFloorUnderlayMatLr1.Value)
            setSensorValue(_sensFloorUnderlayMatLr0.Value);
        else
            setSensorValue(_sensFloorUnderlayMatLr1.Value);

        if (exportDetailPosition)
        {
            readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr0, _values, 0);
            readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr1, _values, 1);
        }
    }

    /// <summary>
    /// Receive a sensFloorUnderlayMatLr object, a matrix of values and a row, and generate the notify 
    /// events with the changes in the childrens capacitiveProximitySensors, showing a more detail about the origin of signal
    /// This is a example of notify format: FLXRYCPSZ, The X, Y and Z correspond at the index, FL0R0CPS0 correspond a signal 
    /// of firts capacitive proximity sensor inside FL0 
    /// </summary>
    /// <param name="sensFloorUnderlayMatLr"></param>
    /// <param name="values"></param>
    /// <param name="row"></param>
   public void readSensFloorUnderlayMatLr(SensFloorUnderlayMatLr sensFloorUnderlayMatLr, float[,] values, int row)
    {
        // Iterate the row values
        for (int i = 0; i < 8; i++)
            // If the value has changed
            if (values[row, i] != sensFloorUnderlayMatLr._values[i])
            {
                // Notify the ON, OFF, CH events with the detail code 
                if (values[row, i] < activationThreshold && sensFloorUnderlayMatLr._values[i] >= activationThreshold)
                {
                    notifyEvent(sensFloorUnderlayMatLr.code
                        + sensFloorUnderlayMatLr._capacitiveProximitySensors[i].code
                        + "\tON\t" + sensFloorUnderlayMatLr._values[i]);
                }
                else  
                if (values[row, i] >= activationThreshold && sensFloorUnderlayMatLr._values[i] < activationThreshold)
                {
                    notifyEvent(sensFloorUnderlayMatLr.code
                        + sensFloorUnderlayMatLr._capacitiveProximitySensors[i].code
                        + "\tOFF");
                }
                else
                {
                    notifyEvent(sensFloorUnderlayMatLr.code
                        + sensFloorUnderlayMatLr._capacitiveProximitySensors[i].code
                        + "\tCH\t" + sensFloorUnderlayMatLr._values[i]);
                }

                values[row, i] = sensFloorUnderlayMatLr._values[i];
            }
    }
}
