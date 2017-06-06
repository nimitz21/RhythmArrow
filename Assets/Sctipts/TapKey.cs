using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapKey : MonoBehaviour {

	private static int hitKeyCounter = 0;

	private int keyId;
	private bool hit = false;

	void OnCollisionEnter2D (Collision2D collision) {
		if (hitKeyCounter == keyId) {
			if (collision.transform.tag == "Arrow") {
				hit = true;
			}
			++hitKeyCounter;
		}
	}

	void OnCollisionExit2D (Collision2D collision) {
		if (hit) {
			Destroy (gameObject);
			Debug.Log ("Miss " + keyId);
		}
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0))  {
			if (hit) {
				Debug.Log ("Perfect");
			} else {
				Debug.Log ("Bad");
			}
			Destroy (gameObject);
		}
	}

	public void setKeyId (int newKeyId) {
		keyId = newKeyId;
	}
}
