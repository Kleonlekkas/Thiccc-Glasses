using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour {
    public GameObject cube;
    public int size;
    public List<GameObject> arrayOfCubes;
    int y;
    bool beenPressed;

	// Use this for initialization
	void Start () {
        y = 10;
        beenPressed = false;
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
                cube.transform.position+=new Vector3(1.0f,1.0f,0.0f);
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

        for (int i = 0; i < size;i++)
        {
            arrayOfCubes.Add((GameObject)Instantiate(cube, new Vector3(0, 0.0f, 0), transform.rotation));
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
