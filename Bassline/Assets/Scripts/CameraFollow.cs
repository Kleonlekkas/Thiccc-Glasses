using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Camera;
    public Transform Player;

    int cameraOffset;
    float m_FieldOfView;
    private void Start()
    {
        cameraOffset = 7;
        m_FieldOfView = 7.0f;
    }
    void Update()
    {
        Camera.position = new Vector3((Player.position.x + cameraOffset), Camera.position.y, Camera.position.z);
    }

}
