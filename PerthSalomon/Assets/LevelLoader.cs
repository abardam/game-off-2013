using UnityEngine;
using System.Collections;
using System.Xml;

public class Level{ 

	private static SPLevel LoadLevel(string filename, string levelName){
		XmlDocument xml = SPFileReaderManager.ReadXML(filename);

		if(xml == null) return null;

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

		return level;
	}
}
