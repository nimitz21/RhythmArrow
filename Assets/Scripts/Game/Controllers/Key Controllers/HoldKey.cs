using UnityEngine;
using System.Collections.Generic;

public class HoldKey : KeySuperClass
{
	private bool beingHeld = false;
	private float availableGuideLineLength;
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
		beingHeldLineRenderer = transform.GetChild (2).GetComponent <LnRenderer> ();
		availableGuideLineLength = ownerArrowController.speed * GameController.getInstance ().getDeltaTime ();
	}

	void FixedUpdate () {
		if (beingHeld) {
			ScoreController.getInstance ().addHoldScore ();
			beingHeldLineLength += ownerArrowController.speed * GameController.getInstance ().getDeltaTime ();
			if (beingHeldLineLength > holdLineLength) {
				Destroy (gameObject);
				Debug.Log ("Perfect");
			} else {
				beingHeldLineRenderer.drawLine (beingHeldLineLength, followingNodes, 1);
			}
		} else {
			if (beingHeldLineLength > 0) {
				Destroy (gameObject);
				Debug.Log ("Good");
			}
		}
		availableGuideLineLength += ownerArrowController.speed * GameController.getInstance ().getDeltaTime ();
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

