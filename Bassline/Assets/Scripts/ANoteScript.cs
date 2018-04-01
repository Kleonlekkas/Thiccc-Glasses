using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANoteScript : MonoBehaviour
{
    // player game object
    private GameObject player;

    #region Extend Bars
    public bool isActive; //bool to track if the bar is active for the first type of jump
    public string activeKey; //str containing the key that will expand the bar if active for the first type of jump
    public string activeKeytwo; //str containing the key that will expand the bar if active for the second type of jump
    public string activeKeythree; //str containing the key that will expand the bar if active for the third type of jump
    public float maxScale; //maximum size the bar can scale to
    public float scaleFactor; //how much the bar scales by per second
    #endregion

    #region Collision
    private bool isBarColliding; // check if bar and player are colliding
    private float pRadius;
    private float barHeight;
    private float barWidth;

    private Vector3 barPos;
    private Vector3 pPos;
    #endregion

    #region Audio
    private AudioSource audio;

    // Music Notes
    public AudioClip audioNoteA;
    public AudioClip audioNoteC;
    public AudioClip audioNoteD;
    public AudioClip audioNoteF;
    public AudioClip audioNoteG;

    public AudioClip audioNoteLowA;
    public AudioClip audioNoteLowC;
    public AudioClip audioNoteLowD;
    public AudioClip audioNoteLowF;
    public AudioClip audioNoteLowG;
    #endregion

    // Use this for initialization
    void Start ()
    {
        // initialize audio, don't play on start
		audio = GetComponent<AudioSource>();
        audio.Stop();

        player = GameObject.FindWithTag ("Player");
        
        pRadius = player.transform.localScale.x / 2;
        barWidth = transform.localScale.x;
        barHeight = transform.localScale.y;

        barPos = transform.position;
        pPos = player.transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        extendBar();

        isBarColliding = isColliding();

        if (isBarColliding)
            runCollision();

    }//end update method

    void extendBar() {
        if (isActive) //checks if bar is active
        {
            if (Input.GetKey(activeKey) || Input.GetKey(activeKeytwo) || Input.GetKey(activeKeythree)) //checks if active key is depressed
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
                // Descale the bar while key is not pressed
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
    }

    bool isColliding()
    {
        pPos = player.transform.position;
        //float distance = ( Vector2.Distance(pPos, barPos));

        // collision logic not working
        //if (distance < (pRadius + barHeight/2))
        if ((pPos.x % 200) < 40 || (pPos.x % 200) > 160)
        {
            if (pPos.y < 3.5 && pPos.y > 0) 
            {
                return true;
            }
        }
        return false;
    }

    void runCollision() {

        float force; // scalar force
        Vector2 forceUp = new Vector2(0,0); // vector force

        //audio clip to be played - default low C
        AudioClip clipToPlay = audioNoteLowC;

        // random int for audio clip to play
        int rand = UnityEngine.Random.Range(1, 4);

        if (Input.GetKey(activeKey)) // 'a' key pressed
        {
            //apply up force
            force = 37f;
            forceUp = new Vector2(0, force);
            player.GetComponent<Rigidbody2D>().AddForce(forceUp, ForceMode2D.Impulse);

            //music note logic --- change notes to audioClips in Unity Inspector
            if (rand == 1)
                clipToPlay = audioNoteLowC;
            else if (rand == 2)
                clipToPlay = audioNoteLowD;
            else if (rand == 3)
                clipToPlay = audioNoteLowF;
            else
                clipToPlay = audioNoteLowC;
            audio.clip = clipToPlay;
            audio.Play();
        }
        else if (Input.GetKey(activeKeytwo)) // 's' key pressed
        {
            //apply up force
            force = 40f;
            forceUp = new Vector2(0, force);
            player.GetComponent<Rigidbody2D>().AddForce(forceUp, ForceMode2D.Impulse);

            //music note logic --- change notes to audioClips in Unity Inspector
            if (rand == 1)
                clipToPlay = audioNoteLowG;
            else if (rand == 2)
                clipToPlay = audioNoteLowA;
            else if (rand == 3)
                clipToPlay = audioNoteC;
            else
                clipToPlay = audioNoteLowG;
            audio.clip = clipToPlay;
            audio.Play();
        }
        else if (Input.GetKey(activeKeythree)) // 'd' key pressed
        {
            //apply up force
            force = 43f;
            forceUp = new Vector2(0, force);
            player.GetComponent<Rigidbody2D>().AddForce(forceUp, ForceMode2D.Impulse);

            //music note logic --- change notes to audioClips in Unity Inspector
            if (rand == 1)
                clipToPlay = audioNoteD;
            else if (rand == 2)
                clipToPlay = audioNoteF;
            else if (rand == 3)
                clipToPlay = audioNoteG;
            else
                clipToPlay = audioNoteA;
            audio.clip = clipToPlay;
            audio.Play();
        }
    }

}
