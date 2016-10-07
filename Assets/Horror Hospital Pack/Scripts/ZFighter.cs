using UnityEngine;
using System.Collections;

public class ZFighter : MonoBehaviour
{

    // Use this for initialization
    private Vector3 lastLocalPosition;
    private Vector3 lastCamPos;
    private Vector3 lastPos;

    void Start()
    {
        lastLocalPosition = transform.localPosition;
        lastPos = transform.position;
        lastCamPos = Camera.main.transform.position - Vector3.up; // just to force update on first frame
    }

    void Update()
    {
        Vector3 camPos = Camera.main.transform.position;
        if (camPos != lastCamPos || transform.position != lastPos)
        {
            lastCamPos = camPos;
            transform.localPosition = lastLocalPosition + (camPos - transform.position) * 0.01f;
            lastPos = transform.position;
        }
    }
}