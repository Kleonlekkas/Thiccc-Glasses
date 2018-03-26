using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour
{
    public bool isActive; //bool to track if the bar is active for the first type of jump
    public string activeKey; //str containing the key that will expand the bar if active for the first type of jump
    public string activeKeytwo; //str containing the key that will expand the bar if active for the second type of jump
    public string activeKeythree; //str containing the key that will expand the bar if active for the third type of jump
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
        barWidth = transform.localScale.x;
        barHeight = transform.localScale.y;

        barPos = transform.position;
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
            else if (Input.GetKey(activeKeytwo)) //checks if active key is depressed
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
            else if (Input.GetKey(activeKeythree)) //checks if active key is depressed
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


    void ourCollision()
    {
        pPos = player.transform.position;
        float distance = ( Vector2.Distance(pPos, barPos));

        // collision logic not working
        //if (distance < (pRadius + barHeight/2))
        if((pPos.x % 200) < 40 || (pPos.x % 200) > 160)
        {
            if (pPos.y < 5 && pPos.y > 0) // proof of concept, dirty code
            {

                //dampen it a bit
                float force = scaleFactor * 0.7f;
                float forcetwo = scaleFactor * 0.8f;
                float forcethree = scaleFactor * 0.9f;

                if (Input.GetKey(activeKey))
                {
                    //apply up force
                    Vector2 bigUps = new Vector2(0, force);
                    player.GetComponent<Rigidbody2D>().AddForce(bigUps, ForceMode2D.Impulse);
                }
                else if (Input.GetKey(activeKeytwo))
                {
                    //apply up force
                    Vector2 bigUps = new Vector2(0, forcetwo);
                    player.GetComponent<Rigidbody2D>().AddForce(bigUps, ForceMode2D.Impulse);
                }
                else if (Input.GetKey(activeKeythree))
                {
                    //apply up force
                    Vector2 bigUpstwo = new Vector2(0, forcethree);
                    player.GetComponent<Rigidbody2D>().AddForce(bigUpstwo, ForceMode2D.Impulse);
                }
                // test if condition entry
                print("NOW WE ACTUALLY IN IT");
            }
        }
    }


}
