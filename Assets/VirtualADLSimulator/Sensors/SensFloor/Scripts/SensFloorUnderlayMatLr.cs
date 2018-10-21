using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensFloorUnderlayMatLr : Sensor{

    private static int _id = 0;
    private float _prevValue = 0f;
    private bool _prevState = false;
    public CapacitiveProximitySensor[] _capacitiveProximitySensors = new CapacitiveProximitySensor[8];
    public float[] _values = new float[8];


    // Use this for initialization
    void Start () {
        if (_code == "")
            _code = gameObject.name + _id++;

        StartCoroutine(readCapacitiveProximitySensor(_capacitiveProximitySensors, _values));
        for (int i = 0; i < 8; i++)
            if (_capacitiveProximitySensors[i]._debug != _debug)
                _capacitiveProximitySensors[i]._debug = _debug;
    }
	
	// Update is called once per frame
	void Update () {


        for (int i = 0; i < 8; i++)
            if (_capacitiveProximitySensors[i]._debug != _debug)
                _capacitiveProximitySensors[i]._debug = _debug;

     


    }

    public void exportData()
    {
        if (_exportData && RegistryActivityManager != null)
        {
            if (_value != _prevValue)
            {
                if (_value != 0f && !_prevState)
                    notifyEvent("\tON\t" + _value);
                else
                if (_value != 0f && _prevState)
                    notifyEvent("\tCH\t" + _value);
                else
                    notifyEvent("\tOFF\t");
                _prevState = _state;
                _prevValue = _value;
            }

        }
    }

    IEnumerator readCapacitiveProximitySensor(CapacitiveProximitySensor[] capacitiveProximitySensors, float[] values)
    {
        while (true)
        {
            bool state = false;
            int numActivations = 0;
            float value = 0f;
            for (int i = 0; i < 8; i++)
            {
                if (capacitiveProximitySensors[i]._state)
                {
                    values[i] = capacitiveProximitySensors[i]._value;
                    _state = true;
                    if (!state)
                        state = true;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                values[i] = capacitiveProximitySensors[i]._value;
                numActivations += capacitiveProximitySensors[i]._numActivations;
            }

            if (!state)
                _state = false;

            _value = values.Max();
            _numActivations = numActivations;

            exportData();

            yield return new WaitForSecondsRealtime(1 / _frecuency);
        }


    }
}
