using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

	private GameObject player;
    private AudioSource audio;
    public GameObject[] lightFluor;
    public Material onMaterial;
    public Material offMaterial;
    public bool isOn;


    // Use this for initialization
    void Start () {

        //Red Button
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.red);

		player = GameObject.Find("FPSController");
        audio = GetComponent<AudioSource>();
        isOn = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) < 3f && Input.GetKeyDown(KeyCode.F) && !isOn)
        {
            //Green Button
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.green);

            this.GetComponentsInChildren<Light>()[0].color = Color.green;
            for (int i = 0; i < lightFluor.Length; ++i)
            {
                lightFluor[i].transform.GetChild(0).GetComponent<Light>().enabled = true;
                Renderer lightRend = lightFluor[i].GetComponent<Renderer>();
                lightRend.material = onMaterial;
            }
            //this.enabled = false;
			isOn = true;
            audio.Play();
        } else if (Vector3.Distance(player.transform.position, transform.position) < 3f && Input.GetKeyDown(KeyCode.F) && isOn)
        {
            //Red Button
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.red);

            this.GetComponentsInChildren<Light>()[0].color = Color.red;
            for (int i = 0; i < lightFluor.Length; ++i)
            {
                lightFluor[i].transform.GetChild(0).GetComponent<Light>().enabled = false;
                Renderer lightRend = lightFluor[i].GetComponent<Renderer>();
                lightRend.material = offMaterial;
            }
            isOn = false;
            audio.Play();
        }
	}
}
