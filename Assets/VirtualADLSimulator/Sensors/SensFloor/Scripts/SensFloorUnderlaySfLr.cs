using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SensFloorUnderlaySfLr : Sensor {

    private static int _id = 0;
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr0;
    public SensFloorUnderlayMatLr _sensFloorUnderlayMatLr1;
    public float[,] _values = new float[2, 8];
 

    // Use this for initialization
    void Start()
    {
        if (_code == "")
            _code = gameObject.name + _id++;

        StartCoroutine(readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr0, _values, 0));
        StartCoroutine(readSensFloorUnderlayMatLr(_sensFloorUnderlayMatLr1, _values, 1));
    }

    // Update is called once per frame
    void Update()
    {
        if (_sensFloorUnderlayMatLr0._state || _sensFloorUnderlayMatLr1._state)
            _state = true;
        else
            _state = false;

        if (_sensFloorUnderlayMatLr0._debug != _debug)
            _sensFloorUnderlayMatLr0._debug = _debug;
        if (_sensFloorUnderlayMatLr1._debug != _debug)
            _sensFloorUnderlayMatLr1._debug = _debug;

        if (_sensFloorUnderlayMatLr0._value >= _sensFloorUnderlayMatLr1._value)
            _value = _sensFloorUnderlayMatLr0._value;
        else
            _value = _sensFloorUnderlayMatLr1._value;
    }

    IEnumerator readSensFloorUnderlayMatLr(SensFloorUnderlayMatLr sensFloorUnderlayMatLr, float[,] values, int coroutineNumber)
    {

        while (true)
        {
            for (int i = 0; i < 8; i++)
                if (values[coroutineNumber, i] != sensFloorUnderlayMatLr._values[i])
                {
                    if(values[coroutineNumber, i] < _activationThreshold && sensFloorUnderlayMatLr._values[i] >= _activationThreshold)
                    {
                        notifyEvent(sensFloorUnderlayMatLr._code
                            + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._code
                            + "\tON\t" + sensFloorUnderlayMatLr._values[i]);
                    }
                    else
                    {
                        if(sensFloorUnderlayMatLr._values[i] <= _activationThreshold)
                        {
                            notifyEvent(sensFloorUnderlayMatLr._code
                                + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._code
                                + "\tOFF");
                        }
                        else{
                            notifyEvent(sensFloorUnderlayMatLr._code
                                + sensFloorUnderlayMatLr._capacitiveProximitySensors[i]._code
                                + "\tCH\t" + sensFloorUnderlayMatLr._values[i]);
                        }

                    }
                    values[coroutineNumber, i] = sensFloorUnderlayMatLr._values[i];

                }

            yield return new WaitForSecondsRealtime(1 / _frecuency);
        }


    }
}
