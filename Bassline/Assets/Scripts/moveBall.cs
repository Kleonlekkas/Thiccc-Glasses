using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour {
    
    // The player rigidbody
    public Rigidbody2D rb;
    
    // speed of song and speed of player
	public float bpm;
    public float ballSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Regulate position over time relative to bpm
        //float moveSpeed = -(ballSpeed - (bpm / 60));

        Vector2 movement = new Vector2(ballSpeed, 0);
        movement = movement + (Vector2)(transform.position);
        transform.position = movement;

        // return player to top of screen if they fall
        if (rb.position.y < -30.0f) {
            transform.position = new Vector2(rb.position.x, 120.0f);
        }
    }
}
