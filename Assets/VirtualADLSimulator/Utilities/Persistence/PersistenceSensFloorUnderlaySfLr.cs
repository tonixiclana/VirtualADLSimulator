using UnityEngine;
using System.Collections;

[System.Serializable]
public class PersistenceSensFloorUnderlaySfLr : PersistenceSensor<PersistenceSensFloorUnderlaySfLr>, IPersistenceComponent<PersistenceSensFloorUnderlaySfLr>
{
    /// <summary>
    /// The flag to allow the depthest notification events
    /// </summary>
    public bool _exportDetailPosition;

    public void addComponentInGameobject(GameObject gm)
    {
        if (_code != "")
        {
            gm.AddComponent<SensFloorUnderlaySfLr>();
            gm.GetComponent<SensFloorUnderlaySfLr>()._code = _code;
            gm.GetComponent<SensFloorUnderlaySfLr>()._debug = _debug;
            gm.GetComponent<SensFloorUnderlaySfLr>()._exportData = _exportData;
            gm.GetComponent<SensFloorUnderlaySfLr>()._exportDetailPosition = _exportDetailPosition;
            gm.GetComponent<SensFloorUnderlaySfLr>()._frecuency = _frecuency;
        }
    }

    public PersistenceSensFloorUnderlaySfLr loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<SensFloorUnderlaySfLr>() != null)
        {
            _code = gm.GetComponent<SensFloorUnderlaySfLr>()._code;
            _activationThreshold = gm.GetComponent<SensFloorUnderlaySfLr>()._activationThreshold;
            _debug = gm.GetComponent<SensFloorUnderlaySfLr>()._debug;
            _exportData = gm.GetComponent<SensFloorUnderlaySfLr>()._exportData;
            _frecuency = gm.GetComponent<SensFloorUnderlaySfLr>()._frecuency;
            _exportDetailPosition = gm.GetComponent<SensFloorUnderlaySfLr>()._exportDetailPosition;
        }

        return this;
    }
}
