using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    private GameObject player;

    private static int score;
    public Text scoreText;

	// Use this for initialization
	void Start ()
    {
        score = 0;

        player = GameObject.FindWithTag("Player");
        scoreText = Text.FindObjectOfType<Text>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if((player.transform.position - this.transform.position).magnitude < 5)
        {
            score += 100;
            scoreText.text = "Score: " + score.ToString();
            Destroy(gameObject);
        }
	}
}
