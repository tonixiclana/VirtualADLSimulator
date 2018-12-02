using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPersistenceComponent<T> {

    void addComponentInGameobject(GameObject gm);
    T loadComponentInfo(GameObject gm);

}
