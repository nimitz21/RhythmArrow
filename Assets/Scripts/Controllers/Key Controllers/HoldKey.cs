using UnityEngine;
using System.Collections.Generic;

public class HoldKey : KeySuperClass
{
	private bool beingHeld = false;
	private float availableGuideLineLength;
	private float holdLineLength;
	private List<Vector3> nodesAfter;
	private Transform ownerArrow;
	private ArrowController ownerArrowController;
	private LnRenderer lineRenderer;
	private float beingHeldLineLength = 0;
	private LnRenderer beingHeldLineRenderer;

	void Start () {
		ownerArrowController = ownerArrow.GetComponent <ArrowController> ();
		lineRenderer = transform.GetComponent <LnRenderer> ();
		beingHeldLineRenderer = transform.GetChild (0).GetComponent <LnRenderer> ();
	}

	void FixedUpdate () {
		if (beingHeld) {
			beingHeldLineLength += ownerArrowController.velocity * Time.deltaTime;
			if (beingHeldLineLength > holdLineLength) {
				Destroy (gameObject);
				Debug.Log ("Perfect");
			} else {
				beingHeldLineRenderer.drawLine (beingHeldLineLength, nodesAfter, 1);
			}
		}
		availableGuideLineLength += ownerArrowController.velocity * Time.deltaTime;
		if (availableGuideLineLength < holdLineLength) {
			lineRenderer.drawLine (availableGuideLineLength, nodesAfter, 2);
		} else {
			lineRenderer.drawLine (holdLineLength, nodesAfter, 2);
		}
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


	override public void unHold () {
		beingHeld = false;
	}

	override public void tap () {
		//do nothing
	}

	override public void hold () {
		if (hit) {
			beingHeld = true;
		}
	}

	protected override void OnTriggerExit (Collider collider) {
		if (!beingHeld) {
			Destroy (gameObject);
			Debug.Log ("Miss");
		}
	}

}

