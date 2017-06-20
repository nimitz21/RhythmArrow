using UnityEngine;
using System.Collections;

public class swipeGood : goodZone
{
	private SwipeKey parentSwipeKey;

	void Start () {
		parentSwipeKey = GetComponentInParent <SwipeKey> ();
	}


	protected override void OnTriggerExit (Collider collider) {
		if (parentKey.good && !parentSwipeKey.getIsSwiped ()) {
			Destroy (transform.parent.gameObject);
			Debug.Log ("Miss");
		}
	}

}

