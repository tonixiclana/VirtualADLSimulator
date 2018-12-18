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
        gm.AddComponent<ContactSensor>().contactTag = contactTag;
    }

    public PersistenceContactSensor loadComponentInfo(GameObject gm)
    {
        contactTag = gm.GetComponent<ContactSensor>().contactTag;

        return this;
    }
}
