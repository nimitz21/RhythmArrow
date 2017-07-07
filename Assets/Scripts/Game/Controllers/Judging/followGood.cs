using UnityEngine;
using System.Collections;

public class followGood : goodZone
{
	private FollowKey parentFollowKey;

	void Start () {
		parentFollowKey = GetComponentInParent <FollowKey> ();
	}

	protected override void OnTriggerExit (Collider collider) {
		if (!parentFollowKey.getBeingHeld ()) {
			Destroy (transform.parent.gameObject);
			Debug.Log ("Miss");
		}
	}

}

