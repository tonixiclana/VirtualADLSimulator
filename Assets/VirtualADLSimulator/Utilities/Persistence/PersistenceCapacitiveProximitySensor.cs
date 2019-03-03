using UnityEngine;
using System.Collections;

[System.Serializable]
public class PersistenceCapacitiveProximitySensor : PersistenceSensor<PersistenceCapacitiveProximitySensor>, IPersistenceComponent<PersistenceCapacitiveProximitySensor>
{
    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<CapacitiveProximitySensor>();
        gm.GetComponent<CapacitiveProximitySensor>().code = _code;
        gm.GetComponent<CapacitiveProximitySensor>().debug = _debug;
        gm.GetComponent<CapacitiveProximitySensor>().exportData = _exportData;
        gm.GetComponent<CapacitiveProximitySensor>().frecuency = _frecuency;
    }

    public PersistenceCapacitiveProximitySensor loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<CapacitiveProximitySensor>() != null)
        {
            _code = gm.GetComponent<CapacitiveProximitySensor>().code;
            _activationThreshold = gm.GetComponent<CapacitiveProximitySensor>().activationThreshold;
            _debug = gm.GetComponent<CapacitiveProximitySensor>().debug;
            _exportData = gm.GetComponent<CapacitiveProximitySensor>().exportData;
            _frecuency = gm.GetComponent<CapacitiveProximitySensor>().frecuency;

        }

        return this;
    }
}
