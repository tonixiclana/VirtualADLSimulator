using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{
    public GameObject prefab; // This is our prefab object that will be exposed in the inspector
    public List<GameObject> elements = new List<GameObject>();
    public int numberToCreate; // number of objects to create. Exposed in inspector

    void Start()
    {
        //Populate();
        //elements.Add((GameObject)Instantiate(pr, transform));
    }

    void Update()
    {

    }

    public void addElement(GameObject gameObject)
    {
        elements.Add((GameObject)Instantiate(gameObject, transform));
    }

    public void removeAllElements()
    {
        foreach(GameObject g in elements)
        {
            Destroy(g);
        }
    }

    void Populate()
    {
        GameObject newObj; // Create GameObject instance

        for (int i = 0; i < numberToCreate; i++)
        {
            // Create new instances of our prefab until we've created as many as we specified
            newObj = (GameObject)Instantiate(prefab, transform);

            // Randomize the color of our image
            //newObj.GetComponent.color = Random.ColorHSV();
        }

    }
}
