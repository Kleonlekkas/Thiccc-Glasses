using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	//List of all our enemies, duh
	List<MyObject> enemies;

	public GameObject enemy;
	public GameObject end;

    //Map to hold coordinates of our objects
    private int[,] map;
    //Scale that objects will be moved by.
    private float scale;


    // Use this for initialization
    void Start ()
    {

		//initialize lists (will have one for each type of object that
		//has multiples. then we'll put those lists into one SUPER LIST
		enemies = new List<MyObject>();

        //Initialize map. this will be streamed in in the future
        //1 is player, 2 is durian, 3 is movable object, 4 is immovable object, 5 is goal
        map = new int[,]{
            { 0, 2, 0, 0, 5 },
            { 0, 0, 2, 0, 0 },
            { 0, 0, 0, 2, 0 },
            { 0, 0, 0, 0, 0 },
            { 1, 0, 0, 0, 0 }
        };

        scale = 3.0f;

        //Set its position to be the center of the grid we made
		m_Camera.transform.position = new Vector3(scale * (map.GetLength(0) / 2), (scale * 5), scale * (map.GetLength(0) / 2));

        vertWall.localScale = new Vector3(0.1f, 0.1f, scale * (map.GetLength(0)));
        horzWall.localScale = new Vector3(scale * (map.GetLength(1)), 0.1f, 0.1f);

        //Draw in the walls
        for (int i = 0; i < map.GetLength(0); i++)
        {
            Instantiate(vertWall, new Vector3((i * scale) - scale / 2, 0, scale * (map.GetLength(1) / 2)), vertWall.transform.rotation);
        }
        for (int n = 0; n < map.GetLength(1); n++)
        {
            Instantiate(horzWall, new Vector3(scale * (map.GetLength(0) / 2), 0, (n * scale) - scale / 2), horzWall.transform.rotation);
        }

            Instantiate(vertWall, new Vector3(((map.GetLength(0)) * scale) - scale / 2, 0, scale * (map.GetLength(0) / 2)), vertWall.transform.rotation);
            Instantiate(horzWall, new Vector3(scale * (map.GetLength(1) / 2), 0, ((map.GetLength(1)) * scale) - scale / 2), horzWall.transform.rotation);

            Transform myFloor = Instantiate(floor, new Vector3(scale * (map.GetLength(0) / 2), 0.0f, scale * (map.GetLength(1) / 2)), floor.transform.rotation);
            myFloor.localScale = new Vector3(scale * (map.GetLength(0)), 0.1f, scale * (map.GetLength(1)));
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
					Debug.Log ("Player: " + i.ToString() + " , " + n.ToString());
                }
				//Place enemy
				if (map[i, n] == 2)
				{
					enemies.Add(new MyObject(Instantiate (enemy, new Vector3(i * scale, 0.0f, n * scale), enemy.transform.rotation), n, i));
					Debug.Log ("Enemy: " + i.ToString() + " , " + n.ToString());
				}
				//Place End
				if (map[i, n] == 5)
				{
					Instantiate (end, new Vector3(i * scale, 0.0f, n * scale), end.transform.rotation);
					Debug.Log ("Goal: " + i.ToString() + " , " + n.ToString());
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
				if (map [i, n] == 1) {
					//Left and Right side
					if (n == 0) {
						leftBlocked = true;
					} else if (n == (map.GetLength(0) - 1)) {
						rightBlocked = true;
					}
					//Top and Bottom
					if (i == 0) {
						topBlocked = true;
					} else if (i == (map.GetLength(1) - 1)) {
						bottomBlocked = true;
					}
				}
			}
		}


        //The player wants to move up
        if (direction == "Up" && !topBlocked)
        {
			moveObjectUp(myPlayerObj.theObject);
			myPlayerObj.vertInd--;
        }

        //The player wants to move Down
        if (direction == "Down" && !bottomBlocked)
        {
			moveObjectDown(myPlayerObj.theObject);
			myPlayerObj.vertInd++;
        }

        //The player wants to move Left
        if (direction == "Left" && !leftBlocked)
        {
			moveObjectLeft(myPlayerObj.theObject);
			myPlayerObj.horzInd--;
        }

        //The player wants to move Right
		if (direction == "Right" && !rightBlocked)
        {
			moveObjectRight(myPlayerObj.theObject);
			myPlayerObj.horzInd++;
        }

		//Update Durians
		updateDurians();

		//Update map
		updateMap (myPlayerObj.theObject.name);
			

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
				//Should check if in line of sight, we'll do that later
				if (enemies [i].vertInd < myPlayerObj.vertInd) {
					//Player is below enemy
                    ///Check if in line of sight, continue
                   for (int j = enemies[i].vertInd + 1; j < (myPlayerObj.vertInd - enemies[i].vertInd) + enemies[i].vertInd; j++)
                    {
                        if (map[j, enemies[i].horzInd] != 0)
                        {
                            //Space is occupied
                            Debug.Log("Vertical Checked: " + j.ToString());
                            shouldSkip = true;
                        }
                    }
                   if (shouldSkip)
                    {
                        continue;
                    }

					//move enemy down
					moveObjectDown(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].horzInd, enemies[i].vertInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].vertInd++;

					//update the new maps enemy position
					map[enemies[i].horzInd, enemies[i].vertInd] = 2;

				} else if (enemies [i].vertInd > myPlayerObj.vertInd) {
					//Player is above enemy

					//move enemy Up
					moveObjectUp(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].horzInd, enemies[i].vertInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].vertInd--;

					//update the new maps enemy position
					map[enemies[i].horzInd, enemies[i].vertInd] = 2;
				}

			} else if (enemies [i].vertInd == myPlayerObj.vertInd) {
				//same row
				if (enemies [i].horzInd < myPlayerObj.horzInd) {
					//Player is right of enemy

					//move enemy Right
					moveObjectRight(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].horzInd, enemies[i].vertInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].horzInd++;

					//update the new maps enemy position
					map[enemies[i].horzInd, enemies[i].vertInd] = 2;

				} else if (enemies [i].horzInd > myPlayerObj.horzInd) {
					//Player is Left of Enemy

					//move enemy Left
					moveObjectLeft(enemies[i].theObject);

					//change that enemies current position on the map to 0
					map[enemies[i].horzInd, enemies[i].vertInd] = 0;

					//change the enemies personal index accordingly
					enemies[i].horzInd--;

					//update the new maps enemy position
					map[enemies[i].horzInd, enemies[i].vertInd] = 2;
				}

			}

			//Check if enemy is in the same space as the player. If so, they lose
			if (enemies [i].horzInd == myPlayerObj.horzInd && enemies [i].vertInd == myPlayerObj.vertInd) {
				Debug.Log ("Player Loses");
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
	}


}


