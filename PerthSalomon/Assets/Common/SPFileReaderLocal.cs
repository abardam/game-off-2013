using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SPFileReaderLocal : SPFileReader {

	public override string[] ReadLevel(){
		return File.ReadAllLines("Book1.csv");
	}

	public override XmlDocument ReadEvents ()
	{
		XmlDocument xml = new XmlDocument();
		xml.Load("Events.xml");

		return xml;
	}
}
