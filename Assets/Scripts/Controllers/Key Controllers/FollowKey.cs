using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class FollowKey : KeySuperClass
{

	private bool beingHeld = false;
	private List<float> childrenSpawnTime;
	private bool childSpawned = false;
	private Transform ownerArrow;
	private ArrowController ownerArrowController;
	private int parentSpawnNode;
	private LnRenderer lineRenderer;
	private float connectorLineLength;

	private void spawnChild () {
		int childSpawnNodeCounter = parentSpawnNode;
		Vector3 positionBeforeSpawnNode = transform.position;
		float totalDistanceFromParent = 0;
		float distanceToNextNode = Vector3.Distance (positionBeforeSpawnNode, ownerArrowController.arrow.Nodes [childSpawnNodeCounter].Position.vector3 ());
		float childSpawnDistance = (childrenSpawnTime [0] - spawnTime) * ownerArrowController.speed;
		while (totalDistanceFromParent + distanceToNextNode < childSpawnDistance) {
			totalDistanceFromParent += distanceToNextNode;
			positionBeforeSpawnNode = ownerArrowController.arrow.Nodes [childSpawnNodeCounter].Position.vector3 ();
			++childSpawnNodeCounter;
			distanceToNextNode = Vector3.Distance (positionBeforeSpawnNode, ownerArrowController.arrow.Nodes [childSpawnNodeCounter].Position.vector3 ());
		}
		GameObject newChild = Instantiate (GameController.getInstance ().followKeyPrefab);
		FollowKey childScript = newChild.GetComponent <FollowKey> ();
		childScript.spawnTime = childrenSpawnTime [0];
		if (childrenSpawnTime.Count > 0) {
 			childScript.setChildrenSpawnTime (childrenSpawnTime.GetRange (1, childrenSpawnTime.Count - 1));
		} else {
			childScript.setChildrenSpawnTime (new List<float> ());
		}
		childScript.setOwnerArrow (ownerArrow);
		childScript.parentSpawnNode = childSpawnNodeCounter;
		newChild.transform.position = (childSpawnDistance - totalDistanceFromParent) * 
			Vector3.Normalize (ownerArrowController.arrow.Nodes [childSpawnNodeCounter].Position.vector3 () - positionBeforeSpawnNode) + positionBeforeSpawnNode;
		childSpawned = true;
	}
		
	void Start () {
		lineRenderer = GetComponent <LnRenderer> ();
		connectorLineLength = GameController.getInstance ().getDeltaTime () * ownerArrowController.speed;
	}

	void FixedUpdate ()
	{
		if (childrenSpawnTime.Count > 0) {
			if (!childSpawned) {
				connectorLineLength += GameController.getInstance ().getDeltaTime () * ownerArrowController.speed;
				lineRenderer.drawLine (connectorLineLength, ownerArrowController.arrow.nodesToVector3 ().GetRange (parentSpawnNode, ownerArrowController.arrow.Nodes.Count - parentSpawnNode), 2);
				if (childrenSpawnTime [0] <= GameController.getInstance ().getTime () + GameController.getInstance ().getGuideLineLength () / ownerArrowController.speed) {
					spawnChild ();
				}
			}
		}
	}

	public void setChildrenSpawnTime (List<float> newChildrenSpawnTime) {
		childrenSpawnTime = newChildrenSpawnTime;
	}

	public void setOwnerArrow (Transform newOwnerArrow) {
		ownerArrow = newOwnerArrow;
		ownerArrowController = ownerArrow.GetComponent <ArrowController> ();
	}

	public void setParentSpawnNode (int newParentSpawnNode) {
		parentSpawnNode = newParentSpawnNode;
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
		beingHeld = true;
	}
		
}


