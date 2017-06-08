using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	private static GameController instance;

	private Chart chart;
	private List<GameObject> arrows;
	private int arrowCounter;
	private float guideLineLength;
	private int globalKeyCounter;

	public GameObject arrowPrefab;
	public GameObject tapKeyPrefab;
	public GameObject holdKeyPrefab;

	private void addArrow (Vector3 position) {
		GameObject newArrow = Instantiate (arrowPrefab);
		newArrow.transform.position = position;
		ArrowController newArrowController = newArrow.GetComponent <ArrowController> ();
		newArrowController.arrow = chart.Arrows [arrowCounter];
		newArrowController.arrowId = arrowCounter;
		newArrowController.velocity = chart.Bpm * 3;
		arrows.Add (newArrow);
		++arrowCounter;
	}

	void Start () {
		instance = this;
		chart = Chart.Load (Application.dataPath + "//Charts//testChartMetaData.xml");
		Debug.Log (chart.Title);
		Debug.Log (chart.Bpm);
		arrows = new List<GameObject> ();
		arrowCounter = 0;
		guideLineLength = 500;
	}

	void FixedUpdate () {
		//Spawn arrow when in time
		if (arrowCounter < chart.Arrows.Count) {
			while (arrowCounter < chart.Arrows.Count - 1 && Time.time >= chart.Arrows [arrowCounter].SpawnTime) {
				addArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
			}
			if (Time.time >= chart.Arrows [arrowCounter].SpawnTime) {
				addArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
				//gameObject.SetActive (false);;
			}
		}
	}

	public static GameController getInstance () {
		return instance;
	}

	public float getGuideLineLength () {
		return guideLineLength;
	}

	public int getGlobalKeyCounter () {
		return globalKeyCounter;
	}

	public void incrementGlobalKeyCounter () {
		++globalKeyCounter;
	}

}
