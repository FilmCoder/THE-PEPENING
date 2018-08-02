using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float speedV = 2.0f;
    public float speedH = 2.0f;

    private float yaw = 0;
    private float pitch = 0;

	private void Start()
	{
        Cursor.lockState = UnityEngine.CursorLockMode.Locked;
	}

	void Update () {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedH * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(-pitch, yaw, 0);
	}
}
