using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class movePlayer : MonoBehaviour {

	public Camera m_Camera;

	//Class to hold our objects
	public class MyObject{
		//reference to the game object were holding
		public GameObject theObject;
		//Reference to our horizontal and vertical position in the map
		public int horzInd;
		public int vertInd;

		public MyObject(GameObject p_obj, int p_horz, int p_vert) {
			theObject = p_obj;
			horzInd = p_horz;
			vertInd = p_vert;
		}
	}
    
	//Floor and walls
    public Transform vertWall;
    public Transform horzWall;
    public Transform floor;

	//our custom class to hold it and its indices
	private MyObject myPlayerObj;
    private MyObject endObject;

    //List of all our enemies
    List<MyObject> enemies;

    //List of all our milk
    List<MyObject> milkCrates;

    public GameObject milkCrateObj;
    public GameObject orangeCrateObj;
    public GameObject enemy;
    public GameObject end;

    //Scale that objects will be moved by.
    private float scale;
    private float scaleBack;
    private float camDistance;
    //Map to hold coordinates of our objects
    private int[,] map;
    //creates a string to read level design file
    public string sLevels;

    // Use this for initialization
    void Start ()
    {
        //initialize lists (will have one for each type of object that
        //has multiples. then we'll put those lists into one SUPER LIST
        enemies = new List<MyObject>();
        milkCrates = new List<MyObject>();
        
        //array to make current map
        map = new int[,] { };

        //get levels from file
        sLevels = System.IO.File.ReadAllText("Assets/Script/test.txt");
        string[] levels = sLevels.Split('_');
        
        if (SceneManager.GetActiveScene().name == "first_puzzle")
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
            scaleBack = 0.0f;
            camDistance = (scale * (5 + ((map.GetLength(0) % 5))));
        }
        if (SceneManager.GetActiveScene().name == "second_puzzle")
        {
            map = new int[,]{
            { 0, 0, 2, 0, 0, 5 },
            { 0, 0, 0, 0, 0, 4 },
            { 0, 0, 0, 0, 0, 0 },
            { 4, 0, 0, 0, 0, 2 },
			{ 0, 0, 0, 0, 0, 0 },
            { 1, 0, 0, 4, 2, 0 }
            };
            scale = 3.0f;
            scaleBack = 1.5f;
            camDistance = (scale * 7);
        }
        if (SceneManager.GetActiveScene().name == "third_puzzle")
        {
            map = new int[,]{
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 5 },
            { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 2, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 2, 0, 2, 0, 4, 0, 0, 0 },
            { 2, 0, 0, 3, 0, 0, 0, 0, 0, 0 },
            { 4, 3, 0, 0, 3, 0, 0, 0, 2, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 },
            { 0, 4, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 1, 0, 4, 2, 0, 0, 0, 0, 0, 0 }
            };
            scale = 3.0f;
            scaleBack = 1.5f;
            camDistance = (scale * 10);
        }

        //scale = 3.0f;

        //Set its position to be the center of the grid we made
        m_Camera.transform.position = new Vector3(scale * (map.GetLength(0) / 2) - scaleBack, camDistance, scale * (map.GetLength(0) / 2) - scaleBack);

        vertWall.localScale = new Vector3(0.1f, 0.1f, scale * (map.GetLength(0)));
        horzWall.localScale = new Vector3(scale * (map.GetLength(1)), 0.1f, 0.1f);

        //Draw in the walls
        for (int i = 0; i < map.GetLength(0); i++)
        {
            Transform tempWall = Instantiate(vertWall, new Vector3((i * scale) - scale / 2, 0, scale * (map.GetLength(1) / 2) - scaleBack), vertWall.transform.rotation);
			//tempWall.localScale = newVector3 ();
        }
        for (int n = 0; n < map.GetLength(1); n++)
        {
            Instantiate(horzWall, new Vector3(scale * (map.GetLength(0) / 2) - scaleBack, 0, (n * scale) - scale / 2), horzWall.transform.rotation);
        }

            Instantiate(vertWall, new Vector3(((map.GetLength(0)) * scale) - scale / 2, 0, scale * (map.GetLength(0) / 2) - scaleBack), vertWall.transform.rotation);
            Instantiate(horzWall, new Vector3(scale * (map.GetLength(1) / 2) - scaleBack, 0, ((map.GetLength(1)) * scale) - scale / 2), horzWall.transform.rotation);

            Transform myFloor = Instantiate(floor, new Vector3(scale * (map.GetLength(0) / 2) - scaleBack, 0.0f, scale * (map.GetLength(1) / 2) - scaleBack), floor.transform.rotation);
		    myFloor.localScale = new Vector3 ((scale * (map.GetLength (0)) + scale * (map.GetLength(0) % 5)), 0.1f, (scale * (map.GetLength (1))) + scale * (map.GetLength(0) % 5));
            myFloor.position = new Vector3(myFloor.position.x, myFloor.position.y - 0.25f, myFloor.position.z);
 

        //Get length of first dimension
        for (int i = 0; i < map.GetLength(0); i++)
        {

            //Get length of second dimension
            for (int n = 0; n < map.GetLength(1); n++)
            {
                //Place player
                if (map[i, n] == 1)
                {
					myPlayerObj = new MyObject(GameObject.FindGameObjectWithTag("Player"), n, i);
					myPlayerObj.theObject.transform.position = new Vector3(i * scale, 0.0f, n * scale);
                }

                //Place enemies
                if (map[i, n] == 2)
                    enemies.Add(new MyObject(Instantiate(enemy, new Vector3(i * scale, 0.0f, n * scale), enemy.transform.rotation), n, i));
                 
                //Place milk crates
                if (map[i, n] == 3)
                    milkCrates.Add(new MyObject(Instantiate(milkCrateObj, new Vector3(i * scale, 0.0f, n * scale), milkCrateObj.transform.rotation), n, i));
                 
                //Place orange crates
                if (map[i, n] == 4)
                    Instantiate(orangeCrateObj, new Vector3(i * scale, 0.0f, n * scale), orangeCrateObj.transform.rotation);

                //Place End
                if (map[i, n] == 5)
                    endObject = new MyObject(Instantiate(end, new Vector3(i * scale, 0.0f, n * scale), end.transform.rotation), n, i);
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.W))
        {
            myPlayerObj.theObject.transform.rotation = Quaternion.Euler(0, 270, 0);
            playerMoveAttempt("Up");
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            myPlayerObj.theObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            playerMoveAttempt("Left");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            myPlayerObj.theObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            playerMoveAttempt("Down");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            myPlayerObj.theObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            playerMoveAttempt("Right");
        }

    }//end update

    void playerMoveAttempt(string direction) {

        bool topBlocked = false;
        bool bottomBlocked = false;
        bool leftBlocked = false;
        bool rightBlocked = false;

        //Get length of first dimension
        for (int i = 0; i < map.GetLength(0); i++)
        {
            //Get length of second dimension
            for (int n = 0; n < map.GetLength(1); n++)
            {
                //I is vertical,
                //N is horizontally across
                //If its our player
                if (map[i, n] == 1)
                {
                    //Left and Right side
                    if (n == 0)
                    {
                        leftBlocked = true;
                    }
                    else if (n == (map.GetLength(0) - 1))
                    {
                        rightBlocked = true;
                    }
                    //Top and Bottom
                    if (i == 0)
                    {
                        topBlocked = true;
                    }
                    else if (i == (map.GetLength(1) - 1))
                    {
                        bottomBlocked = true;
                    }

                    //ORANGE CRATES
                    //boundary check sides
                    //If were all the way on the left, dont check for a block there, etc
                    if (n > 0)
                    {
                        //orange crate to our left
                        if (map[i, n - 1] == 4)
                        {
                            leftBlocked = true;
                        }

                        //If its a milk crate
                        if (map[i, n - 1] == 3)
                        {
                            if (myPlayerObj.horzInd == 1)
                            {
                                leftBlocked = true;
                            }
                            //check to see if we can check its left
                            if (n > 1)
                            {
                                if (map[i, n - 2] != 0)
                                {
                                    leftBlocked = true;
                                }
                            }
                        }


                    }
                    if (n < map.GetLength(0) - 1)
                    {
                        //orange 
                        if (map[i, n + 1] == 4) 
                        {
                            rightBlocked = true;

                        }
                        //If its a milk crate - CHECKING RIGHT SIDE
                        if (map[i, n + 1] == 3)
                        {
                            if (myPlayerObj.horzInd == map.GetLength(1)-2)
                            {
                                rightBlocked = true;  
                            }
                            //check to see if we can check its right
                            if (n < map.GetLength(0) - 2)
                            {
                                if (map[i, n + 2] != 0)
                                {
                                    rightBlocked = true;
                                }
                            }
                        }
                    }
                    if (i > 0)
                    {
                        if (map[i - 1, n] == 4)
                        {
                            topBlocked = true;
                        }
                        //If its a milk crate
                        if (map[i-1, n] == 3)
                        {
                            if (myPlayerObj.vertInd == 1)
                            {
                                topBlocked = true;
                            }
                            //check to see if we can check its left
                            if (i > 1)
                            {
                                if (map[i - 2, n] != 0)
                                {
                                    topBlocked = true;
                                }
                            }
                        }
                    }
                    if (i < map.GetLength(1) - 1)
                    {
                        if (map[i+1, n] == 4)
                        {
                            bottomBlocked = true;

                        }
                        //If its a milk crate
                        if (map[i+1, n] == 3)
                        {
                            if (myPlayerObj.vertInd == map.GetLength(1) - 2)
                            {
                                bottomBlocked = true;
                            }
                            //check to see if we can check its left
                            if (i < map.GetLength(0) - 2)
                            {
                                if (map[i + 2, n] != 0)
                                {
                                    bottomBlocked = true;
                                }
                            }
                        }
                    }
                }
            }
        }

                //The player wants to move up
                if (direction == "Up" && !topBlocked)
                {
                    for (int i = 0; i < milkCrates.Count; i++){
                        if (milkCrates[i].horzInd == myPlayerObj.horzInd){
                            if (milkCrates[i].vertInd == myPlayerObj.vertInd-1){
                                moveObjectUp(milkCrates[i].theObject);
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 0;
                                milkCrates[i].vertInd--;
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 3;
                            }
                        }
                    }
                    moveObjectUp(myPlayerObj.theObject);
                    myPlayerObj.vertInd--;
                }
                //The player wants to move Down
                if (direction == "Down" && !bottomBlocked)
                {
                    for (int i = 0; i < milkCrates.Count; i++){
                        if (milkCrates[i].horzInd == myPlayerObj.horzInd){
                            if (milkCrates[i].vertInd == myPlayerObj.vertInd + 1){
                                moveObjectDown(milkCrates[i].theObject);
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 0;
                                milkCrates[i].vertInd++;
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 3;
                            }
                        }
                    }
                    moveObjectDown(myPlayerObj.theObject);
                    myPlayerObj.vertInd++;
                }

                //The player wants to move Left
                if (direction == "Left" && !leftBlocked)
                {
                    for (int i = 0; i < milkCrates.Count; i++) {
                        if (milkCrates[i].horzInd == myPlayerObj.horzInd - 1) {
                            if (milkCrates[i].vertInd == myPlayerObj.vertInd) {
                                moveObjectLeft(milkCrates[i].theObject);
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 0;
                                milkCrates[i].horzInd--;
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 3;
                             }
                        }
                     }
                    moveObjectLeft(myPlayerObj.theObject);
                    myPlayerObj.horzInd--;
                }

                //The player wants to move Right
                if (direction == "Right" && !rightBlocked)
                {
                    for (int i = 0; i < milkCrates.Count; i++){
                        if (milkCrates[i].horzInd == myPlayerObj.horzInd+1){
                            if (milkCrates[i].vertInd == myPlayerObj.vertInd) {
                                moveObjectRight(milkCrates[i].theObject);
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 0;
                                milkCrates[i].horzInd++;
                                map[milkCrates[i].vertInd, milkCrates[i].horzInd] = 3;
                            }
                        }
                    }
            moveObjectRight(myPlayerObj.theObject);
                    myPlayerObj.horzInd++;
                }


        //Update map
        updateMap(myPlayerObj.theObject.name);

        //Update Durians
        updateDurians();

        //Debug.Log("Players Location: " + myPlayerObj.vertInd + "," + myPlayerObj.horzInd);

        //printMap();


    }//end player move attempt

    //Functions for moving objects
    void moveObjectUp(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x - scale,
             obj.transform.position.y,
             obj.transform.position.z);
    }
    void moveObjectDown(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x + scale,
             obj.transform.position.y,
             obj.transform.position.z);
    }
    void moveObjectLeft(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x,
             obj.transform.position.y,
             obj.transform.position.z - scale);
    }
    void moveObjectRight(GameObject obj)
    {
        obj.transform.position = new Vector3(
             obj.transform.position.x,
             obj.transform.position.y,
             obj.transform.position.z + scale);
    }

	void updateDurians() {
		//To be run after player moves.
		//Check the durians position in relation to the player
		//chase accordingly

		for (int i = 0; i < enemies.Count; i++) {
            bool shouldSkip = false;
			//If the enemy and the player are in the same horizontal index, i.e, the same column
			if (enemies [i].horzInd == myPlayerObj.horzInd) {
				if (enemies [i].vertInd < myPlayerObj.vertInd) {
                    //Player is below enemy
                    ///Check if in line of sight, continue
                    for (int j = enemies[i].vertInd + 1; j < myPlayerObj.vertInd; j++)
                    {
                        if (map[j, enemies[i].horzInd] != 0)
                        {
                            //Space is occupied
                            shouldSkip = true;
                        }
                    }
                   if (shouldSkip)
                    {
                        continue;
                    }
					//move enemy down
					moveObjectDown(enemies[i].theObject);
					enemies[i].theObject.transform.rotation = Quaternion.Euler(0, 90, 0);

					//change that enemies current position on the map to 0
					map[enemies[i].vertInd, enemies[i].horzInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].vertInd++;

					//update the new maps enemy position
					map[enemies[i].vertInd, enemies[i].horzInd] = 2;

				} else if (enemies [i].vertInd > myPlayerObj.vertInd) {
                    //Player is above enemy
                    ///Check if in line of sight, continue

                    for (int j = myPlayerObj.vertInd + 1; j < enemies[i].vertInd; j++)
                    {
                        
                        if (map[j, enemies[i].horzInd] != 0)
                        {
                            //Space is occupied
                            shouldSkip = true;
                        }
                    }
                    if (shouldSkip)
                    {
                        //Debug.Log("Skipped!");
                        continue;
                    }

                    //move enemy Up
					enemies[i].theObject.transform.rotation = Quaternion.Euler(0, 270, 0);
                    moveObjectUp(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].vertInd, enemies[i].horzInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].vertInd--;

					//update the new maps enemy position
					map[enemies[i].vertInd, enemies[i].horzInd] = 2;
				}

			} else if (enemies [i].vertInd == myPlayerObj.vertInd) {
				//same row
				if (enemies [i].horzInd < myPlayerObj.horzInd) {
                    //Player is right of enemy

                    ///Check if in line of sight, continue
                    for (int j = enemies[i].horzInd + 1; j < myPlayerObj.horzInd; j++)
                    {
                        if (map[enemies[i].vertInd, j] != 0)
                        {
                            //Space is occupied
                            shouldSkip = true;
                        }
                    }
                    if (shouldSkip)
                    {
                        continue;
                    }

                    //move enemy Right
					enemies[i].theObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    moveObjectRight(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].vertInd, enemies[i].horzInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].horzInd++;

					//update the new maps enemy position
					map[enemies[i].vertInd, enemies[i].horzInd] = 2;

				} else if (enemies [i].horzInd > myPlayerObj.horzInd) {
                    //Player is Left of Enemy

                    ///Check if in line of sight, continue
                    //(int j = myPlayerObj.vertInd + 1; j < enemies[i].vertInd; j++)
                    for (int j = myPlayerObj.horzInd + 1; j < enemies[i].horzInd; j++)
                    {
                        if (map[enemies[i].vertInd, j] != 0)
                        {
                            //Space is occupied
                            shouldSkip = true;
                        }
                    }
                    if (shouldSkip)
                    {
                        continue;
                    }

                    //move enemy Left
					enemies[i].theObject.transform.rotation = Quaternion.Euler(0, 180, 0);
                    moveObjectLeft(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].vertInd, enemies[i].horzInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].horzInd--;

					//update the new maps enemy position
					map[enemies[i].vertInd, enemies[i].horzInd] = 2;
				}

			}

			//Check if enemy is in the same space as the player. If so, they lose
			if (enemies [i].horzInd == myPlayerObj.horzInd && enemies [i].vertInd == myPlayerObj.vertInd) {
                //Restart scene
                //For some reason, light does not restart so scene gets darker
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

		}


	}

	//update the map..
	void updateMap(string objectToUpdate) {
		//take the name of the object and update accordingly
		if (objectToUpdate == "Player") {
			//Get length of first dimension
			for (int i = 0; i < map.GetLength(0); i++)
			{
				//Get length of second dimension
				for (int n = 0; n < map.GetLength(1); n++)
				{
					//I is vertical,
					//N is horizontally across
					//If its our player
					if (map [i, n] == 1) {
						//Player no longer occupies that spot
						map [i, n] = 0;
					}
				}
			}
			//Assign players new spot
			map[myPlayerObj.vertInd, myPlayerObj.horzInd] = 1;
		}

        //Check if player has hit the end
        if (myPlayerObj.vertInd == endObject.vertInd && myPlayerObj.horzInd == endObject.horzInd)
        {
            if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
            {
                //load the next scene in line
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } else
            {
                //Take us to main menu, no more levels left
                SceneManager.LoadScene("main_menu");
            }


        }
	}

    //Utility Functions

    //function to print out entire map, for debug purposes
    void printMap()
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int n = 0; n < map.GetLength(1); n++)
            {
                Debug.Log("Position: " + i + "," + n + "Contents: " + map[i, n]);
            }
        }
        
    }

    //Lerping function for moving blocks
    float lerp(float v0, float v1, float t)
    {
        return (1 - t) * v0 + t * v1;
    }

}


