using UnityEngine;
using System.Collections;

[System.Serializable]
public class PersistenceCapacitiveProximitySensor : PersistenceSensor<PersistenceCapacitiveProximitySensor>, IPersistenceComponent<PersistenceCapacitiveProximitySensor>
{
    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<CapacitiveProximitySensor>();
        gm.GetComponent<CapacitiveProximitySensor>()._code = _code;
        gm.GetComponent<CapacitiveProximitySensor>()._debug = _debug;
        gm.GetComponent<CapacitiveProximitySensor>()._exportData = _exportData;
        gm.GetComponent<CapacitiveProximitySensor>()._frecuency = _frecuency;
    }

    public PersistenceCapacitiveProximitySensor loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<CapacitiveProximitySensor>() != null)
        {
            _code = gm.GetComponent<CapacitiveProximitySensor>()._code;
            _activationThreshold = gm.GetComponent<CapacitiveProximitySensor>()._activationThreshold;
            _debug = gm.GetComponent<CapacitiveProximitySensor>()._debug;
            _exportData = gm.GetComponent<CapacitiveProximitySensor>()._exportData;
            _frecuency = gm.GetComponent<CapacitiveProximitySensor>()._frecuency;

        }

        return this;
    }
}
