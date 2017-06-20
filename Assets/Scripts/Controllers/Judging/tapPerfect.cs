using UnityEngine;
using System.Collections;

public class tapPerfect : perfectZone
{

	protected override void OnTriggerExit (Collider collider) {
		if (parentKey.perfect) {
			parentKey.perfect = false;
		}
	}

}

