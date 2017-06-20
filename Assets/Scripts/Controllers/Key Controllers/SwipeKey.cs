using UnityEngine;
using System.Collections;

public class SwipeKey : KeySuperClass
{

	private float rotation;
	private bool isSwiped = false;
	private bool beingSwiped = false;

	void Update ()
	{
		if (isSwiped) {
			if (!beingSwiped) {
				//should change this part after transitioning to touch input
				Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				float angle = (Vector3.Cross (Vector3.up, mousePositionInWorld - transform.position).z < 0 ? -1 : 1) * Vector2.Angle (mousePositionInWorld - transform.position, Vector3.up);
				if (angle < 0) {
					angle += 360;
				}
				float offSet = Mathf.Abs (angle - rotation);
				if (offSet < 22.5 || offSet > 337.5) {
					Destroy (gameObject);
					Debug.Log ("Perfect");
				} else {
					Destroy (gameObject);
					Debug.Log ("Miss swipe");
				}
			}
		}
	}

	public void setRotation (float newRotation) {
		rotation = newRotation;
		transform.GetChild (2).Rotate (0, 0, rotation);
	}
	public bool getIsSwiped () {
		return isSwiped;
	}

	override public void tap () {
		if (good || perfect) {
			isSwiped = true;
			beingSwiped = true;
		} else {
			Destroy (gameObject);
			Debug.Log ("Bad");
		}
	}

	override public void hold () {
		//do nothing
	}

	override public void unHold () {
		beingSwiped = false;
	}
		
}

