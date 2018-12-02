using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PersistenceRigidbody : PersistenceComponent<PersistenceRigidbody>, IPersistenceComponent<PersistenceRigidbody>
{

    public float mass;
    public float drag;
    public float angularDrag;
    public bool useGravity;
    public bool isKinematic;
    public int constraints;

    public void addComponentInGameobject(GameObject gm)
    {

        gm.AddComponent<Rigidbody>().mass = mass;
        gm.GetComponent<Rigidbody>().drag = drag;
        gm.GetComponent<Rigidbody>().angularDrag = angularDrag;
        gm.GetComponent<Rigidbody>().useGravity = useGravity;
        gm.GetComponent<Rigidbody>().isKinematic = isKinematic;
        gm.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)constraints;

        
    }

    public PersistenceRigidbody loadComponentInfo(GameObject gm)
    {
        mass = gm.GetComponent<Rigidbody>().mass;
        drag = gm.GetComponent<Rigidbody>().drag;
        angularDrag = gm.GetComponent<Rigidbody>().angularDrag;
        useGravity = gm.GetComponent<Rigidbody>().useGravity;
        isKinematic = gm.GetComponent<Rigidbody>().isKinematic;
        constraints = (int)gm.GetComponent<Rigidbody>().constraints;
        
        return this;
    }
}
