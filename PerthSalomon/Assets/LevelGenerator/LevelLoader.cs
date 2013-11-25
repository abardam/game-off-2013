using UnityEngine;
using System.Collections;
using System.Xml;

public class LevelLoader 
{ 

	private static string[,] ParseGridFile(string gridFileName) 
	{
		SPFileReader reader = new SPFileReaderLocal();
		
		string[] values = reader.ReadGrid(gridFileName);
		int height = values.Length;
		string[] t = values[0].Split(',');
		
		string[,] array = new string[height,t.Length];
		
		for(int i = 0; i < values.Length; ++i)
		{
			string[] st = values[i].Split(',');
			
			for(int j = 0; j < st.Length; ++j)
			{
				array[i,j] = st[j];
			}
		}

		return array;
	}

	private static void InstantiateGrid(
		string gridFileContents,
		SPLevel level
	)
	{



		//		int width = gridFileContents.GetLength(1);
//		int height = gridFileContents.GetLength(0);
//
//		for (int i = 0; i < height; ++i)
//		{
//			for (int j = 0; j < width; ++j)
//			{
//				Vector3 pos = Util.GridToVec3(j,i);
//				Quaternion rot = Quaternion.identity;
//				Object obj = null;
//				
//				switch(grid1[i,j])
//				{
//				case "1":
//					obj = gridCube;
//					break;
//				case "s":
//					obj = player;
//					break;
//				case "e1":
//					obj = guard1;
//					break;
//				default:
//					continue;
//				}
//				
//				Object temp = Instantiate(obj, pos, rot);
//			}
//		}
	}

	private static void LoadAndInstantiateGrid(
		SPLevel level  						// [out]
	)
	{
		string[,] gridFileContent = ParseGridFile(level.GridFilename);
	}

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
			
				//LoadAndInstantiateGrid(level);

				break;
			}
		}
		// delete me
		GameObject player = GameObject.Instantiate(Resources.LoadAssetAtPath("Assets/Player/Prefab/Player.prefab", typeof(GameObject)) ) as GameObject;
		
		if (!player)
		{
			Debug.Log("Motherfuckka");
		}
		else
		{
			Debug.Log("NO NULL BR0");
		}
		return level;
	}
}
