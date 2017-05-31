using System.Collections.Generic;
using System.Xml.Serialization;

public class Arrow
{
	public float SpawnTime;

	[XmlArray("Nodes")]
	[XmlArrayItem("Node")]
	public List<Node> Nodes = new List<Node>();
}

