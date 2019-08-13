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
        popUp.GetComponentInChildren<TextMeshProUGUI>().text = str;
    }

    public void showPopUp()
    {
        popUp.SetActive(true);
    }

    public void hidePopUp()
    {
        popUp.SetActive(false);
    }

    // Update is called once per frame
    void Update () {
	}

}
