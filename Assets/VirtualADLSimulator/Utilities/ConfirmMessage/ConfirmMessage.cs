using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmMessage : MonoBehaviour {

    public int state = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void changeState(int newState)
    {
        state = newState;
    }

    public void showConfirmMessage(string message, Func<bool> action)
    {
        state = 0;
        IEnumerator coroutine = doActionWithConfirmMessage(action);
        gameObject.SetActive(true);
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        StartCoroutine(coroutine);
        
    }

    public IEnumerator doActionWithConfirmMessage(Func<bool> action)
    {
        yield return new WaitWhile(delegate { return (state == 0) ? true : false; });

        if (state > 0)
            action.Invoke();

        gameObject.SetActive(false);

    }




}
