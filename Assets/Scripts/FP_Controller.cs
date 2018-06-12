using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour {

	Actions actions;
	PlayerController playerController;

	private CharacterController characterController;
	Animator animator;

	public float movementSpeed = 5.0f;
	public float mouseSensitivity = 5.0f;
	public float jumpSpeed = 2.0f;

	private float verticalRotation = 0f;
	private float verticalVelocity = 0f;

	// Use this for initialization
	void Start () {
		Screen.lockCursor = true;
		characterController = GetComponent<CharacterController> ();
		actions = GetComponentInChildren<Actions> ();
		playerController = GetComponentInChildren<PlayerController> ();
		playerController.SetArsenal ("Rifle");
		animator = GetComponentInChildren<Animator> ();
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
			actions.Jump ();
		}

		Vector3 speed = new Vector3 (sideSpeed, verticalVelocity, forwardSpeed);

		speed = transform.rotation * speed;

		if (Input.GetKey (KeyCode.LeftShift)) {
			forwardSpeed = forwardSpeed * 2;
			speed = speed * 2;
		}

		characterController.Move(speed * Time.deltaTime);

		animator.SetFloat ("Speed", Mathf.Abs(forwardSpeed));

		if (Mathf.Abs(forwardSpeed) > 0 || Mathf.Abs(sideSpeed) >0) {
			animator.SetBool("Aiming", false);
		}
	}
}
