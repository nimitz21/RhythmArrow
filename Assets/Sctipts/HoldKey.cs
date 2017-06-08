using UnityEngine;
using System.Collections.Generic;

public class HoldKey : KeySuperClass
{
	private bool held = false;
	private float availableGuideLineLength;
	private float holdLineLength;
	private List<Vector3> nodesAfter;
	private Transform ownerArrow;
	private ArrowController ownerArrowController;
	private LnRenderer lineRenderer;
	private float heldLineLength = 0;
	private LnRenderer heldLineRenderer;

	void OnTriggerExit (Collider collider) {
		if (!held) {
			Destroy (gameObject);
			Debug.Log ("Miss " + keyId);
		}
		hit = false;
	}

	void Start () {
		ownerArrowController = ownerArrow.GetComponent <ArrowController> ();
		lineRenderer = transform.GetComponent <LnRenderer> ();
		heldLineRenderer = transform.GetChild (0).GetComponent <LnRenderer> ();
	}

	void FixedUpdate () {
		if (held) {
			if (!Input.GetMouseButton (0)) {
				Destroy (gameObject);
				Debug.Log ("Miss " + keyId);
			} else {
				heldLineLength += ownerArrowController.velocity * Time.deltaTime;
				if (heldLineLength > holdLineLength) {
					Destroy (gameObject);
					Debug.Log ("Perfect " + keyId);
				} else {
					heldLineRenderer.drawLine (heldLineLength, nodesAfter, 1);
				}
			}
		}
		availableGuideLineLength += ownerArrowController.velocity * Time.deltaTime;
		if (availableGuideLineLength < holdLineLength) {
			lineRenderer.drawLine (availableGuideLineLength, nodesAfter, 2);
		} else {
			lineRenderer.drawLine (holdLineLength, nodesAfter, 2);
		}
	}

	override public void activate () {
		held = true;
	}

	public void setAvailableGuideLineLength (float newAvailableGuideLineLength) {
		availableGuideLineLength = newAvailableGuideLineLength;
	}

	public void setHoldLineLength (float newHoldLineLength) {
		holdLineLength = newHoldLineLength;
	}

	public void setNodesAfter (List<Vector3> newNodesAfter) {
		nodesAfter = newNodesAfter;
	}

	public void setOwnerArrow (Transform newOwnerArrow) {
		ownerArrow = newOwnerArrow;
	}

}

