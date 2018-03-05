using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour {
    public GameObject myPrefab;
    public int size;
    public List<GameObject> arrayOfCubes;
    int y;
    bool beenPressed;

	// Use this for initialization
	void Start () {
        y = 10;
        beenPressed = false;

        arrayOfCubes = new List<GameObject>();

        spawnCopy();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("space"))
        {
            if (!beenPressed)
            {
                //transform.localScale += new Vector3(0, 1.0f, 0);
               // this.transform.position += new Vector3(0, 1.0f, 0);
                beenPressed = true;
                moveCubes(1);
            }

        }
        if (Input.GetKeyUp("space"))
        {
           // transform.localScale += new Vector3(0, -1.0f, 0);
           // this.transform.position += new Vector3(0, -1.0f, 0);
            moveCubes(-1);
            beenPressed = false;
        } 


    }

    void spawnCopy()
    {
        //GameObject newCube = (GameObject)Instantiate(cube, new Vector3(0, 5.0f, 0), transform.rotation);
        //Instantiate(newCube, new Vector3(0, 5.0f, 0));

        for (int i = 0; i < size; i++)
        {
            Debug.Log("cube added");
            //arrayOfCubes.Add((GameObject)Instantiate(cube, new Vector3(0, 0.0f, 0), transform.rotation));
            //arrayOfCubes.Add(Instantiate(myPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity));

            //Instantiate(myPrefab, new Vector3(0.0f * i, 0.0f, 0.0f), Quaternion.identity);

            //dis part dun work
            //GameObject myObj = Instantiate(myPrefab, new Vector3(0.0f * i, 0.0f, 0.0f), Quaternion.identity);
        }
    }

    void moveCubes(int sign)
    {
        for (int i = 0; i < size; i++)
        {
            arrayOfCubes[i].transform.position += new Vector3(0, sign  * 1.25f, 0);
        }
    }



}
