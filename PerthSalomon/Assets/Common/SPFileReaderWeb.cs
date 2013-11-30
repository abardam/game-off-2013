using UnityEngine;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;

public class SPFileReaderWeb : SPFileReader {

	private Hashtable name2file = new Hashtable();
	private string toGUI = "";

	public void Start(){
		SPFileReaderManager.reader = this;
	}

	IEnumerator fileOpen (string filename) {
		WWW download = new WWW(Application.dataPath + "/" + filename);
		toGUI = Application.dataPath + filename;
		yield return download;
		name2file[filename] = download.text;
		toGUI = download.text;
	}

	public override string[] ReadGrid (string f)
	{
		if(name2file[f] == null)
		{
			StartCoroutine(fileOpen (f));
			return null;
		}
		else
		{
			string[] result = Regex.Split((string)name2file[f], "\r\n|\r|\n");
			return result;
		}
	}

	public override System.Xml.XmlDocument ReadXML (string f)
	{
		if(name2file[f] == null)
		{
			StartCoroutine(fileOpen(f));
			return null;
		}

		XmlDocument xmlDoc = new XmlDocument();
		xmlDoc.LoadXml((string)name2file[f]);

		return xmlDoc;

	}

	public void OnGUI(){
		//GUI.Box(new Rect(0,0,Screen.width,Screen.height), toGUI);
	}
}
