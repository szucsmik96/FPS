using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour {

	private CharacterController characterController;

	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 2.0f;

	private float verticalRotation = 0f;
	private float verticalVelocity = 0f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float rotLeftRight = Input.GetAxis("Mouse X")*mouseSensitivity;
		transform.Rotate(0, rotLeftRight,0);

		verticalRotation -= Input.GetAxis ("Mouse Y") * mouseSensitivity;
		verticalRotation = Mathf.Clamp (verticalRotation, -80f, 80f);
		Camera.main.transform.localRotation = Quaternion.Euler (verticalRotation, 0, 0);

		float forwardSpeed = Input.GetAxis ("Vertical") * movementSpeed;
		float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
		verticalVelocity += Physics.gravity.y * Time.deltaTime;
		if (characterController.isGrounded && Input.GetButton("Jump")) {
			verticalVelocity = jumpSpeed;
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = speed * 2;
		}

		characterController.Move(speed * Time.deltaTime);


	}
}
