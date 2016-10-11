using UnityEngine;
using System.Collections;

public class Blinking : MonoBehaviour
{

    public

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float factor = 10;
        float rand = Random.value*factor;
        if (rand < 0.7)
        {
            gameObject.GetComponent<Light>().enabled = false;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else gameObject.GetComponent<Light>().enabled = true;
    }
}