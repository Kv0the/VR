using UnityEngine;
using System.Collections;

public class LockedDoor : MonoBehaviour {

    private GameObject player;
    AudioSource audio;
    public float timeAudio;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("FPSController");
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(player.transform.position, transform.position) < 5f && Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(this.lockedDoor());
        }
    }

    public IEnumerator lockedDoor()
    {
        audio.timeSamples = 0;
        audio.pitch = 1.5f;
        audio.Play();
        yield return null;
    }
}
