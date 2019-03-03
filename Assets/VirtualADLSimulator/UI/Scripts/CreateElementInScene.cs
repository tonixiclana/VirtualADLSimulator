using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateElementInScene : MonoBehaviour {

    public SelectGameobjectCursor selectGameobjectCursor;

    GameObject createFromPrefab(GameObject gm, GameObject parent)
    {
        return Instantiate(gm, parent.transform);
    }

    private void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {

        //highlighingByTag("Floor")
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
