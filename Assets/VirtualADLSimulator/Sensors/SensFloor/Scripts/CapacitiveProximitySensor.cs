using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CapacitiveProximitySensor : MonoBehaviour{

    private static int _id = 0;
    public Sensor _sensor = new Sensor();

    public List<string> _gameObjectsInContact = new List<string>();

    // Use this for initialization
    void Start () {
        if (_sensor._code == "")
            _sensor._code = gameObject.name + _id++;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider colInfo)
    {
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null && colInfo.gameObject.GetComponent<Rigidbody>().mass >= _sensor._activationThreshold 
            /*&& colInfo.gameObject.layer != 9*/)
        {
           /* if (!_gameObjectsInContact.Contains(colInfo.gameObject.name))
            {*/

                if (!_sensor._state)
                {
                    _sensor._state = true;
                    _sensor._numActivations++;
                    if (_sensor._debug)
                        this.GetComponent<Renderer>().enabled = true;
                        this.GetComponent<Renderer>().material.color = Color.yellow;
                }

                _gameObjectsInContact.Add(colInfo.gameObject.name);
                StartCoroutine(readSensor(colInfo));

                /*if (_sensor._debug)
                    Debug.Log(gameObject.name + " ON " + _sensor._value + " " + System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff"));*/
            }
    }
    

    private void OnTriggerExit(Collider colInfo)
    {
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null) {

            _sensor._value -= colInfo.GetComponent<Rigidbody>().mass;
            _gameObjectsInContact.Remove(colInfo.gameObject.name);

            if (_gameObjectsInContact.Count == 0) {
                _sensor._state = false;
                this.GetComponent<Renderer>().enabled = false;

            }
            
           /* if (_sensor._debug)
                Debug.Log(gameObject.name + " OFF " + _sensor._value + " " + System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff"));*/
        }
    }

    IEnumerator readSensor(Collider colInfo)
    {
        _sensor._value += colInfo.GetComponent<Rigidbody>().mass;

        yield return new WaitForSecondsRealtime(0);
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
