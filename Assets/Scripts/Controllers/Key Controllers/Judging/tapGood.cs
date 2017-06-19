using UnityEngine;
using System.Collections;

public class tapGood : goodZone
{

	protected override void OnTriggerExit (Collider collider) {
		if (parentKey.good) {
			Destroy (gameObject);
			Debug.Log ("Miss");
		}
	}

}

