using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<GameObject> bars = new List<GameObject>();
    public GameObject barPrefab; // set up generation of bars

    // Use this for initialization
    void Start ()
    {
        //Automatically Generate Bar Prefabs
        for (int i = 1; i < 50; i++)
        {
            GameObject bar = Instantiate(barPrefab, new Vector3(i * 200.0F, 0, 0), Quaternion.identity);
            bar.GetComponent<ANoteScript>().activeKeytwo = "s";
            bar.GetComponent<ANoteScript>().activeKeythree = "d";
            bars.Add(bar);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
