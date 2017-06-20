using UnityEngine;
using System.Collections;

public abstract class judgingZones : MonoBehaviour
{

	protected KeySuperClass parentKey; 

	void Awake () {
		parentKey = GetComponentInParent <KeySuperClass> ();
	}

	protected abstract void OnTriggerExit (Collider collider);

	protected abstract void OnTriggerStay (Collider collider);

}

