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
	private int traveledNode;

	private void spawnKey () {
		int keySpawnNodeCounter = traveledNode;
		Vector3 positionBeforeSpawnNode = transform.position;
		float totalDistanceFromArrow = 0;
		float distanceToNextNode = Vector3.Distance (positionBeforeSpawnNode, arrow.Nodes [keySpawnNodeCounter].Position.vector3 ());
		float keySpawnDistance = (arrow.Keys [keyCounter].SpawnTime - Time.time) * velocity;
		while (totalDistanceFromArrow + distanceToNextNode < keySpawnDistance) {
			totalDistanceFromArrow += distanceToNextNode;
			positionBeforeSpawnNode = arrow.Nodes [keySpawnNodeCounter].Position.vector3 ();
			++keySpawnNodeCounter;
			distanceToNextNode = Vector3.Distance (positionBeforeSpawnNode, arrow.Nodes [keySpawnNodeCounter].Position.vector3 ());
		}
		GameObject newTapKey = Instantiate (GameController.getInstance ().tapKeyPrefab);
		newTapKey.transform.position = (keySpawnDistance - totalDistanceFromArrow) * Vector3.Normalize (arrow.Nodes [keySpawnNodeCounter].Position.vector3 () - positionBeforeSpawnNode) + positionBeforeSpawnNode;
		newTapKey.GetComponent <TapKey> ().setKeyId (GameController.getInstance ().getGlobalKeyCounter ());
		GameController.getInstance ().incrementGlobalKeyCounter ();
		++keyCounter;
	}

	private void spawnKeys () {
		if (keyCounter < arrow.Keys.Count) {
			while (keyCounter < arrow.Keys.Count - 1 && arrow.Keys [keyCounter].SpawnTime <= Time.time + guideLineLength / velocity) {
				spawnKey ();
			}
			if (arrow.Keys [keyCounter].SpawnTime <= Time.time + guideLineLength / velocity) {
				spawnKey ();
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
		traveledNode = 1;
	}

	void FixedUpdate () {
		//Move arrow and change to next destination when arrived at destination
		//Debug.Log ("Time " + Time.time);
		Vector3 nextPosition = transform.position + velocity * (destination - transform.position).normalized * Time.deltaTime;
		if (Vector3.Distance (nextPosition, destination) < Vector3.Distance (transform.position, destination)) {
			transform.position = nextPosition;
			drawGuideLine ();
			spawnKeys ();
		} else {
			++nodeCounter;
			++traveledNode;
			if (nodeCounter < arrow.Nodes.Count) {
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

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0))  {
			Debug.Log ("clicked arrow instead");
		}
	}

}
