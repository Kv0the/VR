using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class GrabScript : MonoBehaviour {

	GameObject grabedObject;
    GameObject candidateObject;
    float grabObjectSize;
	Vector3 actualPosition;
	float positionLengthVar = 1000;
	bool rotateModel = false;

	public float thrust;
	public FirstPersonController fpc;
	public ColorButton colorButton;

	public float rotateSpeed = 2.0f;

	GameObject GetMouseHoverObject(float range) {
		Vector3 position = gameObject.transform.position;
		RaycastHit raycastHit;
		Vector3 target = position + Camera.main.transform.forward * range;

		if (Physics.Linecast (position, target, out raycastHit))
			return raycastHit.collider.gameObject;

		return null;
	}

	void rotateFunction() {
		if(Input.GetKey(KeyCode.A))
            grabedObject.transform.Rotate(rotateSpeed, 0, 0);
        if (Input.GetKey(KeyCode.S))
            grabedObject.transform.Rotate(0, rotateSpeed, 0);
        if (Input.GetKey(KeyCode.D))
            grabedObject.transform.Rotate(0, 0, rotateSpeed);
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

		Vector3 direction = (Camera.main.transform.forward * positionLengthVar - actualPosition);
		direction.Normalize ();

		grabedObject.GetComponent<Rigidbody> ().AddForce (direction * thrust, ForceMode.Acceleration);
		grabedObject = null;

	}
	
	// Update is called once per frame
	void Update () {
		if (colorButton.isOn) {
			//Debug.Log (GetMouseHoverObject (5));
			TryHighlightObject (GetMouseHoverObject (5));

			if (Input.GetMouseButtonDown (0)) {
				if (grabedObject == null) {
					TryGrabObject (GetMouseHoverObject (5));
				} else
					DropObject ();
			}

			if (grabedObject != null) {
				if (Input.GetMouseButtonDown (1)) {
					rotateModel = true;
					fpc.enabled = false;
				} else if (Input.GetMouseButtonUp (1)) {
					rotateModel = false;
					fpc.enabled = true;
				}

				Vector3 newPosition = gameObject.transform.position + Camera.main.transform.forward * grabObjectSize;
				actualPosition = Camera.main.transform.forward * positionLengthVar;
				grabedObject.transform.position = newPosition;
				grabedObject.GetComponent<Rigidbody> ().angularVelocity = Vector3.zero;
				grabedObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
				if (rotateModel) {
					rotateFunction ();
				}
			}
		}
	}
}
