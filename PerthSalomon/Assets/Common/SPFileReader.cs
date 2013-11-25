using UnityEngine;
using System.Collections;
using System.Xml;

public abstract class SPFileReader {

	public abstract string[] ReadGrid(string f);
	public abstract XmlDocument ReadXML(string f);

}
