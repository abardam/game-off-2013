using UnityEngine;
using System.Collections;
using System.Xml;

public abstract class SPFileReader {

	public abstract string[] ReadLevel();
	public abstract XmlDocument ReadEvents();


}
