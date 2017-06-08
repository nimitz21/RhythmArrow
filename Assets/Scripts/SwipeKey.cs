using UnityEngine;
using System.Collections;

public class SwipeKey : KeySuperClass
{

	private float rotation;
	private bool clicked = false;

	void OnTriggerExit (Collider collider) {
		if (hit) {
			Destroy (gameObject);
			Debug.Log ("Miss " + keyId);
		}
	}

	void Update ()
	{
		if (clicked) {
			Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			float angle = (Vector3.Cross (Vector3.up, mousePositionInWorld - transform.position).z < 0 ? -1 : 1) * Vector2.Angle (mousePositionInWorld - transform.position, Vector3.up);
			if (angle < 0) {
				angle += 360;
			}
			if (angle - rotation < 45 || angle - rotation > 325) {
				Destroy (gameObject);
				Debug.Log ("Perfect " + keyId);
			}
		}
	}

	override public void activate () {
		clicked = true;
		if (!hit) {
			Destroy (gameObject);
			Debug.Log ("Bad " + keyId);
		}
	}

	public void setRotation (float newRotation) {
		rotation = newRotation;
		transform.GetChild (0).Rotate (0, 0, rotation);
	}
}

