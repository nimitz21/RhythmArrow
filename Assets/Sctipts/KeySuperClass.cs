using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeySuperClass : MonoBehaviour {

	private static int hitKeyCounter = 0;

	protected int keyId;
	protected int ownerArrowId;
	protected bool hit = false;

	void OnTriggerEnter (Collider collider) {
		if (hitKeyCounter == keyId && collider.transform.GetComponent <ArrowController> ().arrowId == ownerArrowId) {
			if (collider.transform.tag == "Arrow") {
				hit = true;
			}
			++hitKeyCounter;
		}
	}
		
	public void setKeyId (int newKeyId) {
		keyId = newKeyId;
	}

	public void setOwnerArrowId (int newOwnerArrowId) {
		ownerArrowId = newOwnerArrowId;
	}

	public abstract void activate ();

}