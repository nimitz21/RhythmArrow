using UnityEngine;
using System.Collections;

public class holdPerfect : perfectZone
{

	protected override void OnTriggerExit (Collider collider)
	{
		parentKey.perfect = false;
	}

}

