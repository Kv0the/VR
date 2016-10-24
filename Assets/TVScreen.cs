using UnityEngine;
using System.Collections;

public class TVScreen : MonoBehaviour
{

    private Renderer r;
    AudioSource audioS;
    private GameObject player;
    private Light lightTV;
    private bool b;
    private int tries;
    public Texture background;
    public MovieTexture[] channelsTV;
    public AudioClip[] soundsTV;
    private AudioSource[] sourceTV;
    private bool[] isPlayingChannel;
    public bool isOn = false;

    // Use this for initialization
    void Start()
    {
        r = this.GetComponent<Renderer>();
        player = GameObject.Find("FPSController");
        lightTV = GetComponentInChildren<Light>();
        tries = 0;
        sourceTV = new AudioSource[channelsTV.Length+1];
        isPlayingChannel = new bool[channelsTV.Length+1];
        for (int i = 0; i < channelsTV.Length+1; ++i)
        {
            sourceTV[i] = this.gameObject.AddComponent<AudioSource>();
            sourceTV[i].spatialBlend = 1.0f;
            isPlayingChannel[i] = false;
        }
        r.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 3f && Input.GetKeyDown(KeyCode.E))
        {
            if (tries % (channelsTV.Length + 1) == channelsTV.Length)
            {
                r.enabled = false;
                lightTV.enabled = false;
            }
            else
            {
                r.enabled = true;
                r.material.mainTexture = channelsTV[tries % (channelsTV.Length + 1)];
                lightTV.enabled = true;
            }
            b = true;
            if (tries < channelsTV.Length + 1)
            {
                if (tries != channelsTV.Length)
                {
                    MovieTexture movie = channelsTV[tries];
                    movie.loop = true;
                    movie = channelsTV[tries];
                    movie.Play();
                    sourceTV[tries].clip = soundsTV[tries];
                    sourceTV[tries].loop = true;
                }
            }
            for (int i = 0; i < (channelsTV.Length + 1); ++i)
            {
                if (!isPlayingChannel[tries % (channelsTV.Length + 1)])
                {
                    if (tries != channelsTV.Length) sourceTV[tries % (channelsTV.Length + 1)].Play();
                    else isPlayingChannel[tries % (channelsTV.Length + 1)] = true;
                    isPlayingChannel[tries % (channelsTV.Length + 1)] = true;
                }
                if (i == tries % (channelsTV.Length + 1)) sourceTV[i].mute = false;
                else sourceTV[i].mute = true;
            }
            tries++;
        }
    }
}
