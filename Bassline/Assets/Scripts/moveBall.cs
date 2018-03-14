using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour {

    // LERP
    float t;
    public Vector2 startPosition;
    public Vector2 target;
    public float timeToReachTarget;

    //public float speed;
	
    // Thrust Physics
	public float thrust;
    public Rigidbody2D rb;
	
    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		
        // LERP Init
        startPosition = transform.position;
    }
	
	void FixedUpdate()
    {
        Vector2 myThrust = new Vector2(thrust, 0);

        if (Input.GetKeyDown(KeyCode.T)) { 
            rb.AddForce(myThrust, ForceMode2D.Force);
        }
    }
	
    void Update()
    {
        // LERP Calc
        t += Time.deltaTime / timeToReachTarget;

        // LERP on X Axis
        transform.position = Vector2.Lerp(
            new Vector2(startPosition.x, transform.position.y),
            new Vector2(target.x, transform.position.y), t);

        //transform.Translate(Vector2.right * Time.deltaTime * speed);
    }
}
