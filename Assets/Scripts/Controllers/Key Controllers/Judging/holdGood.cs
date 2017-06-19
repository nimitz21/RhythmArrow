using UnityEngine;
using System.Collections;

public class holdGood : goodZone
{
	private HoldKey parentholdKey;

	void Start () {
		parentholdKey = GetComponentInParent <HoldKey> ();
	}

	protected override void OnTriggerExit (Collider collider) {
		if (!parentholdKey.getBeingHeld) {
			Destroy (gameObject);
			Debug.Log ("Miss");
		}
	}


}

