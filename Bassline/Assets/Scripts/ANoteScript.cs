using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour {
    public GameObject myPrefab;
    public float maxScale;
    public float scaleFactor;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {

        if (Input.GetKey("space"))
        {
            transform.localScale += new Vector3(0, scaleFactor * Time.deltaTime, 0);
            this.transform.position += new Vector3(0, scaleFactor / 2 * Time.deltaTime, 0);
            if (this.transform.localScale.y > maxScale)
            {
                this.transform.localScale = new Vector3(1, maxScale, 1);
            }
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



}
