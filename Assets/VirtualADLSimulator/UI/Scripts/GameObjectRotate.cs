using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectRotate : MonoBehaviour {

    public bool rotateToRight = false;
    public float rotateSpeed = 10;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0,0,(rotateToRight)? -rotateSpeed : +rotateSpeed));
	}
}
