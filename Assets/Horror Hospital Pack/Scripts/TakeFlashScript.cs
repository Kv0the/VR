using UnityEngine;
using System.Collections;

public class TakeFlashScript : MonoBehaviour {

	private bool isGrap = false;
	public Transform positionFlash;
	private GameObject player;
	private int level = 1;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("FPSController");
	}
	
	// Update is called once per frame
	void Update () {
		if (isGrap) {
			transform.parent = positionFlash;
			transform.position = positionFlash.position;
			transform.rotation = positionFlash.rotation;
			if (Input.GetKeyDown (KeyCode.E)) {
				level += 1;
				GetComponentInChildren<Light> ().intensity = 2 * level;
				if (level == 3)
					level = 0;
			}
		}
		if (Vector3.Distance (player.transform.position, transform.position) < 3f && Input.GetKeyDown (KeyCode.F)) {
			isGrap = true;
			GetComponent<Rigidbody> ().detectCollisions = false;
			GetComponent<Rigidbody> ().isKinematic = false;
		}
	}
}
