using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapKey : MonoBehaviour {

	private static int hitKeyCounter = 0;

	private int keyId;
	public int ownerARrrowId;
	private bool hit = false;


	void OnTriggerEnter (Collider collider) {
		if (hitKeyCounter == keyId && collider.transform.GetComponent <ArrowController> ().arrowId == ownerARrrowId) {
			if (collider.transform.tag == "Arrow") {
				hit = true;
			}
			++hitKeyCounter;
		}
	}

	void OnTriggerExit (Collider collider) {
		if (hit) {
			Destroy (gameObject);
			Debug.Log ("Miss " + keyId);
		}
	}

	public void setKeyId (int newKeyId) {
		keyId = newKeyId;
	}

	public void setOwnerArrowId (int newId) {
		ownerARrrowId = newId;
	}

	public void activate() {
		if (Input.GetMouseButtonDown (0))  {
			if (hit) {
				Debug.Log ("Perfect " + keyId );
			} else {
				Debug.Log ("Bad " + keyId);
			}
			hit = false;
			Destroy (gameObject);
		}
	}
}
