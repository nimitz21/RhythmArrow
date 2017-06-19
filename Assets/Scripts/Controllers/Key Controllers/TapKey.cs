using UnityEngine;
using System.Collections;

public class TapKey : KeySuperClass
{

	override public void tap () {
		Destroy (gameObject);
		if (perfect) {
			Debug.Log ("Perfect");
		} else if (good) {
			Debug.Log ("Good");
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

}

