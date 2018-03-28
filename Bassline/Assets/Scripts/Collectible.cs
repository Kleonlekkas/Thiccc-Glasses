using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private GameObject player;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if((player.transform.position - this.transform.position).magnitude < 5)
        {
            Destroy(gameObject);
        }
	}
}
