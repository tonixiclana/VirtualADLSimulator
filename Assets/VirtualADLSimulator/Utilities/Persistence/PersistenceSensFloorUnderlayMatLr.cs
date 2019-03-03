using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PersistenceSensFloorUnderlayMatLr : PersistenceSensor<PersistenceSensFloorUnderlayMatLr>, IPersistenceComponent<PersistenceSensFloorUnderlayMatLr>
{
    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<SensFloorUnderlayMatLr>();
        gm.GetComponent<SensFloorUnderlayMatLr>().code = _code;
        gm.GetComponent<SensFloorUnderlayMatLr>().debug = _debug;
        gm.GetComponent<SensFloorUnderlayMatLr>().exportData = _exportData;
        gm.GetComponent<SensFloorUnderlayMatLr>().frecuency = _frecuency;
    }

    public PersistenceSensFloorUnderlayMatLr loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<SensFloorUnderlayMatLr>() != null)
        {
            _code = gm.GetComponent<SensFloorUnderlayMatLr>().code;
            _activationThreshold = gm.GetComponent<SensFloorUnderlayMatLr>().activationThreshold;
            _debug = gm.GetComponent<SensFloorUnderlayMatLr>().debug;
            _exportData = gm.GetComponent<SensFloorUnderlayMatLr>().exportData;
            _frecuency = gm.GetComponent<SensFloorUnderlayMatLr>().frecuency;
          
        }

        return this;
    }
}
