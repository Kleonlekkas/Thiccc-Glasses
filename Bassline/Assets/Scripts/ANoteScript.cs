using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour
{
    public bool isActive; //bool to track if the bar is active
    public string activeKey; //str containing the key that will expand the bar if active
    public float maxScale; //maximum size the bar can scale to
    public float scaleFactor; //how much the bar scales by per second

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(isActive) //checks if bar is active
        {
            if (Input.GetKey(activeKey)) //checks if active key is depressed
            {
                //increase scale and transform position to make it appear the bar is rising
                transform.localScale += new Vector3(0, scaleFactor * Time.deltaTime, 0);
                this.transform.position += new Vector3(0, scaleFactor / 2 * Time.deltaTime, 0);

                //don't let it get too large
                if (this.transform.localScale.y > maxScale)
                {
                    this.transform.localScale = new Vector3(1, maxScale, 1);
                }
                //or high
                if (this.transform.position.y > (maxScale - 1) / 2)
                {
                    this.transform.position = new Vector3(this.transform.position.x, (maxScale - 1) / 2, this.transform.position.z);
                }
            }
            else
            {
                transform.localScale += new Vector3(0, -scaleFactor * Time.deltaTime, 0);
                this.transform.position += new Vector3(0, -scaleFactor / 2 * Time.deltaTime, 0);
                if (this.transform.localScale.y < 1)
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                if (this.transform.position.y < 0)
                {
                    this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                }
            }
        }
        else
        {

        }
    }



}
