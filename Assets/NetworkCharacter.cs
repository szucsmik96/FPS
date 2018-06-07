using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkCharacter : Photon.MonoBehaviour {
	//photon view observes this instead of the transform of playercontroller

	Vector3 realPosition = Vector3.zero;
	Quaternion realRotation = Quaternion.identity;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			
		} else {
			transform.position = Vector3.Lerp (transform.position, realPosition, 0.1f);
			transform.rotation = Quaternion.Lerp (transform.rotation, realRotation, 0.1f);
		}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info){
		if (stream.isWriting) {
			//This is our player, send actual position to the network
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			//someone else's player. receive their position and update our version of that player
			realPosition=(Vector3)stream.ReceiveNext();
			realRotation=(Quaternion)stream.ReceiveNext();
		}
	}
}
