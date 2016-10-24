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

    public void interaction()
    {
        StartCoroutine(this.lockedDoor());
    }

    public IEnumerator lockedDoor()
    {
        audio.timeSamples = 0;
        audio.pitch = 1.5f;
        audio.Play();
        yield return null;
    }
}
