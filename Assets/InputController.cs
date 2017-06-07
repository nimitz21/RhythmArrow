using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Debug.Log ("click " + Camera.main.ScreenPointToRay (Input.mousePosition).origin + " " + Camera.main.ScreenPointToRay (Input.mousePosition).direction * 10);
			RaycastHit hit;
			int keyLayerMask = 1 << 9;
			if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, keyLayerMask)) {
				hit.transform.GetComponent <TapKey> ().activate ();
			}
		}
	}
}
