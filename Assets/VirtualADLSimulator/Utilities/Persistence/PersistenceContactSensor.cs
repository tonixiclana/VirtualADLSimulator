using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceContactSensor  : PersistenceSensor<PersistenceContactSensor>, IPersistenceComponent<PersistenceContactSensor>
{
    /// <summary>
    /// The tag of the contactor
    /// </summary>
    public string contactTag;

    public void addComponentInGameobject(GameObject gm)
    {
        if (_code != "")
        {
            gm.AddComponent<ContactSensor>();
            gm.GetComponent<ContactSensor>().code = _code;
            gm.GetComponent<ContactSensor>().debug = _debug;
            gm.GetComponent<ContactSensor>().exportData = _exportData;
            gm.GetComponent<ContactSensor>().ContactTag = contactTag;
            gm.GetComponent<ContactSensor>().frecuency = _frecuency;
        }
        
  
    }

    public PersistenceContactSensor loadComponentInfo(GameObject gm)
    {

        if (gm.GetComponent<ContactSensor>() != null)
        {
            _code = gm.GetComponent<ContactSensor>().code;
            _activationThreshold = gm.GetComponent<ContactSensor>().activationThreshold;
            _debug = gm.GetComponent<ContactSensor>().debug;
            _exportData = gm.GetComponent<ContactSensor>().exportData;
            _frecuency = gm.GetComponent<ContactSensor>().frecuency;
            contactTag = gm.GetComponent<ContactSensor>().ContactTag;
        }
        

        return this;
    }
}
