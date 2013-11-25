using UnityEngine;
using System.Collections;
using System.Xml;

public class Level{ 

	public static SPLevel LoadLevel(string filename, string levelName){
		SPFileReader reader = new SPFileReaderLocal();
		XmlDocument xml = reader.ReadXML(filename);

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
