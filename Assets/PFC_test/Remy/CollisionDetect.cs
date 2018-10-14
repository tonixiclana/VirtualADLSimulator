using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetect : MonoBehaviour {

	public int collisionCount = 0;
	private bool _isGrounded;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (collisionCount == 0)
			_isGrounded = false;
		else
			_isGrounded = true;
	}

	bool isGrounded(){
		return _isGrounded;
	}

	void OnCollisionEnter (Collision colInfo)
	{
		foreach (ContactPoint contact in colInfo.contacts) {
			if (Vector3.Angle (contact.normal, Vector3.up) < 45)
				Debug.DrawRay (contact.point, contact.normal, Color.green, 2);
		}
		collisionCount++;
	}

	void OnCollisionExit (Collision colInfo)
	{
		collisionCount--;
	}
}
