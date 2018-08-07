using UnityEngine;

public class CameraScript : MonoBehaviour {

    public float speedV = 2.0f;
    public float speedH = 2.0f;

    float yaw = 0;
    float pitch = 0;

	void Start()
	{
        Cursor.lockState = CursorLockMode.Locked;
	}

	void Update () {
        yaw += speedH * Input.GetAxis("Mouse X");
        pitch += speedH * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(-pitch, yaw, 0);
	}
}
