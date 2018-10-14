using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCapture : MonoBehaviour {

	public KeyCode RUNBUTTOM = KeyCode.LeftShift;

	static public Vector2 axis = new Vector2();
	static public bool runButtom = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		axis = new Vector2(Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical")).normalized;
		runButtom = Input.GetKey (RUNBUTTOM);
	}
		
}
