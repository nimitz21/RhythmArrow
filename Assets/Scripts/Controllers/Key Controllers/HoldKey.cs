using UnityEngine;
using System.Collections.Generic;

public class HoldKey : KeySuperClass
{
	private bool beingHeld = false;
	private float availableGuideLineLength = 0;
	private float holdLineLength;
	private List<Vector3> followingNodes;
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
				beingHeldLineRenderer.drawLine (beingHeldLineLength, followingNodes, 1);
			}
		}
		availableGuideLineLength += ownerArrowController.velocity * Time.deltaTime;
		if (availableGuideLineLength < holdLineLength) {
			lineRenderer.drawLine (availableGuideLineLength, followingNodes, 2);
		} else {
			lineRenderer.drawLine (holdLineLength, followingNodes, 2);
		}
	}

	public void setHoldLineLength (float newHoldLineLength) {
		holdLineLength = newHoldLineLength;
	}

	public void setFollowingNodes (List<Vector3> newFollowingNodes) {
		followingNodes = newFollowingNodes;
	}

	public void setOwnerArrow (Transform newOwnerArrow) {
		ownerArrow = newOwnerArrow;
	}

	public bool getBeingHeld () {
		return beingHeld;
	}
		
	override public void unHold () {
		beingHeld = false;
	}

	override public void tap () {
		//do nothing
	}

	override public void hold () {
		if (perfect) {
			beingHeld = true;
		}
	}

}

