using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform CameraObj;
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
		//for testing purposes to see it all


			m_FieldOfView = Player.position.y;
		


		CameraObj.position = new Vector3((Player.position.x + cameraOffset), Player.position.y, CameraObj.position.z);




		//Set camera field of view based on players y
		//(Camera is a keyword, had to change to CameraObj)

		//Set a tiny cap
		if (m_FieldOfView > 10) {
			Camera.main.orthographicSize = m_FieldOfView;
		}

    }

}
