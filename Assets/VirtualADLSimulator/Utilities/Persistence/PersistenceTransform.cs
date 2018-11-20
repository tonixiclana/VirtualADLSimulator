using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PersistenceTransform {

    public int parentId = -1;
    public int layer;
    public string tag;
    public Vector3 goPosition;
    public Quaternion goRotation;
    public Vector3 goScale;

}
