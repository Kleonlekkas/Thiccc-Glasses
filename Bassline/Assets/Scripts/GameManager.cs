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
        for (int i = 1; i < 100; i++)
        {
            GameObject collectible = Instantiate(collectPrefab, new Vector3(i * 25.0F, 10f, 0), Quaternion.identity);
            GameObject collectible1 = Instantiate(collectPrefab, new Vector3(i * 50.0F, 20f, 0), Quaternion.identity);
            GameObject collectible2 = Instantiate(collectPrefab, new Vector3((i * 75.5F), 30f, 0), Quaternion.identity);
            GameObject collectible3 = Instantiate(collectPrefab, new Vector3((i * 125.0F), 40f, 0), Quaternion.identity);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
