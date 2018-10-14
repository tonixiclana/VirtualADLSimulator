using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SensFloorUnderlaySfLr : MonoBehaviour {

    private static int _id = 0;
    public Sensor _sensor = new Sensor();
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr0;
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr1;
    public float[,] _values = new float[2, 8];
 

    // Use this for initialization
    void Start()
    {
        if (_sensor._code == "")
            _sensor._code = gameObject.name + _id++;

        StartCoroutine(readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr0, _values, 0));
        StartCoroutine(readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr1, _values, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (_sensFloorUnderlayMatLr0._sensor._state || _sensFloorUnderlayMatLr1._sensor._state)
            _sensor._state = true;
        else
            _sensor._state = false;

        if (_sensFloorUnderlayMatLr0._sensor._debug != _sensor._debug)
            _sensFloorUnderlayMatLr0._sensor._debug = _sensor._debug;
        if (_sensFloorUnderlayMatLr1._sensor._debug != _sensor._debug)
            _sensFloorUnderlayMatLr1._sensor._debug = _sensor._debug;

        if (_sensFloorUnderlayMatLr0._sensor._value >= _sensFloorUnderlayMatLr1._sensor._value)
            _sensor._value = _sensFloorUnderlayMatLr0._sensor._value;
        else
            _sensor._value = _sensFloorUnderlayMatLr1._sensor._value;
    }

    IEnumerator readSensFloorUnderlayMatLr(SensFloorUnderlayMatLr sensFloorUnderlayMatLr, float[,] values, int coroutineNumber)
    {

        while (true)
        {
            for (int i = 0; i < 8; i++)
                if (values[coroutineNumber, i] != sensFloorUnderlayMatLr._values[i])
                {
                    if(values[coroutineNumber, i] < _sensor._activationThreshold && sensFloorUnderlayMatLr._values[i] >= _sensor._activationThreshold)
                    {
                        _sensor.notifyEvent(sensFloorUnderlayMatLr._sensor._code
                            + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._sensor._code
                            + "\tON\t" + sensFloorUnderlayMatLr._values[i]);
                    }
                    else
                    {
                        if(sensFloorUnderlayMatLr._values[i] <= _sensor._activationThreshold)
                        {
                            _sensor.notifyEvent(sensFloorUnderlayMatLr._sensor._code
                                + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._sensor._code
                                + "\tOFF");
                        }
                        else{
                            _sensor.notifyEvent(sensFloorUnderlayMatLr._sensor._code
                                + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._sensor._code
                                + "\tCH\t" + sensFloorUnderlayMatLr._values[i]);
                        }

                    }
                    values[coroutineNumber, i] = sensFloorUnderlayMatLr._values[i];

                }

            yield return new WaitForSecondsRealtime(1 / _sensor._frecuency);
        }


    }
}
