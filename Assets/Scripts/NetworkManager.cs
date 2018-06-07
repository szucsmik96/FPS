using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour {

	public GameObject standbyCamera;
	SpawnSpot[] spawnSpots;

	// Use this for initialization
	void Start () {
		spawnSpots = GameObject.FindObjectsOfType<SpawnSpot>();
		Connect ();
	}

	static void Connect ()
	{
		PhotonNetwork.ConnectUsingSettings ("MultiFPS v001");
		//PhotonNetwork.offlineMode = true;
	}

	void OnGUI(){
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());
	}
	void OnConnectedToMaster(){
		PhotonNetwork.JoinRandomRoom();
	}
	void OnPhotonRandomJoinFailed(){
		PhotonNetwork.CreateRoom (null);
	}
	void OnJoinedRoom(){
		Debug.Log ("joined");
		SpawnMyPlayer ();
	}
	void SpawnMyPlayer(){
		if (spawnSpots == null) {
			Debug.LogError ("no spawnspots");
			return;
		}
		SpawnSpot mySpawnSpot = spawnSpots[Random.Range(0,spawnSpots.Length)];
		GameObject myPlayerGO = (GameObject)PhotonNetwork.Instantiate ("PlayerController", mySpawnSpot.transform.position, mySpawnSpot.transform.rotation, 0);
		myPlayerGO.GetComponent<FP_Controller> ().enabled = true;
		myPlayerGO.GetComponent<FP_Shooter> ().enabled = true;
		myPlayerGO.transform.Find ("Main Camera").gameObject.SetActive (true);
		standbyCamera.SetActive (false);
	}
}
