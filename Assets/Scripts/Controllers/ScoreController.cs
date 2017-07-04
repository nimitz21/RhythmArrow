using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{

	private static ScoreController instance;

	private int score;

	[SerializeField]
	private Text displayedScore;

	void Start () {
		instance = this;
		score = 0;
	}

	void Update () {
		int temp = score;
		int divisor = 1000000000;
		displayedScore.text = "";
		while (divisor != 0) {
			displayedScore.text += temp / divisor;
			temp %= divisor;
			divisor /= 10;
		}
	}

	public void addPerfectScore () {
		score += 200;
	}

	public void addGoodScore () {
		score += 100;
	}

	public void addHoldScore () {
		score += 5;
	}

	public static ScoreController getInstance () {
		return instance;
	}

}

