using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private Chart chart;
	private List<GameObject> arrows;
	private int arrowCounter;
	private float guideLineLength;

	public GameObject arrowPrefab;

	private void addArrow (Vector3 position) {
		GameObject newArrow = Instantiate (arrowPrefab);
		newArrow.transform.position = position;
		ArrowController newArrowController = newArrow.GetComponent <ArrowController> ();
		newArrowController.setArrow (chart.Arrows [arrowCounter]);
		newArrowController.setVelocity (chart.Bpm * 3);
		newArrowController.setGuideLineLength (guideLineLength);
		arrows.Add (newArrow);
	}

	void Start () {
		chart = Chart.Load (Application.dataPath + "//Charts//testChartMetaData.xml");
		Debug.Log (chart.Title);
		Debug.Log (chart.Bpm);
		arrows = new List<GameObject> ();
		arrowCounter = 0;
		guideLineLength = 1000;
	}

	void Update () {
		while (arrowCounter < chart.Arrows.Count - 1 && Time.time >= chart.Arrows [arrowCounter].SpawnTime) {
			addArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
			++arrowCounter;
		}
		if (Time.time >= chart.Arrows [arrowCounter].SpawnTime) {
			addArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
			gameObject.SetActive (false);;
		}
	}

}
