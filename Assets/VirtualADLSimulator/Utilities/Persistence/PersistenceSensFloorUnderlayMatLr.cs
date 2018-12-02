using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PersistenceSensFloorUnderlayMatLr : PersistenceSensor<PersistenceSensFloorUnderlayMatLr>, IPersistenceComponent<PersistenceSensFloorUnderlayMatLr>
{
    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<SensFloorUnderlayMatLr>();
        gm.GetComponent<SensFloorUnderlayMatLr>()._code = _code;
        gm.GetComponent<SensFloorUnderlayMatLr>()._debug = _debug;
        gm.GetComponent<SensFloorUnderlayMatLr>()._exportData = _exportData;
        gm.GetComponent<SensFloorUnderlayMatLr>()._frecuency = _frecuency;
    }

    public PersistenceSensFloorUnderlayMatLr loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<SensFloorUnderlayMatLr>() != null)
        {
            _code = gm.GetComponent<SensFloorUnderlayMatLr>()._code;
            _activationThreshold = gm.GetComponent<SensFloorUnderlayMatLr>()._activationThreshold;
            _debug = gm.GetComponent<SensFloorUnderlayMatLr>()._debug;
            _exportData = gm.GetComponent<SensFloorUnderlayMatLr>()._exportData;
            _frecuency = gm.GetComponent<SensFloorUnderlayMatLr>()._frecuency;
          
        }

        return this;
    }
}
