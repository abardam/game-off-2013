using UnityEngine;
using System.Collections;
using System.Xml;

public class LevelLoader : MonoBehaviour
{ 
	public GridSpawner gridSpawner;
	public EventManager eventManager;
	public string levelName;
	private bool levelLoaded;

	private bool LoadLevel(){
		XmlDocument xml = SPFileReaderManager.ReadXML("levels.xml");
		if(xml == null) return false;

		SPLevel level = new SPLevel();


		XmlNodeList levelsNode = xml.SelectNodes("//Levels/Level");
		foreach(XmlNode levelNode in levelsNode){
			string lName = levelNode.Attributes["name"].Value;

			if(lName == levelName){

				level.Name = lName;
				level.EventsFilename = levelNode.Attributes["events"].Value;
				level.GridFilename = levelNode.Attributes["grid"].Value;
				GameState.GetInstance().LevelName = lName;

				break;
			}
		}

		gridSpawner.GridFilename = level.GridFilename;
		eventManager.EventsFilename = level.EventsFilename;

		return true;
	}

	void Start(){
		if(GameState.GetInstance().LevelName != "") levelName = GameState.GetInstance().LevelName;
		levelLoaded = false;
	}

	void Update(){
		if(!levelLoaded){

			levelLoaded = LoadLevel();
		}
	}

	public void TryLoadLevel ()
	{
		levelLoaded = false;
	}
}
