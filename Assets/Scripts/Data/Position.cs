using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class Position
{
	public float Absis;

	public float Ordinate;

	public Vector3 vector3 () {
		return Camera.main.ScreenToWorldPoint(new Vector3 (Absis / 1000 * Screen.height  * 4 / 3 + (Screen.width - Screen.height * 4 / 3) / 2, Ordinate / 1000 * Screen.height, -Camera.main.transform.position.z));
	}
}
	

