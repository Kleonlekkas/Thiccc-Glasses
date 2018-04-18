using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlayer : MonoBehaviour {

    public Camera m_Camera;

    public Transform vertWall;
    public Transform horzWall;
    public Transform floor;

    public GameObject playerObj;

    //Map to hold coordinates of our objects
    private int[,] map;
    //Scale that objects will be moved by.
    private float scale;


    // Use this for initialization
    void Start ()
    {

        //Initialize map. this will be streamed in in the future
        //1 is player, 2 is durian, 3 is movable object, 4 is immovable object, 5 is goal
        map = new int[,]{
            { 0, 0, 0, 0, 5 },
            { 0, 0, 2, 0, 0 },
            { 0, 0, 0, 2, 0 },
            { 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0 }
        };

        scale = 3.0f;

        //Set its position to be the center of the grid we made
        m_Camera.transform.position = new Vector3(scale * (map.GetLength(0) / 2), 15.0f, scale * (map.GetLength(0) / 2));

        //Get length of first dimension
        for (int i = 0; i < map.GetLength(0); i++)
        {
            //Get length of second dimension
            for (int n = 0; n < map.GetLength(1); n++)
            {
                //Draw in the walls
                Instantiate(vertWall, new Vector3((i * scale) - scale/2, 0, scale * (map.GetLength(1) / 2)), vertWall.transform.rotation);
                Instantiate(horzWall, new Vector3(scale * (map.GetLength(0) /2), 0, (n * scale) - scale / 2), horzWall.transform.rotation);
                //Draw in an extra wall for each edge, and draw our floor
                if (i == 4 && n == 4)
                {
                    Instantiate(vertWall, new Vector3(((i + 1) * scale) - scale / 2, 0, scale * (map.GetLength(0) / 2)), vertWall.transform.rotation);
                    Instantiate(horzWall, new Vector3(scale * (map.GetLength(0) / 2), 0, ((n + 1) * scale) - scale / 2), horzWall.transform.rotation);

                    Transform myFloor = Instantiate(floor, new Vector3(scale * (map.GetLength(0) / 2), 0.0f, scale * (map.GetLength(1) / 2)), floor.transform.rotation);
                    myFloor.localScale = new Vector3(scale * (map.GetLength(0)), 0.1f, scale * (map.GetLength(1)));
                }
                //Place player
                if (map[i, n] == 1)
                {
                    //Origin is our bottom left tile
                    playerObj.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
                }
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.W)) {
            playerMoveAttempt("Up");
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            playerMoveAttempt("Left");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            playerMoveAttempt("Down");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            playerMoveAttempt("Right");
        }

        /*
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
                    playerObj.transform.position.z + scale);
            }
        }
        if (!isLeftBlocked) { 
            if (Input.GetKeyDown(KeyCode.A))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x - scale,
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
                    playerObj.transform.position.z - scale);
            }
        }
        if (!isRightBlocked)
        {
            if (Input.GetKeyDown(KeyCode.D))
            {
                playerObj.transform.position = new Vector3(
                    playerObj.transform.position.x + scale,
                    playerObj.transform.position.y,
                    playerObj.transform.position.z);
            }
        }
        */
    }//end update

    void playerMoveAttempt(string direction) { 

        //The player wants to move up
        if (direction == "Up")
        {
            moveObjectUp(playerObj);
        }

        //The player wants to move Down
        if (direction == "Down")
        {
            moveObjectDown(playerObj);
        }

        //The player wants to move Left
        if (direction == "Left")
        {
            moveObjectLeft(playerObj);
        }

        //The player wants to move Right
        if (direction == "Right")
        {
            moveObjectRight(playerObj);
        }

        //Get length of first dimension
        for (int i = 0; i < map.GetLength(0); i++)
        {
            //Get length of second dimension
            for (int n = 0; n < map.GetLength(1); n++)
            {

            }
        }

    }

    //Functions for moving objects
    void moveObjectUp(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x,
             obj.transform.position.y,
             obj.transform.position.z + scale);
    }
    void moveObjectDown(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x,
             obj.transform.position.y,
             obj.transform.position.z - scale);
    }
    void moveObjectLeft(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x - scale,
             obj.transform.position.y,
             obj.transform.position.z);
    }
    void moveObjectRight(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x + scale,
             obj.transform.position.y,
             obj.transform.position.z);
    }



}


