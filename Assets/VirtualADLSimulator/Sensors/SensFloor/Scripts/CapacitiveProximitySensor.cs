/*
 * @Author Antonio J Morales Rodríguez
 * 
 * @Version 1.0
 * 
 * @Copyright Antonio J Morales Rodríguez
 * 
 * @Description This script simulate the working of a capacitive sensor
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script simulate the working of a capacitive sensor, detect collision with a trigger and normal colliders
/// </summary>
[RequireComponent(typeof(Collider))]
[AddComponentMenu("ADLVirtualSimulator/CapacitiveProximitySensor")]
[System.Serializable]
public class CapacitiveProximitySensor : Sensor{

    /// <summary>
    /// List with the gameobjects in contact with the sensor
    /// </summary>
    [Tooltip("List with the gameobjects in contact with the sensor")]
    public List<string> _gameObjectsInContact = new List<string>();

    /// <summary>
    /// Id to assign a code if the code var is empty
    /// </summary>
    [Tooltip("Id to assign a code if the code var is empty")]
    private static int _id = 0;


    void Start () {
        
        // If the code is empty assign automatically a code name based in the convention, for capacitive Proximity sensor is: CPS{id}
        if (_code == "")
            _code = "CPS" + _id++;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

  
	}

    /// <summary>
    /// Run when a gameobject collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnTriggerEnter(Collider colInfo)
    {
        //Apply the rigidbody validation
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null)
        {
            _gameObjectsInContact.Add(colInfo.gameObject.name);

            //call to set Sensor value with the mass of rigidbody
            while (!setSensorValue(Value += colInfo.GetComponent<Rigidbody>().mass)) ;


            //If sensor state is true
            if (State)
            {
                //Debug include the highlighted of the capacitive sensor
                if (_debug)
                {
                    this.GetComponent<Renderer>().enabled = true;
                    this.GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
    }

    /// <summary>
    /// Run when a gameobject leave the collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnTriggerExit(Collider colInfo)
    {
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null) {
            setSensorValue(Value -= colInfo.GetComponent<Rigidbody>().mass);
            _gameObjectsInContact.Remove(colInfo.gameObject.name);

            if (!State) 
                //Debug include the highlighted of the capacitive sensor
                if (_debug)
                    this.GetComponent<Renderer>().enabled = false;
            
        }
    }


    /// <summary>
    /// Run when a gameobject collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnCollisionEnter(Collision colInfo)
    {
        //Apply the rigidbody validation
        if (colInfo.collider.GetComponent<Rigidbody>() != null)
        {
            _gameObjectsInContact.Add(colInfo.gameObject.name);

            //call to set Sensor value with the mass of rigidbody
            while (!setSensorValue(Value += colInfo.collider.GetComponent<Rigidbody>().mass)) ;


            //If sensor state is true
            if (State)
            {
                //Debug include the highlighted of the capacitive sensor
                if (_debug)
                {
                    this.GetComponent<Renderer>().enabled = true;
                    this.GetComponent<Renderer>().material.color = Color.yellow;
                }
            }
        }
    }

    /// <summary>
    /// Run when a gameobject leave the collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnCollisionExit(Collision colInfo)
    {
        if (colInfo.collider.GetComponent<Rigidbody>() != null)
        {
            setSensorValue(Value -= colInfo.collider.GetComponent<Rigidbody>().mass);
            _gameObjectsInContact.Remove(colInfo.gameObject.name);

            if (!State)
                //Debug include the highlighted of the capacitive sensor
                if (_debug)
                    this.GetComponent<Renderer>().enabled = false;

        }
    }
    /*
    void OnCollisionEnter(Collision colInfo){
        

        if (colInfo.gameObject.GetComponent<Rigidbody>() != null 
            && colInfo.gameObject.GetComponent<Rigidbody>().mass >= _activationThreshold 
            && colInfo.gameObject.layer != 9)
        {
                if (!_gameObjectsInContact.Contains(colInfo.gameObject.name)){

                    StartCoroutine(readSensor(colInfo));
                    _gameObjectsInContact.Add(colInfo.gameObject.name);

                    if (!_state)
                        _state = true;

                    if (_debug)
                    {
                        StartCoroutine(drawContactPoints(colInfo));
                        _numActivations++;
                        Debug.Log(gameObject.name + " ON " + _value + " " + System.DateTime.Now);
                }
            }
        }

    }

    void OnCollisionExit(Collision colInfo)
    {
        _value -= colInfo.rigidbody.mass;
        _gameObjectsInContact.Remove(colInfo.gameObject.name);
        if (_gameObjectsInContact.Count == 0)
            _state = false;

        if (_debug)
        {
            Debug.Log(gameObject.name + " exits Collision with: " + colInfo.gameObject.name + " at " + System.DateTime.Now);
        }    
    }

            IEnumerator readSensor(Collision colInfo)
        {
            _value += colInfo.rigidbody.mass;

            yield return new WaitForSecondsRealtime(0);
        }

        IEnumerator drawContactPoints(Collision colInfo)
        {
            foreach (ContactPoint contact in colInfo.contacts)
                Debug.DrawRay(contact.point, contact.normal, Color.green, 3);

            yield return new WaitForSeconds(0);
        }*/

}
