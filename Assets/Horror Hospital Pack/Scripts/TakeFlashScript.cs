using UnityEngine;
using System.Collections;

public class TakeFlashScript : MonoBehaviour {

	private bool isGrap = false;
	public Transform positionFlash;
	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("FPSController");
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrap) {
			transform.position = positionFlash.position;
			transform.rotation = player.transform.GetChild(0).transform.rotation * positionFlash.rotation;

		}
		if (Vector3.Distance (player.transform.position, transform.position) < 3f && Input.GetKeyDown (KeyCode.F)) {
			isGrap = true;
			GetComponent<Rigidbody> ().detectCollisions = false;
		}
	}
}
