using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour {

	private Vector3 destination;
	private int nodeCounter;
	private LnRenderer lineRenderer;
	private int keyCounter;
	private int traveledNode;

	public Arrow arrow { get; set; }
	public float velocity { get; set; }

	private GameObject getTapKeyInstance () { 
		return Instantiate (GameController.getInstance ().tapKeyPrefab);
	}

	private GameObject getHoldKeyInstance (int keySpawnNodeCounter, float keySpawnDistance) {
		GameObject newKey = Instantiate (GameController.getInstance ().holdKeyPrefab);
		HoldKey holdKey = newKey.GetComponent <HoldKey> ();
		holdKey.setHoldLineLength (arrow.Keys [keyCounter].Duration * velocity);
		holdKey.setFollowingNodes (arrow.nodesToVector3 ().GetRange (keySpawnNodeCounter, arrow.Nodes.Count - keySpawnNodeCounter));
		holdKey.setOwnerArrow (transform);
		return newKey;
	}

	private GameObject getSwipeKeyInstance () {
		GameObject newKey = Instantiate (GameController.getInstance ().swipeKeyPrefab);
		switch (arrow.Keys [keyCounter].Direction) {
		case "u":
			newKey.GetComponent <SwipeKey> ().setRotation (0);
			break;
		case "ul":
			newKey.GetComponent <SwipeKey> ().setRotation (45);
			break;
		case "l":
			newKey.GetComponent <SwipeKey> ().setRotation (90);
			break;
		case "dl":
			newKey.GetComponent <SwipeKey> ().setRotation (135);
			break;
		case "d":
			newKey.GetComponent <SwipeKey> ().setRotation (180);
			break;
		case "dr":
			newKey.GetComponent <SwipeKey> ().setRotation (225);
			break;
		case "r":
			newKey.GetComponent <SwipeKey> ().setRotation (270);
			break;
		case "ur":
			newKey.GetComponent <SwipeKey> ().setRotation (315);
			break;
		}
		return newKey; 
	}

	private GameObject getFollowKeyInstance (int keySpawnNodeCounter) {
		GameObject newkey = Instantiate (GameController.getInstance ().followKeyPrefab);
		FollowKey followKey = newkey.GetComponent <FollowKey> ();
		followKey.setChildrenSpawnTime (arrow.Keys [keyCounter].getChildrenSpawnTimes ());
		followKey.setOwnerArrow (transform);
		followKey.setParentSpawnNode (keySpawnNodeCounter);
		return newkey;
	}

	private void instantiateKey (int keySpawnNodeCounter, Vector3 positionBeforeSpawnNode, float totalDistanceFromArrow, float keySpawnDistance) {
		GameObject newKey = null;
		switch (arrow.Keys [keyCounter].Type) {
		case "tap": 
			newKey = getTapKeyInstance ();
			break;
		case "hold":
			newKey = getHoldKeyInstance (keySpawnNodeCounter, keySpawnDistance);
			break;
		case "swipe":
			newKey = getSwipeKeyInstance ();
			break;
		case "follow":
			newKey = getFollowKeyInstance (keySpawnNodeCounter);
			break;
		}
		newKey.transform.position = (keySpawnDistance - totalDistanceFromArrow) * Vector3.Normalize (arrow.Nodes [keySpawnNodeCounter].Position.vector3 () - positionBeforeSpawnNode) + positionBeforeSpawnNode;
		newKey.GetComponent <KeySuperClass> ().spawnTime = arrow.Keys [keyCounter].SpawnTime;
	}

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
		instantiateKey (keySpawnNodeCounter, positionBeforeSpawnNode, totalDistanceFromArrow, keySpawnDistance);
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
		lineRenderer = gameObject.GetComponent <LnRenderer> ();
		keyCounter = 0;
		traveledNode = 1;
	}

	void FixedUpdate () {
		//Move arrow and change to next destination when arrived at destination
		//Debug.Log ("Time " + Time.time);
		Vector3 nextPosition = transform.position + velocity * (destination - transform.position).normalized * Time.deltaTime;
		if (Vector3.Distance (nextPosition, destination) < Vector3.Distance (transform.position, destination)) {
			transform.position = nextPosition;
			lineRenderer.drawLine (GameController.getInstance ().getGuideLineLength (), arrow.nodesToVector3 (). GetRange(nodeCounter, arrow.Nodes.Count - nodeCounter), 3);
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

	public void setDestination (Vector3 newDestination) {
		destination = newDestination;
		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2((destination.y - transform.position.y), (destination.x - transform.position.x)) * Mathf.Rad2Deg - 90);
	}

	public int getTraveledNode () {
		return traveledNode;
	}

}
