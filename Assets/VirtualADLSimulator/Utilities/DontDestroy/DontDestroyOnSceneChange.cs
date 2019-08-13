using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnSceneChange : MonoBehaviour {

	// Use this for initialization
	void Awake () {

        DontDestroyOnLoad(this.gameObject);

        /*
        if (GameObject.fin.Find(gameObject.name).Count() < 2)
            DontDestroyOnLoad(this.gameObject);
        else
            Destroy(gameObject);
            */
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
