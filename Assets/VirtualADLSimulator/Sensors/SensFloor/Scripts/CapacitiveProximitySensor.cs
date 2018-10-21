using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CapacitiveProximitySensor : Sensor{

    private static int _id = 0;
    private float _prevValue = 0f;
    private bool _prevState = false;
    public List<string> _gameObjectsInContact = new List<string>();

    // Use this for initialization
    void Start () {
        if (_code == "")
            _code = gameObject.name + _id++;
    }
	
	// Update is called once per frame
	void Update () {

  
	}

    private void OnTriggerEnter(Collider colInfo)
    {
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null && colInfo.gameObject.GetComponent<Rigidbody>().mass >= _activationThreshold 
            /*&& colInfo.gameObject.layer != 9*/)
        {

                if (!_state)
                {
                    _state = true;
                    _numActivations++;
                    if (_debug)
                        this.GetComponent<Renderer>().enabled = true;
                        this.GetComponent<Renderer>().material.color = Color.yellow;
                }

                _gameObjectsInContact.Add(colInfo.gameObject.name);
                StartCoroutine(readSensor(colInfo));
            }
    }
    

    private void OnTriggerExit(Collider colInfo)
    {
        if (colInfo.gameObject.GetComponent<Rigidbody>() != null) {

            _value -= colInfo.GetComponent<Rigidbody>().mass;
            _gameObjectsInContact.Remove(colInfo.gameObject.name);

            if (_gameObjectsInContact.Count == 0) {
                _state = false;
                this.GetComponent<Renderer>().enabled = false;

            }
            
           /* if (_sensor._debug)
                Debug.Log(gameObject.name + " OFF " + _sensor._value + " " + System.DateTime.Now.ToString("MM/dd/yy HH:mm:ss.ffffff"));*/
        }
    }

    IEnumerator readSensor(Collider colInfo)
    {
        _value += colInfo.GetComponent<Rigidbody>().mass;

        if (_exportData && RegistryActivityManager != null)
        {
            if (_value != _prevValue)
            {
                if (_value != 0f && !_prevState)
                    notifyEvent("\tON\t" + _value);
                else
                if (_value != 0f && _prevState)
                    notifyEvent("\tCH\t" + _value);
                else
                    notifyEvent("\tOFF\t");
                _prevState = _state;
                _prevValue = _value;
            }

        }

        yield return new WaitForSecondsRealtime(1 / _frecuency);
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
