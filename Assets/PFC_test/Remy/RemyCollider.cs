using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemyCollider : MonoBehaviour {

	public SkinnedMeshRenderer meshRenderer;
	public MeshCollider _collider;

	// Use this for initialization
	void Start () {
		_collider.inflateMesh = true;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCollider ();
	}
		
	public void UpdateCollider() {
		Mesh colliderMesh = new Mesh();
		meshRenderer.BakeMesh(colliderMesh);
		_collider.sharedMesh = null;
		_collider.sharedMesh = colliderMesh;
	
	}

	void OnCollisionEnter (Collision colInfo)
	{
		Debug.Log ("in");
		/*foreach (ContactPoint contact in colInfo.contacts) {
			if (Vector3.Angle (contact.normal, Vector3.up) < 45)
				Debug.DrawRay (contact.point, contact.normal, Color.green, 2);
		}*/
	}
}
