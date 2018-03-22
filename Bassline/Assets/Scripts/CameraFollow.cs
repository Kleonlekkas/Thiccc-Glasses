using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform CameraObj;
    public Transform Player;

    int cameraOffset;
    float m_FieldOfView;

    // get current field of view
    public float scaleFOV;

    private void Start()
    {
        cameraOffset = 7;
        m_FieldOfView = 10.0f;
    }
    void Update()
    {
        // apply FOV
        m_FieldOfView = Player.position.y + scaleFOV;
		
		CameraObj.position = new Vector3((Player.position.x + cameraOffset), Player.position.y, CameraObj.position.z);

        //Set camera field of view based on players y
        //(Camera is a keyword, had to change to CameraObj)

        if (Player.position.y < 0) {
            m_FieldOfView = Player.position.y + (2*scaleFOV);
        }

		//Set a tiny cap
		if (m_FieldOfView > 15) {
			Camera.main.orthographicSize = m_FieldOfView;
		}

    }

}
