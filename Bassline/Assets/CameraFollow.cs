﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Followplatform;
    public Transform Player;

    void Update()
    {
        Followplatform.position = new Vector3(Player.position.x, Followplatform.position.y, Followplatform.position.z);
    }

}