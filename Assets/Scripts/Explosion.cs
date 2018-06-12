using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

	float lifeSpan = 3f;
	public GameObject fireEffect;
	public GameObject explosionEffect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		lifeSpan -= Time.deltaTime;
		if (lifeSpan < 0) {
			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Collider[] cols = Physics.OverlapSphere (transform.position, 5f);
			foreach (Collider col in cols) {
				if (col.gameObject.tag == "Enemy") {
					Instantiate(fireEffect, col.transform.position, Quaternion.identity);
					col.gameObject.tag = "Untagged";
					Debug.Log ("multiple enemies hit");
				}
			}
			Destroy (gameObject);
		}
	}

	void OnCollisionEnter(Collision collision){
		//Debug.Log (collision.gameObject.tag);
		if (collision.gameObject.tag == "Enemy") {
			collision.gameObject.tag = "Untagged";
			Instantiate(fireEffect, collision.transform.position, Quaternion.identity);
			Instantiate(explosionEffect, transform.position, Quaternion.identity);
			Destroy (gameObject);
		}
	}
}
