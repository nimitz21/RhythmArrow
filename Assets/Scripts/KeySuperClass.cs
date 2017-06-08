using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeySuperClass : MonoBehaviour {

	private static int hitKeyCounter = 0;

	protected int keyId; //should be private after debug.log is removed from inherited class
	private int ownerArrowId;
	private float spawnTime;

	protected bool hit = false;

	void OnTriggerStay (Collider collider) {
		if (!hit) {
			if (Time.time > spawnTime - 0.1) { 
				if (hitKeyCounter == keyId && collider.transform.GetComponent <ArrowController> ().arrowId == ownerArrowId) {
					if (collider.transform.tag == "Arrow") {
						hit = true;
					}
					++hitKeyCounter;
				}
			}
		}
	}
		
	public void setKeyId (int newKeyId) {
		keyId = newKeyId;
	}

	public void setOwnerArrowId (int newOwnerArrowId) {
		ownerArrowId = newOwnerArrowId;
	}

	public void setSpawnTime (float newSpawnTime) {
		spawnTime = newSpawnTime;
	}

	public abstract void activate ();

}