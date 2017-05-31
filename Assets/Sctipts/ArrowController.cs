using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	private Arrow arrow;
	private float velocity;
	private Vector3 destination;
	private int nodeCounter;
	private LineRenderer lineRenderer;
	private float guideLineLength;
	private int keyCounter;
	private float traveledDistance;

	private void spawnKeys () {
		while (keyCounter < arrow.Keys.Count - 1 && arrow.Keys [keyCounter].SpawnTime <= Time.time + guideLineLength / velocity) {
			int keySpawnNodeCounter = 0;
			if (Vector3.Distance (transform.position, arrow.Nodes [keySpawnNodeCounter].Position.vector3 ()) > (arrow.Keys [keyCounter].SpawnTime - Time.time) * velocity) {
				
			}
		}
	}

	private void drawGuideLine () {
		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition (0, new Vector3 (transform.position.x, transform.position.y, transform.position.z + 1));
		int guideLineNodeCounter = nodeCounter;
		float currentGuideLineLength = 0;
		Vector3 currentNode = transform.position;
		while (guideLineNodeCounter < arrow.Nodes.Count - 1 && currentGuideLineLength + Vector3.Distance (currentNode, arrow.Nodes [guideLineNodeCounter].Position.vector3 ()) <= guideLineLength) {
			++lineRenderer.positionCount;
			Vector3 newNode = arrow.Nodes [guideLineNodeCounter].Position.vector3 ();
			lineRenderer.SetPosition (lineRenderer.positionCount - 1, new Vector3 (newNode.x, newNode.y, newNode.z + 1));
			currentGuideLineLength += Vector3.Distance (currentNode, arrow.Nodes [guideLineNodeCounter].Position.vector3 ());
			currentNode = arrow.Nodes [guideLineNodeCounter].Position.vector3 ();
			++guideLineNodeCounter;
		}
		if (currentGuideLineLength + Vector3.Distance (currentNode, arrow.Nodes [guideLineNodeCounter].Position.vector3 ()) <= guideLineLength) {
			++lineRenderer.positionCount;
			Vector3 newNode = arrow.Nodes [guideLineNodeCounter].Position.vector3 ();
			lineRenderer.SetPosition (lineRenderer.positionCount - 1, new Vector3 (newNode.x, newNode.y, newNode.z + 1));
			currentGuideLineLength += Vector3.Distance (currentNode, arrow.Nodes [guideLineNodeCounter].Position.vector3 ());
			currentNode = arrow.Nodes [guideLineNodeCounter].Position.vector3 ();
			++guideLineNodeCounter;
		}
		if (guideLineNodeCounter < arrow.Nodes.Count) {
			++lineRenderer.positionCount;
			lineRenderer.SetPosition (lineRenderer.positionCount - 1, (guideLineLength - currentGuideLineLength) * Vector3.Normalize (arrow.Nodes [guideLineNodeCounter].Position.vector3 () - currentNode) + currentNode);
		}
	}

	void Start () {
		nodeCounter = 1;
		setDestination (arrow.Nodes [nodeCounter].Position.vector3 ());
		lineRenderer = gameObject.GetComponent <LineRenderer> ();
		keyCounter = 0;
		traveledDistance = 0;
	}

	void Update () {
		//Move arrow and change to next destination when arrived at destination
		Vector3 nextPosition = transform.position + velocity * (destination - transform.position).normalized * Time.deltaTime;
		if (Vector3.Distance (nextPosition, destination) < Vector3.Distance (transform.position, destination)) {
			traveledDistance += Vector3.Distance (transform.position, nextPosition);
			transform.position = nextPosition;
			drawGuideLine ();
		} else {
			++nodeCounter;
			if (nodeCounter < arrow.Nodes.Count) {
				Debug.Log (Time.time);
				setDestination (arrow.Nodes [nodeCounter].Position.vector3 ());
			} else {
				gameObject.SetActive (false);
			}
		}
	}

	public void setArrow (Arrow newArrow) {
		arrow = newArrow;
	}

	public void setVelocity (float newVelocity) {
		velocity = newVelocity;
	}

	public void setDestination (Vector3 newDestination) {
		destination = newDestination;
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2((destination.y - transform.position.y), (destination.x - transform.position.x)) * Mathf.Rad2Deg - 90);
	}

	public void setGuideLineLength (float newGuideLineLength) {
		guideLineLength = newGuideLineLength;
	}

}
