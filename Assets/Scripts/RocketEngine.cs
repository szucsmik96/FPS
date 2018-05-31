using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketEngine : MonoBehaviour {

	private float speed= 10f; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		transform.Translate (transform.forward*speed*Time.deltaTime, Space.World);
	}
}
