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
	private AudioSource audio; // our note from editor
	private GameObject player; // reference to our player

	// Use this for initialization
	void Start ()
    {
		audio = GetComponent<AudioSource>();
		player = GameObject.FindWithTag ("Player");
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

		//only once while its pressed, otherwise the player will hurt their ears like this programmer did
		if (Input.GetKeyDown(activeKey)) {
			//play the note the first time the user presses 
			audio.Play ();
		} else {
			//Sounds better if it doesn't stop. whole note plays, and its a short audio clip anyway
			//audio.Stop ();
		}
    }//end update method


	//if the ball comes into contact with the bar, send it up
	void OnCollisionEnter2D(Collision2D c) {

		//dampen it a bit
		float force = scaleFactor * 0.2f;
		print ("we in it boys");

		//If we hit the player
		if (c.gameObject.tag == "Player") {
			if (isActive) {
				/*
				//if Circlular
				Vector2 dir = c.contacts[0].point - transform.position;
				//we then get the opposite (-Vector2) and normalize it
				dir = -dir.normalized;
				*/

				//then we add the force in the dir * the force
				//but for now, we'll send it up
				Vector2 bigUps = new Vector2(0, force);

				player.GetComponent<Rigidbody2D> ().AddForce (bigUps, ForceMode2D.Impulse);

				print ("we in it boys");
			}

		}//end collision method
	}


}
