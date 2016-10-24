using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dead : MonoBehaviour {

    private GameObject player;
    private bool once;
    public AudioClip impact;
    public AudioClip end;
    public AudioClip endWorld;
    float t;
    public AudioSource audioPlayer;

    void Start()
    {
        once = true;
    }

    void Update()
    {
        if (transform.position.y < -20f && once)
        {
            StartCoroutine(this.playImpact());
           // System.Threading.Thread.Sleep(2000);
            StartCoroutine(this.playEnd());
            //StartCoroutine(this.playEndWorld());
            t = Time.time;
            once = false;    
        } else if (transform.position.y < -20f && Time.time - t > 4.0f && !audioPlayer.isPlaying) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public IEnumerator playImpact()
    {
        audioPlayer.timeSamples = 0;
        audioPlayer.pitch = 1.5f;
        audioPlayer.PlayOneShot(impact, 10.0f);
        yield return null;
    }

    public IEnumerator playEnd()
    {
        audioPlayer.timeSamples = 0;
        audioPlayer.pitch = 1f;
        audioPlayer.PlayOneShot(end, 1.0f);
        audioPlayer.PlayOneShot(endWorld, 7.0f);
        yield return null;
    }

    public IEnumerator playEndWorld()
    {
        audioPlayer.timeSamples = 0;
        audioPlayer.pitch = 1f;     
        yield return null;
    }
}
