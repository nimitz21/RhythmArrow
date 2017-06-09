using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LnRenderer : MonoBehaviour {

	private LineRenderer lineRenderer;
	private int nodeCounter;
	private float currentLength;
	private Vector3 currentNode;
	private List<Vector3> nodes;
	private float depth;

	void Start () {
		lineRenderer = transform.GetComponent <LineRenderer> ();
	}

	private void addPosition () {
		++lineRenderer.positionCount;
		Vector3 newNode = nodes [nodeCounter];
		lineRenderer.SetPosition (lineRenderer.positionCount - 1, new Vector3 (newNode.x, newNode.y, newNode.z + depth));
		currentLength += Vector3.Distance (currentNode, nodes [nodeCounter]);
		currentNode = nodes [nodeCounter];
		++nodeCounter;
	}

	public void drawLine (float length, List<Vector3> newNodes, float newDepth) {
		lineRenderer.positionCount = 1;
		lineRenderer.SetPosition (0, new Vector3 (transform.position.x, transform.position.y, transform.position.z + depth));
		nodeCounter = 0;
		currentLength = 0;
		currentNode = transform.position;
		nodes = newNodes;
		depth = newDepth;
		while (nodeCounter < nodes.Count - 1 && currentLength + Vector3.Distance (currentNode, nodes [nodeCounter]) <= length) {
			addPosition ();
		}
		if (currentLength + Vector3.Distance (currentNode, nodes [nodeCounter]) <= length) {
			addPosition ();
		}
		if (nodeCounter < nodes.Count) {
			++lineRenderer.positionCount;
			lineRenderer.SetPosition (lineRenderer.positionCount - 1, (length - currentLength) * Vector3.Normalize (nodes [nodeCounter] - currentNode) + currentNode + Vector3.forward * depth);
		}
	}

	public void initialize () {
		Start ();
	}

}
