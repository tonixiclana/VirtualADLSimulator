using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FilterBySensorMenu : MonoBehaviour {

    public SelectGameobjectCursor gameobjectCursor;

    public Button floorSensor;
    public Button drawerSensor;
    public Button doorSensor;
    public Button presenceSensor;
    public Button windowSensor;
    public Button buttonSensor;

    private string isFiltered = "";

	// Use this for initialization
	void Start () {

        floorSensor.onClick.AddListener(delegate {
            
            if(isFiltered == "Floor")
            {
                gameobjectCursor.highlighingAndBlockByTag("Floor");
                isFiltered = "";
                ColorBlock tmp = floorSensor.colors;
                Color c = tmp.highlightedColor;
                tmp.highlightedColor = floorSensor.colors.normalColor;
                tmp.normalColor = c;
                floorSensor.colors = tmp;
            }
            else
            {
                if (isFiltered == "")
                {
                    gameobjectCursor.highlighingAndBlockByTag("Floor");
                    isFiltered = "Floor";
                    ColorBlock tmp = floorSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = floorSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    floorSensor.colors = tmp;

                }
                else
                {
                    gameobjectCursor.unlockAllLayers();
                    gameobjectCursor.highlighingAndBlockByTag("Floor");
                    isFiltered = "Floor";
                    ColorBlock tmp = floorSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = floorSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    floorSensor.colors = tmp;
                }
                
            }
        });

        drawerSensor.onClick.AddListener(delegate {
            if (isFiltered == "Drawer")
            {
                gameobjectCursor.highlighingAndBlockByTag("Drawer");
                isFiltered = "";
                ColorBlock tmp = drawerSensor.colors;
                Color c = tmp.highlightedColor;
                tmp.highlightedColor = drawerSensor.colors.normalColor;
                tmp.normalColor = c;
                drawerSensor.colors = tmp;
            }
            else
            {
                if (isFiltered == "")
                {
                    gameobjectCursor.highlighingAndBlockByTag("Drawer");
                    isFiltered = "Drawer";
                    ColorBlock tmp = drawerSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = drawerSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    drawerSensor.colors = tmp;

                }
                else
                {
                    gameobjectCursor.unlockAllLayers();
                    gameobjectCursor.highlighingAndBlockByTag("Drawer");
                    isFiltered = "Drawer";
                    ColorBlock tmp = drawerSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = drawerSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    drawerSensor.colors = tmp;
                }

            }

        });

        doorSensor.onClick.AddListener(delegate {
            if (isFiltered == "Door")
            {
                gameobjectCursor.highlighingAndBlockByTag("Door");
                isFiltered = "";
                ColorBlock tmp = doorSensor.colors;
                Color c = tmp.highlightedColor;
                tmp.highlightedColor = doorSensor.colors.normalColor;
                tmp.normalColor = c;
                doorSensor.colors = tmp;
            }
            else
            {
                if (isFiltered == "")
                {
                    gameobjectCursor.highlighingAndBlockByTag("Door");
                    isFiltered = "Door";
                    ColorBlock tmp = doorSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = doorSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    doorSensor.colors = tmp;
                }
                else
                {
                    gameobjectCursor.unlockAllLayers();
                    gameobjectCursor.highlighingAndBlockByTag("Door");
                    isFiltered = "Door";
                    ColorBlock tmp = doorSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = doorSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    doorSensor.colors = tmp;
                }

            }

        });

        windowSensor.onClick.AddListener(delegate {
            if (isFiltered == "Window")
            {
                gameobjectCursor.highlighingAndBlockByTag("Window");
                isFiltered = "";
                ColorBlock tmp = windowSensor.colors;
                Color c = tmp.highlightedColor;
                tmp.highlightedColor = windowSensor.colors.normalColor;
                tmp.normalColor = c;
                doorSensor.colors = tmp;
            }
            else
            {
                if (isFiltered == "")
                {
                    gameobjectCursor.highlighingAndBlockByTag("Window");
                    isFiltered = "Window";
                    ColorBlock tmp = windowSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = windowSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    windowSensor.colors = tmp;
                }
                else
                {
                    gameobjectCursor.unlockAllLayers();
                    gameobjectCursor.highlighingAndBlockByTag("Window");
                    isFiltered = "Window";
                    ColorBlock tmp = windowSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = windowSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    windowSensor.colors = tmp;
                }

            }

        });

        buttonSensor.onClick.AddListener(delegate {

            if (isFiltered == "Button")
            {
                gameobjectCursor.highlighingAndBlockByTag("HomeAppliances");
                isFiltered = "";
                ColorBlock tmp = buttonSensor.colors;
                Color c = tmp.highlightedColor;
                tmp.highlightedColor = buttonSensor.colors.normalColor;
                tmp.normalColor = c;
                buttonSensor.colors = tmp;
            }
            else
            {
                if (isFiltered == "")
                {
                    gameobjectCursor.highlighingAndBlockByTag("HomeAppliances");
                    isFiltered = "Button";
                    ColorBlock tmp = buttonSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = buttonSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    buttonSensor.colors = tmp;

                }
                else
                {
                    gameobjectCursor.unlockAllLayers();
                    gameobjectCursor.highlighingAndBlockByTag("HomeAppliances");
                    isFiltered = "Button";
                    ColorBlock tmp = buttonSensor.colors;
                    Color c = tmp.normalColor;
                    tmp.normalColor = buttonSensor.colors.highlightedColor;
                    tmp.highlightedColor = c;
                    buttonSensor.colors = tmp;
                }

            }
        });

        /* presenceSensor.onClick.AddListener(delegate {
             gameobjectCursor.highlighingAndBlockByTag("");
         });

         floorSensor.onClick.AddListener(delegate {
             gameobjectCursor.highlighingAndBlockByTag("Floor");
         });

         floorSensor.onClick.AddListener(delegate {
             gameobjectCursor.highlighingAndBlockByTag("Floor");
         });*/

    }
	
	// Update is called once per frame
	void Update () {
		
	}



}
