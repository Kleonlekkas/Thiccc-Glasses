using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

    public GameObject playerObj;

    private bool isLeftBlocked;
    private bool isRightBlocked;
    private bool isTopBlocked;
    private bool isBottomBlocked;


    // Use this for initialization
    void Start ()
    {
        isLeftBlocked = false;
        isRightBlocked = false;
        isTopBlocked = false;
        isBottomBlocked = false;
    }

    // Update is called once per frame
    void Update() {

        if (playerObj.transform.position.x < -12.0f)
        {
            isLeftBlocked = true;
        }
        else
        {
            isLeftBlocked = false;
        }

        if (playerObj.transform.position.x > 12.0f)
        {
            isRightBlocked = true;
        }
        else
        {
            isRightBlocked = false;
        }

        if (playerObj.transform.position.z < -5.0f)
        {
            isBottomBlocked = true;
        }
        else
        {
            isBottomBlocked = false;
        }

        if (playerObj.transform.position.z > 5.0f)
        {
            isTopBlocked = true;
        }
        else
        {
            isTopBlocked = false;
        }

        if (!isTopBlocked)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x,
                    playerObj.transform.position.y,
                    playerObj.transform.position.z + 2.0f);
            }
        }
        if (!isLeftBlocked) { 
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x - 3.0f,
                    playerObj.transform.position.y,
                    playerObj.transform.position.z);
            }
        }
        if (!isBottomBlocked)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x,
                    playerObj.transform.position.y,
                    playerObj.transform.position.z - 2.0f);
            }
        }
        if (!isRightBlocked)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x + 3.0f,
                    playerObj.transform.position.y,
                    playerObj.transform.position.z);
            }
        }
    }
}
