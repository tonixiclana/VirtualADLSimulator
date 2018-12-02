using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenceTransformActuator : PersistenceComponent<PersistenceTransformActuator>, IPersistenceComponent<PersistenceTransformActuator> {

    public bool enable;
    public bool enableRotationActuator;
    public bool enablePositionActuator;

    public bool enableReverseAction;
    public float rotationDegreesPerSecond;
    public float positionUnitsPerSecond;

    public bool isLocalRotation;
    public Vector3 targetRotation;
    public Vector3 originalRotation;

    public bool isLocalPosition;
    public Vector3 targetPosition;
    public Vector3 originalPosition;

    public void addComponentInGameobject(GameObject gm)
    {
        gm.AddComponent<TransformActuator>().enabled = enable;
        gm.GetComponent<TransformActuator>().enablePositionActuator = enablePositionActuator;
        gm.GetComponent<TransformActuator>().enableRotationActuator = enableRotationActuator;
        gm.GetComponent<TransformActuator>().enableReverseAction = enableReverseAction;
        gm.GetComponent<TransformActuator>().rotationDegreesPerSecond = rotationDegreesPerSecond;
        gm.GetComponent<TransformActuator>().positionUnitsPerSecond = positionUnitsPerSecond;
        gm.GetComponent<TransformActuator>().isLocalPosition = isLocalPosition;
        gm.GetComponent<TransformActuator>().targetPosition = targetPosition;
        gm.GetComponent<TransformActuator>().originalPosition = originalPosition;
        gm.GetComponent<TransformActuator>().isLocalRotation = isLocalRotation;
        gm.GetComponent<TransformActuator>().targetRotation = targetRotation;
        gm.GetComponent<TransformActuator>().originalRotation = originalRotation;
    }

    public PersistenceTransformActuator loadComponentInfo(GameObject gm)
    {
        enable = gm.GetComponent<TransformActuator>().enabled;
        enablePositionActuator = gm.GetComponent<TransformActuator>().enablePositionActuator;
        enableRotationActuator = gm.GetComponent<TransformActuator>().enableRotationActuator;
        enableReverseAction = gm.GetComponent<TransformActuator>().enableReverseAction;
        rotationDegreesPerSecond = gm.GetComponent<TransformActuator>().rotationDegreesPerSecond;
        positionUnitsPerSecond = gm.GetComponent<TransformActuator>().positionUnitsPerSecond;
        isLocalPosition = gm.GetComponent<TransformActuator>().isLocalPosition;
        targetPosition = gm.GetComponent<TransformActuator>().targetPosition;
        originalPosition = gm.GetComponent<TransformActuator>().originalPosition;
        isLocalRotation = gm.GetComponent<TransformActuator>().isLocalRotation;
        targetRotation = gm.GetComponent<TransformActuator>().targetRotation;
        originalRotation = gm.GetComponent<TransformActuator>().originalRotation;

        return this;
    }


}
