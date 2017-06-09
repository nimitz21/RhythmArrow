using UnityEngine;
using System.Collections;

public class TapKey : KeySuperClass
{

	override public void tap () {
		Destroy (gameObject);
		if (hit) {
			Debug.Log ("Perfect");
		} else {
			Debug.Log ("Bad");
		}
	}

	override public void hold () {
		//do nothing
	}

	override public void unHold () {
		//do nothing
	}

	protected override void OnTriggerExit (Collider collider) {
		if (hit) {
			Destroy (gameObject);
			Debug.Log ("Miss");
		}
	}

}

