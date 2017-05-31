using System.Xml;
using System.Xml.Serialization;
using UnityEngine;

public class Position
{
	public float Absis;

	public float Ordinate;

	public Vector3 vector3() {
		return new Vector3 (Absis, Ordinate, 0);
	}
}
	

