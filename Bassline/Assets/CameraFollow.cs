using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Followplatform;
    public Transform Player;

    int cameraOffset;

    private void Start()
    {
        cameraOffset = 7;
    }
    void Update()
    {
        Followplatform.position = new Vector3((Player.position.x + cameraOffset), Followplatform.position.y, Followplatform.position.z);
    }

}
