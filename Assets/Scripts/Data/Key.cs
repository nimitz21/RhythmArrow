using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Key
{
	
	public float SpawnTime;
	public string Type;
	public float Duration;
	public string Direction;
	public List<Child> Children; 

	public List<float> getChildrenSpawnTimes () {
		List<float> childrenSpawnTime = new List<float> ();
		foreach (Child child in Children) {
			childrenSpawnTime.Add (child.SpawnTime);
		}
		return childrenSpawnTime;
	}

}

