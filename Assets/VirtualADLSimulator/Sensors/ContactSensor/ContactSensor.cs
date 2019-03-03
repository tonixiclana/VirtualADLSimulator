using UnityEngine;
using System;
using System.Xml.Linq;
using System.ComponentModel;
/// <summary>
/// This script simulate the working of a contact sensor, detect collision with a trigger and normal colliders
/// </summary>
[RequireComponent(typeof(Collider))]

[System.Serializable]
[Description("Contact sensor is used to detect if two bodies are in contact")]
public class ContactSensor: Sensor{


    /// <summary>
    /// Id to assign a code if the code var is empty
    /// </summary>
    private static int _id = 0;

    [SerializeField]
    private string contactTag;

    
    [Description("The contact tag")]
    public string ContactTag
    {
        get
        {
            return this.contactTag;
        }
        set
        {
            this.contactTag = value;
        }
    }
    

    private Vector3 originalPosition;
    private Quaternion originalRotation;


    private void Awake()
    {
        // If the code is empty assign automatically a code name based in the convention, for contact Proximity sensor is: CONTACTSENSOR{id}
        if (code == "")
            code = "CONTACTSENSOR" + _id++;
    }


    void Start()
    {

        FindObjectOfType<RegistryActivityManager>().addSensor(this);

        originalPosition = transform.parent.position;
        transform.position = originalPosition;
        //transform.localPosition += new Vector3(-0.1f,0,0);
        originalPosition = transform.position;

      
        originalRotation = transform.rotation;
        transform.rotation = originalRotation;
    }


    private void FixedUpdate()
    {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
    }

    private void OnTriggerEnter(Collider colInfo)
    {

        if(colInfo.transform.tag == contactTag)
        {
            setSensorValue(1);
        }
    }

    

    private void OnTriggerExit(Collider colInfo)
    {

        if (colInfo.transform.tag == contactTag)
        {
            setSensorValue(0);
        }

    }

    /*

    /// <summary>
    /// Run when a gameobject collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnCollisionEnter(Collision colInfo)
    {

        
        setSensorValue(1);
        value = 1;
        
    }

    /// <summary>
    /// Run when a gameobject leave the collision with the sensor collider and the gameobject is a trigger and have rigidbody
    /// </summary>
    /// <param name="colInfo"></param>
    private void OnCollisionExit(Collision colInfo)
    {
        //Apply the rigidbody validation
        setSensorValue(0);
  
        value = 0;

    }
    */
}
