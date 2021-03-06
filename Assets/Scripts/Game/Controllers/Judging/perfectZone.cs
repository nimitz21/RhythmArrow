﻿using UnityEngine;
using System.Collections;

public abstract class perfectZone : judgingZones
{

	override protected abstract void OnTriggerExit (Collider collider);

	override protected void OnTriggerStay (Collider collider) {
		if (!parentKey.perfect) {
			if (GameController.getInstance ().getTime () > parentKey.spawnTime - 0.1) { 
				if (collider.transform.tag == "Arrow") {
					parentKey.perfect = true;
				}
			}
		}
	}
}

