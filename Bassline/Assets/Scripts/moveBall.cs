using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour {
    
    private bool flag; //temp flag for delaying start of level until button press
    
    // The player rigidbody
    public Rigidbody2D rb;

    //public float thrust;

    // speed of song and speed of player
	public float bpm;
    public float ballSpeed;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();

        ballSpeed = 1;

        flag = false; //temp flag for delaying start of level until button press
    }

    void Update()
    {
        if(!flag && Input.GetKeyDown("space"))
        {
            flag = true;
        }

        //if(flag) //only activate thrusters if we're live 
        //{
            // Regulate position over time relative to bpm
            float moveSpeed = -(ballSpeed - (bpm / 60));

            Vector2 movement = new Vector2(moveSpeed, 0);
            movement = movement + (Vector2)(transform.position);
            transform.position = movement;
        //}
    }
}
