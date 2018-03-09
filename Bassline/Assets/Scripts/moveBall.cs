using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveBall : MonoBehaviour {

    public float thrust;
    public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}

    void FixedUpdate()
    {
        Vector2 myThrust = new Vector2(thrust, 0);

        if (Input.GetKeyDown(KeyCode.T)) { 
            rb.AddForce(myThrust, ForceMode2D.Force);
        }
    }

    // Update is called once per frame
    void Update () {
        //transform.Translate(new Vector2(0.1f,0.0f));
	}
}
