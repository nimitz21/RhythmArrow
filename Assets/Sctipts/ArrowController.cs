using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	private Vector3 destination;
	private int nodeCounter;
	private LineRenderer lineRenderer;
	private int keyCounter;
	private int traveledNode;

	public Arrow arrow { get; set; }
	public int arrowId { get; set; }
	public float velocity { get; set; }

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
		GameObject newKey = null;
		if (arrow.Keys [keyCounter].Type == "tap") {
			newKey = Instantiate (GameController.getInstance ().tapKeyPrefab);
		} else if (arrow.Keys [keyCounter].Type == "hold") {
			newKey = Instantiate (GameController.getInstance ().holdKeyPrefab);
			HoldKey holdKey = newKey.GetComponent <HoldKey> ();
			holdKey.setAvailableGuideLineLength (GameController.getInstance ().getGuideLineLength () - keySpawnDistance);
			holdKey.setHoldLineLength (arrow.Keys [keyCounter].Duration * velocity);
			holdKey.setNodesAfter (arrow.nodesToVector3 ().GetRange (keySpawnNodeCounter, arrow.Nodes.Count - keySpawnNodeCounter));
			holdKey.setOwnerArrow (transform);
		}
		newKey.transform.position = (keySpawnDistance - totalDistanceFromArrow) * Vector3.Normalize (arrow.Nodes [keySpawnNodeCounter].Position.vector3 () - positionBeforeSpawnNode) + positionBeforeSpawnNode;
		KeySuperClass newKeySuperClass = newKey.GetComponent <KeySuperClass> ();
		newKeySuperClass.setKeyId (GameController.getInstance ().getGlobalKeyCounter ());
		GameController.getInstance ().incrementGlobalKeyCounter ();
		newKeySuperClass.setOwnerArrowId (arrowId);
		++keyCounter;
	}

	private void spawnKeys () {
		if (keyCounter < arrow.Keys.Count) {
			while (keyCounter < arrow.Keys.Count - 1 && arrow.Keys [keyCounter].SpawnTime <= Time.time + GameController.getInstance ().getGuideLineLength () / velocity) {
				spawnKey ();
			}
			if (arrow.Keys [keyCounter].SpawnTime <= Time.time + GameController.getInstance ().getGuideLineLength () / velocity) {
				spawnKey ();
			}
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
			GetComponent <LnRenderer> ().drawLine (GameController.getInstance ().getGuideLineLength (), arrow.nodesToVector3 (). GetRange(nodeCounter, arrow.Nodes.Count - nodeCounter), 3);
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

	public void setVelocity (float newVelocity) {
		velocity = newVelocity;
	}

	public void setDestination (Vector3 newDestination) {
		destination = newDestination;
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2((destination.y - transform.position.y), (destination.x - transform.position.x)) * Mathf.Rad2Deg - 90);
	}

}
