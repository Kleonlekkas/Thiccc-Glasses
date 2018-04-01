using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> bars = new List<GameObject>();
    public GameObject barPrefab; // set up generation of bars
    public GameObject collectPrefab; // set up generation of bars

    // Use this for initialization
    void Start ()
    {
        //Automatically Generate Bar Prefabs
        for (int i = 0; i < 100; i++)
        {
            GameObject bar = Instantiate(barPrefab, new Vector3(i * 200.0F, 0, 0), Quaternion.identity);
            bar.GetComponent<ANoteScript>().activeKeytwo = "s";
            bar.GetComponent<ANoteScript>().activeKeythree = "d";
            bars.Add(bar);
        }

        //Automatically Generate Collectible Prefabs
        for (int i = 0; i < 100; i++)
        {
            GameObject collectible = Instantiate(collectPrefab, new Vector3(i * 210.0F, 30.0f, 0), Quaternion.identity);
            GameObject collectible1 = Instantiate(collectPrefab, new Vector3(i * 225.0F, 40.0f, 0), Quaternion.identity);
            GameObject collectible2 = Instantiate(collectPrefab, new Vector3((i * 230.0F)+10.0f, 25.0f, 0), Quaternion.identity);
            GameObject collectible3 = Instantiate(collectPrefab, new Vector3((i * 243.0F)-10.0f, 50.0f, 0), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
