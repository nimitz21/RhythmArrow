using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeySuperClass : MonoBehaviour {

	private UnityAction unHoldListener; 

	protected bool hit = false;
	protected float spawnTime;


	void Awake () {
		unHoldListener = new UnityAction (unHold);
	}

	void OnEnable () {
		InputController.startListeningToUnhold (unHoldListener);
	}

	void OnDisable () {
		InputController.stopListeningToUnhold (unHoldListener);
	}

	public void setSpawnTime (float newSpawnTime) {
		spawnTime = newSpawnTime;
	}

	public abstract void tap ();

	public abstract void hold ();

	public abstract void unHold ();

	protected abstract void OnTriggerExit (Collider collider);

	protected virtual void OnTriggerStay (Collider collider) {
		if (!hit) {
			if (Time.time > spawnTime - 0.1) { 
				if (collider.transform.tag == "Arrow") {
					hit = true;
				}
			}
		}
	}

}