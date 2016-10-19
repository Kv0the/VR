using UnityEngine;
using System.Collections;

public class OpenDoorScript : MonoBehaviour {

	private GameObject player;
	public float grados = 60f;
	public float openDoorDeegre;
	public float closeDoorDeegre;
	private bool isOpenDoor = false;

	AudioSource audio;
	public float timeAudio;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("FPSController");
		audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, transform.position) < 5f && Input.GetKeyDown (KeyCode.F)) {
			if (!isOpenDoor) {
				StartCoroutine (this.openDoor ());
			} else {
				StartCoroutine (this.closeDoor ());
			}
		}
	}

	public IEnumerator openDoor() {
		audio.timeSamples = 0;
		audio.pitch = 1.5f;
		audio.Play();
		while (Mathf.Abs(openDoorDeegre - transform.localRotation.eulerAngles.y) > 4f) {
			transform.Rotate (new Vector3 (0f, grados, 0f) * Time.deltaTime);
			yield return null;
		}
		isOpenDoor = !isOpenDoor;
		Debug.Log (audio.time);
		audio.Stop ();
		yield return null;
	}

	public IEnumerator closeDoor() {
		audio.time = timeAudio;
		audio.pitch = -1.5f;
		audio.Play();
		while (Mathf.Abs(closeDoorDeegre - transform.localRotation.eulerAngles.y) > 4f) {
			transform.Rotate (new Vector3 (0f, -grados, 0f) * Time.deltaTime);
			yield return null;
		}
		isOpenDoor = !isOpenDoor;
		audio.Stop ();
		yield return null;
	}
}
