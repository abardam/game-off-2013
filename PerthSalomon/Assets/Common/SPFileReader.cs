using UnityEngine;
using System.Collections;
using System.Xml;

public abstract class SPFileReader : MonoBehaviour {

	public abstract string[] ReadGrid(string f);
	public abstract XmlDocument ReadXML(string f);

}

public class SPFileReaderManager {

	public static SPFileReader reader;

	public static string[] ReadGrid(string f){
		return reader.ReadGrid(f);
	}

	public static XmlDocument ReadXML(string f){
		return reader.ReadXML(f);
	}
}