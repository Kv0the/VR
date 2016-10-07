using UnityEngine;
using System.Collections;

public class GrabScript : MonoBehaviour {

	GameObject grabedObject;
    GameObject candidateObject;
    float grabObjectSize;
	Vector3 actualPosition;

	public float thrust;

	GameObject GetMouseHoverObject(float range) {
		Vector3 position = gameObject.transform.position;
		RaycastHit raycastHit;
		Vector3 target = position + Camera.main.transform.forward * range;

		if (Physics.Linecast (position, target, out raycastHit))
			return raycastHit.collider.gameObject;

		return null;
	}

	bool CanGrab(GameObject candidate) {
        return candidate.GetComponent<Rigidbody> () != null && candidate != GameObject.Find("FPSController");
	}

	void TryGrabObject(GameObject grabObject) {
		if (grabObject == null || !CanGrab(grabObject))
			return;

		grabedObject = grabObject;
		grabObjectSize = grabObject.GetComponent<Renderer>().bounds.size.magnitude + 0.6f;
    }

    void TryHighlightObject(GameObject grabObject)
    {
        if(grabedObject == null && candidateObject != null) {
            candidateObject.GetComponent<Renderer>().material.shader = Shader.Find("Standard");
            candidateObject = null;
        }

        if (grabObject == null || !CanGrab(grabObject))
            return;
        

        grabObject.GetComponent<Renderer>().material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
        candidateObject = grabObject;
    }

    void DropObject(){
		if (grabedObject == null)
			return;

		Vector3 direction = (Camera.main.transform.forward * thrust - actualPosition);
		direction.Normalize ();

		Debug.Log (direction);

		grabedObject.GetComponent<Rigidbody> ().AddForce (direction * thrust, ForceMode.Acceleration);
		grabedObject = null;

	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log (GetMouseHoverObject (5));
        TryHighlightObject(GetMouseHoverObject(5));

        if (Input.GetMouseButtonDown (0)) {
			Debug.Log (GetMouseHoverObject (5));
			if (grabedObject == null)
				TryGrabObject (GetMouseHoverObject (5));
			else
				DropObject ();
		}

		if(grabedObject != null) {
			Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward*grabObjectSize;
			actualPosition =Camera.main.transform.forward * thrust;
			grabedObject.transform.position = newPosition;
            grabedObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
	}
}
