using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour {

	private static UnityEvent unHoldEvent = new UnityEvent ();

	void Update () {
		RaycastHit hit;
		int keyLayerMask = 1 << 9;
		if (Input.GetMouseButtonDown (0)) {
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, keyLayerMask)) {
				hit.transform.GetComponent <KeySuperClass> ().tap ();
			}
		}
		if (Input.GetMouseButton (0) && Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, Mathf.Infinity, keyLayerMask) ) {
			hit.transform.GetComponent <KeySuperClass> ().hold ();
		} else {
			unHoldEvent.Invoke ();
		}
	}

	public static void startListeningToUnhold (UnityAction listener) {
		unHoldEvent.AddListener (listener);
	}

	public static void stopListeningToUnhold (UnityAction listener) {
		unHoldEvent.RemoveListener (listener);
	}

}
