using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SensFloorUnderlayMatLr : MonoBehaviour{

    private static int _id = 0;
    public Sensor _sensor = new Sensor();
    public CapacitiveProximitySensor[] _capacitiveProximitySensors = new CapacitiveProximitySensor[8];
    public float[] _values = new float[8]; 

    // Use this for initialization
    void Start () {
        if (_sensor._code == "")
            _sensor._code = gameObject.name + _id++;

        StartCoroutine(readCapacitiveProximitySensor(_capacitiveProximitySensors, _values));
        for (int i = 0; i < 8; i++)
            if (_capacitiveProximitySensors[i]._sensor._debug != _sensor._debug)
                _capacitiveProximitySensors[i]._sensor._debug = _sensor._debug;
    }
	
	// Update is called once per frame
	void Update () {


        for (int i = 0; i < 8; i++)
            if (_capacitiveProximitySensors[i]._sensor._debug != _sensor._debug)
                _capacitiveProximitySensors[i]._sensor._debug = _sensor._debug;


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
                if (capacitiveProximitySensors[i]._sensor._state)
                {
                    values[i] = capacitiveProximitySensors[i]._sensor._value;
                    _sensor._state = true;
                    if (!state)
                        state = true;
                }
            }

            for (int i = 0; i < 8; i++)
            {
                values[i] = capacitiveProximitySensors[i]._sensor._value;
                numActivations += capacitiveProximitySensors[i]._sensor._numActivations;
            }

            if (!state)
                _sensor._state = false;

            _sensor._value = values.Max();
            _sensor._numActivations = numActivations;
            yield return new WaitForSecondsRealtime(1 / _sensor._frecuency);
        }


    }
}
