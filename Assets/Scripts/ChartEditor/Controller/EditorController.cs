using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditorController : MonoBehaviour {

	private static EditorController instance;

	[SerializeField]
	private Scrollbar scrollBar;
	[SerializeField]
	private Text displayedTime;
	[SerializeField]
	private InputField inputField;

	private float time;
	private Chart chart;
	private SortedList gameObjects;

	private void updateDisplayedTime () {
		int minutes = (int)time / 60;
		int seconds = (int)time % 60;
		int miliSeconds = (int)(time * 100) % 100;
		displayedTime.text = "" + minutes / 10 + "" + minutes % 10 + " : " + seconds / 10 + "" + seconds % 10 + " : " + "" + miliSeconds / 10 + "" + miliSeconds % 10;
	}

	void Start () {
		time = 0;
		chart = Chart.Load (Application.dataPath + "//Charts//testChartMetaData.xml");
		instance = this;
		if (chart == null) {
			chart = new Chart ();
		}
	}

	void Update () {
		updateDisplayedTime ();
	}

	public static EditorController getInstance () {
		return instance;
	}

	public float getTime () {
		return time;
	}

	public void changeTime () {
		time = scrollBar.value * chart.Length;
	}

	public void openTimeInputField () {
		inputField.gameObject.SetActive (true);
		inputField.text = "" + displayedTime.text[0] + "" + displayedTime.text[1] + ":" + 
			displayedTime.text[5] + "" + displayedTime.text[6] + ":" + 
			displayedTime.text[10] + "" + displayedTime.text [11];
	}

	public void doneEdittingTimeField () {
		time = ((float)(((inputField.text [0] - 48) * 10 + (inputField.text [1] - 48)) * 60 +
			(inputField.text [3] - 48) * 10 + (inputField.text [4] - 48)) * 100 +
			(inputField.text [6] - 48) * 10 + (inputField.text [7] - 48)) / 100; 
		Debug.Log (time);
		scrollBar.value = time / chart.Length;
		inputField.gameObject.SetActive (false);
	}
}
