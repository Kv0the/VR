using UnityEngine;
using System.Collections;

public class ColorButton : MonoBehaviour {

    private GameObject lightbut;
	private GameObject player;
    public GameObject[] lightFluor;
	public bool isOn = false;


    // Use this for initialization
    void Start () {

        //Red Button
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.red);

        lightbut = GameObject.Find("ButtonLight");
		player = GameObject.Find("FPSController");
    }
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance(player.transform.position, transform.position) < 3f && Input.GetKeyDown(KeyCode.F))
        {
            //Green Button
            Renderer rend = GetComponent<Renderer>();
            rend.material.shader = Shader.Find("Specular");
            rend.material.SetColor("_Color", Color.green);

            //StartCoroutine(Fluorescent);

            lightbut.GetComponent<Light>().color = Color.green;
            //LightFluor.GetComponent<Renderer>().material.SetColor("_EmissionColor")
            for (int i = 0; i < lightFluor.Length; ++i)
            {
                lightFluor[i].transform.GetChild(0).GetComponent<Light>().enabled = true;
                lightFluor[i].transform.GetChild(1).GetComponent<Light>().enabled = true;
            }
            this.enabled = false;
			isOn = true;
        }
	}

    /*private IEnumerator Fluorescent()
    {
        yield return new WaitForSeconds(0.2f);
        LightFluor.transform.GetChild(1).GetComponent<Light>().enabled = true;
    }*/
}
