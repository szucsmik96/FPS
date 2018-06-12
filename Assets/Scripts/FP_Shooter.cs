using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Shooter : MonoBehaviour {

	Actions actions;

	float Gcooldown = 1f;
	float GcooldownRemaining = 0f;

	float Rcooldown = 1f;
	float RcooldownRemaining = 0f;

	float Bcooldown = 0.3f;
	float BcooldownRemaining = 0f;

	public GameObject grenadePrefab;
	private float grenadeImpulse = 100f;

	public GameObject rocketPrefab;
	private Rigidbody rb;


	public float range = 100f;
	public GameObject bulletPrefab;

	private LineRenderer line;
	private Transform laserSpawn;

	// Use this for initialization
	void Start () {
		line = gameObject.GetComponentInChildren<LineRenderer> ();
		//laserSpawn = transform.GetChild(0).transform.GetChild(0).transform.GetChild (0);
		laserSpawn = transform.GetChild(0).transform.GetChild(0);
		//Debug.Log (laserSpawn);
		line.enabled = false;
		actions = GetComponentInChildren<Actions> ();
	}
	
	// Update is called once per frame
	void Update () {

		GcooldownRemaining -= Time.deltaTime;
		RcooldownRemaining -= Time.deltaTime;
		BcooldownRemaining -= Time.deltaTime;


		if (Input.GetButtonDown ("Fire2") && GcooldownRemaining <0) {
			GcooldownRemaining = Gcooldown;
			GameObject grenade = (GameObject)Instantiate (grenadePrefab, Camera.main.transform.position + Camera.main.transform.forward, Camera.main.transform.rotation);
			rb = grenade.GetComponent<Rigidbody> ();
			rb.AddForce (Camera.main.transform.forward*grenadeImpulse, ForceMode.Force);
		}
		if(Input.GetMouseButtonDown(0) && BcooldownRemaining <0){
			BcooldownRemaining = Bcooldown;
			actions.Attack ();
			Ray ray = new Ray (Camera.main.transform.position, Camera.main.transform.forward);
			RaycastHit hitinfo;
			if(Physics.Raycast(ray, out hitinfo, range)){ 
				Vector3 hitPoint = hitinfo.point;
				GameObject go = hitinfo.collider.gameObject;
				//Debug.Log (go.name);
				FireLaser (ray, hitPoint);
				if (go.tag != "Plane") {
					GameObject bullet = (GameObject)Instantiate (bulletPrefab, hitPoint, Quaternion.identity);
				}
			}
			Debug.Log (laserSpawn.transform.position);
		}
		if (Input.GetKeyDown (KeyCode.F) && RcooldownRemaining <0) {
			RcooldownRemaining = Rcooldown;
			GameObject rocket = (GameObject)Instantiate (rocketPrefab, Camera.main.transform.position+Camera.main.transform.forward , Camera.main.transform.rotation);
		}
	}

	public void FireLaser(Ray ray, Vector3 hitPoint){
		line.enabled = true;
		line.SetPosition (0, laserSpawn.transform.position);
		line.SetPosition (1, hitPoint);
		Invoke ("StopLaser", 0.1f);
	}
	public void StopLaser(){
		line.enabled = false;
	}
}
