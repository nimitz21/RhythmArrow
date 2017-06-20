using UnityEngine;
using System.Collections;

public class followPerfect : perfectZone
{
	private FollowKey parentFollowKey;

	void Start () {
		parentFollowKey = GetComponentInParent <FollowKey> ();
	}

	protected override void OnTriggerExit (Collider collider)
	{
		//do nothing
	}

	protected override void OnTriggerStay (Collider collider)
	{
		if (!parentKey.perfect) {
			if (Time.time > parentKey.spawnTime - 0.1) { 
				if (collider.transform.tag == "Arrow") {
					parentKey.perfect = true;
				}
			}
		} else {
			if (parentFollowKey.getBeingHeld ()) {
				Destroy (transform.parent.gameObject);
				Debug.Log ("Perfect follow");
			}
		}
	}
}

