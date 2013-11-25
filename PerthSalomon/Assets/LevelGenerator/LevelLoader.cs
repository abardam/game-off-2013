using UnityEngine;
using System.Collections;
using System.Xml;

public class LevelLoader : MonoBehaviour
{ 
	public GridSpawner gridSpawner;
	public EventManager eventManager;
	public string levelName;

	public void LoadLevel(){
		SPFileReader reader = new SPFileReaderLocal();
		XmlDocument xml = reader.ReadXML("levels.xml");

		SPLevel level = new SPLevel();


		XmlNodeList levelsNode = xml.SelectNodes("//Levels/Level");
		foreach(XmlNode levelNode in levelsNode){
			string lName = levelNode.Attributes["name"].Value;

			if(lName == levelName){

				level.Name = lName;
				level.EventsFilename = levelNode.Attributes["events"].Value;
				level.GridFilename = levelNode.Attributes["grid"].Value;

				break;
			}
		}

		gridSpawner.GridFilename = level.GridFilename;
		eventManager.EventsFilename = level.EventsFilename;


	}

	void Start(){
		LoadLevel();
	}
}
