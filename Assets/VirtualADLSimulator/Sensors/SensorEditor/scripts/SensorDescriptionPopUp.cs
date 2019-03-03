using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using TMPro;

public class SensorDescriptionPopUp : MonoBehaviour {

    public GameObject popUp;

	// Use this for initialization
	void Start () {
        
	}
	
    public void setDescription(string str)
    {
        popUp.GetComponent<TextMeshProUGUI>().text = str;
    }

	// Update is called once per frame
	void Update () {
	}

}
