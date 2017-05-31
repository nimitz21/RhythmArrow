using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot("Chart")]
public class Chart
{
	public string Title;

	public float Bpm;

	[XmlArray("Arrows")]
	[XmlArrayItem("Arrow")]
	public List<Arrow> Arrows = new List <Arrow> ();

	public static Chart Load(string path) {
		var serializer = new XmlSerializer (typeof (Chart));
		using (var stream = new FileStream (path, FileMode.Open)) {
			return serializer.Deserialize (stream) as Chart;
		}
	}
}

