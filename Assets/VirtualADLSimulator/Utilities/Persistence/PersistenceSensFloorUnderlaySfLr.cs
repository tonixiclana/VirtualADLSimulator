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
            gm.GetComponent<SensFloorUnderlaySfLr>().code = _code;
            gm.GetComponent<SensFloorUnderlaySfLr>().debug = _debug;
            gm.GetComponent<SensFloorUnderlaySfLr>().exportData = _exportData;
            gm.GetComponent<SensFloorUnderlaySfLr>().exportDetailPosition = _exportDetailPosition;
            gm.GetComponent<SensFloorUnderlaySfLr>().frecuency = _frecuency;
        }
    }

    public PersistenceSensFloorUnderlaySfLr loadComponentInfo(GameObject gm)
    {
        if (gm.GetComponent<SensFloorUnderlaySfLr>() != null)
        {
            _code = gm.GetComponent<SensFloorUnderlaySfLr>().code;
            _activationThreshold = gm.GetComponent<SensFloorUnderlaySfLr>().activationThreshold;
            _debug = gm.GetComponent<SensFloorUnderlaySfLr>().debug;
            _exportData = gm.GetComponent<SensFloorUnderlaySfLr>().exportData;
            _frecuency = gm.GetComponent<SensFloorUnderlaySfLr>().frecuency;
            _exportDetailPosition = gm.GetComponent<SensFloorUnderlaySfLr>().exportDetailPosition;
        }

        return this;
    }
}
