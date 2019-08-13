using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortCutKeysEditMode : MonoBehaviour {

    //public KeyCode openRegistryActivityMenu = KeyCode.R;
    //public GameObject registryActivityMenu;
    public KeyCode changeCameraMode = KeyCode.Q;
    public GameObject editModeCamera;
    public GameObject firtsPersonModeCamera;
    public GameObject character;
    public GameObject editModeInGameMenuOption;
    public MeshRenderer ceiling;
    public GameObject editSensorlayout;

    
    public void changeToExperimentMode()
    {
        foreach (var s in FindObjectsOfType<SelectGameobjectCursor>())
            s.clearSelection();



        editModeCamera.SetActive(false);
        //experimentModeInGameMenuOption.SetActive(false);
        editModeInGameMenuOption.SetActive(true);
        firtsPersonModeCamera.SetActive(true);
        ceiling.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        character.GetComponent<FirstPersonCamera>().enabled = true;
 
        character.GetComponent<PlayerController>().enabled = true;

        editSensorlayout.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void changeToEditMode()
    {
        firtsPersonModeCamera.SetActive(false);
        character.GetComponent<FirstPersonCamera>().enabled = false;
        character.GetComponent<PlayerController>().currentSpeed = 0;
        character.GetComponent<PlayerController>().enabled = false;
        ceiling.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        foreach (var s in FindObjectsOfType<SelectGameobjectCursor>())
            s.clearSelection();
        editModeCamera.SetActive(true);
        editModeInGameMenuOption.SetActive(false);

        editSensorlayout.SetActive(true);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        /*if (Input.GetKeyDown(openRegistryActivityMenu))
        {
            registryActivityMenu.SetActive(!registryActivityMenu.activeSelf);

            if (registryActivityMenu.activeSelf)
            {
                foreach (var s in FindObjectsOfType<SelectGameobjectCursor>())
                    s.clearSelection();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }*/
        
        if (Input.GetKeyDown(changeCameraMode))
        {
            if (editModeCamera.activeSelf)
                changeToExperimentMode();
            else
                changeToEditMode();
        }


        //Cursor.visible = m_MenuToggle.isOn;//force the cursor visible if anythign had hidden it

    }
}
