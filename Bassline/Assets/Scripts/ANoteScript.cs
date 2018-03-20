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

    System.Random rand;

    //Collision
    private float pRadius;
    private float barHeight;
    private float barWidth;

    private Vector3 barPos;
    private Vector3 pPos;

	// Use this for initialization
	void Start ()
    {
		audio = GetComponent<AudioSource>();
		player = GameObject.FindWithTag ("Player");

        pRadius = player.transform.localScale.x / 2;
        barWidth = this.transform.localScale.x;
        barHeight = this.transform.localScale.y;

        barPos = this.transform.position;
        pPos = player.transform.position;

        rand = new System.Random();

        //maxScale = rand.Next(1, 6);
        //transform.localScale = new Vector3(0, rand.Next(1, 3), 0);
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
                transform.position += new Vector3(0, scaleFactor / 2 * Time.deltaTime, 0);

                barHeight += scaleFactor * Time.deltaTime;

                //don't let it get too large
                if (transform.localScale.y > maxScale)
                {
                    transform.localScale = new Vector3(transform.localScale.x, maxScale, 1);
                    //adjust bar height collision
                    barHeight = maxScale;
                }
                //or high
                if (transform.position.y > (maxScale - 1) / 2)
                {
                    transform.position = new Vector3(transform.position.x, (maxScale - 1) / 2, transform.position.z);
                }
            }
            else
            {
                transform.localScale += new Vector3(0, -scaleFactor * Time.deltaTime, 0);
                transform.position += new Vector3(0, -scaleFactor / 2 * Time.deltaTime, 0);

                barHeight -= scaleFactor * Time.deltaTime;

                if (transform.localScale.y < 1)
                {
                    transform.localScale = new Vector3(transform.localScale.x, 1, 1);
                    barHeight = 1;
                }
                if (transform.position.y < 0)
                {
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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

        ourCollision();

    }//end update method


	//if the ball comes into contact with the bar, send it up
	/* void OnCollisionEnter2D(Collision2D c) {

		//dampen it a bit
		float force = scaleFactor * 0.2f;

		//If we hit the player
		if (c.gameObject.tag == "Player") {
            if (Input.GetKey(activeKey)) {
				
				//if Circlular
				Vector2 dir = c.contacts[0].point - transform.position;
				//we then get the opposite (-Vector2) and normalize it
				dir = -dir.normalized;
				

				//then we add the force in the dir * the force
				//but for now, we'll send it up
				Vector2 bigUps = new Vector2(0, force);

				c.gameObject.GetComponent<Rigidbody2D> ().AddForce (bigUps, ForceMode2D.Impulse);

                print("we in it boys");
            }

		}
    }//end collision method
*/


    bool ourCollision()
    {
        pPos = player.transform.position;
        float distance = ( Vector3.Distance(pPos, barPos));

        if (distance < (pRadius + barHeight/2))
        {

            //dampen it a bit
            float force = scaleFactor * 0.2f;


                if (Input.GetKey(activeKey))
                {
                    /*
                    //if Circlular
                    Vector2 dir = c.contacts[0].point - transform.position;
                    //we then get the opposite (-Vector2) and normalize it
                    dir = -dir.normalized;
                    */

                    //then we add the force in the dir * the force
                    //but for now, we'll send it up
                    Vector2 bigUps = new Vector2(0, force);

                    player.GetComponent<Rigidbody2D>().AddForce(bigUps, ForceMode2D.Impulse);

                    print("we in it boys");
                }

   




            print("NOW WE ACTUALLY IN IT");
        }



        return true;
    }


}
