using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Arrow
{
	public float SpawnTime;

	[XmlArray("Nodes")]
	[XmlArrayItem("Node")]
	public List<Node> Nodes = new List <Node> ();

	[XmlArray("Keys")]
	[XmlArrayItem("Key")]
	public List<Key> Keys = new List <Key> ();

	public List<Vector3> nodesToVector3 () {
		List<Vector3> result = new List<Vector3> ();
		foreach (Node node in Nodes) {
			result.Add (node.Position.vector3 ());
		}
		return result;
	}
}

