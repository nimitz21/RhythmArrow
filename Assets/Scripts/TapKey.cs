using UnityEngine;
using System.Collections;

public class TapKey : KeySuperClass
{

	void OnTriggerExit (Collider collider) {
		if (hit) {
			Destroy (gameObject);
			Debug.Log ("Miss " + keyId);
		}
	}

	override public void activate () {
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

