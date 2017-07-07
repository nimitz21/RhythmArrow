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
	private float time;
	private float deltaTime;
	private bool paused;

	public GameObject arrowPrefab;
	public GameObject tapKeyPrefab;
	public GameObject holdKeyPrefab;
	public GameObject swipeKeyPrefab;
	public GameObject followKeyPrefab;

	private void spawnArrow (Vector3 position) {
		GameObject newArrow = Instantiate (arrowPrefab);
		newArrow.transform.position = position;
		ArrowController newArrowController = newArrow.GetComponent <ArrowController> ();
		newArrowController.arrow = chart.Arrows [arrowCounter];
		newArrowController.speed = chart.Bpm * chart.Arrows [arrowCounter].SpeedScale;
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
		time = -guideLineLength / (chart.Arrows [0].SpeedScale * chart.Bpm);
		paused = false;
	}

	void FixedUpdate () {
		if (!paused) {
			deltaTime = Time.deltaTime;
			time += deltaTime;
			if (arrowCounter < chart.Arrows.Count) {
				while (arrowCounter < chart.Arrows.Count - 1 && time + guideLineLength / (chart.Arrows [arrowCounter].SpeedScale * chart.Bpm) >= chart.Arrows [arrowCounter].SpawnTime) {
					spawnArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
				}
				if (time + guideLineLength / (chart.Arrows [arrowCounter].SpeedScale * chart.Bpm) >= chart.Arrows [arrowCounter].SpawnTime) {
					spawnArrow (chart.Arrows [arrowCounter].Nodes [0].Position.vector3 ());
				}
			}
		} else {
			deltaTime = 0;
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

	public float getTime () {
		return time;
	}

	public float getDeltaTime () {
		return deltaTime;
	}

	public bool getPaused () {
		return paused;
	}

	public void togglePause () {
		paused = !paused;
	}

}
