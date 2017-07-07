using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class KeySuperClass : MonoBehaviour {

	private UnityAction unHoldListener; 

	public bool good = false;
	public bool perfect = false;
	public float spawnTime;

	void Awake () {
		unHoldListener = new UnityAction (unHold);
	}

	void OnEnable () {
		InputController.startListeningToUnhold (unHoldListener);
	}

	void OnDisable () {
		InputController.stopListeningToUnhold (unHoldListener);
	}

	public abstract void tap ();

	public abstract void hold ();

	public abstract void unHold ();

}