using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class SPFileReaderLocal : SPFileReader {

	public override string[] ReadGrid(string filename){
		return File.ReadAllLines(filename);
	}

	public override XmlDocument ReadXML (string filename)
	{
		XmlDocument xml = new XmlDocument();
		xml.Load(filename);

		return xml;
	}

}
