using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmMessage : MonoBehaviour {

    public int state = 0;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject okButton;

    private int inUse = 0;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
    }

    public void changeState(int newState)
    {
        state = newState;
    }

    public void showConfirmMessage(string message, Func<bool> action)
    {
        IEnumerator coroutine = doActionWithConfirmMessage(message, true, action);

        try
        {
            gameObject.SetActive(true);
            StartCoroutine(coroutine);
        }
        catch (Exception e)
        {
            throw e;
        }

    }

    public void showMessage(string message)
    {
        IEnumerator coroutine = doActionWithConfirmMessage(message);

        try
        {
            gameObject.SetActive(true);
            StartCoroutine(coroutine);
        }
        catch (Exception e)
        {
            throw e;
        }
    }

    public IEnumerator doActionWithConfirmMessage(string message, bool isQuestion = false, Func<bool> action = null)
    {
        inUse++;
        yield return new WaitWhile(delegate { return (inUse > 1) ? true : false; });
        if (isQuestion)
        {
            yesButton.SetActive(true);
            noButton.SetActive(true);
        }
        else
            okButton.SetActive(true);

        state = 0;
        gameObject.SetActive(true);
        GetComponentInChildren<TextMeshProUGUI>().text = message;
        yield return new WaitWhile(delegate { return (state == 0) ? true : false; });

        try
        {
            if (state > 0 && action != null)
                action.Invoke();
        }
        catch (Exception e)
        {
            throw e;
        }
        inUse--;

        if (isQuestion)
        {
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }
        else
            okButton.SetActive(false);

        if (inUse == 0)
            gameObject.SetActive(false);
    }




}
