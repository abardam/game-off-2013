using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class GridSpawner : MonoBehaviour 
{	
	public GameObject gridCube;
	public GameObject player;
	public GameObject guard1;
	public CameraControl cameraControl;

	private bool parsed;
	private GameState gameState;
	private string gridFilename;

	private List<GameObject> allGameObjects;

	// parse in thing
	string[,] Parse() 
	{
		SPFileReader reader = new SPFileReaderLocal();

		string[] values = reader.ReadGrid(gridFilename);
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

	public GridSpawner():base(){	
		parsed = true;
		allGameObjects = new List<GameObject>();
	}

	void Start() 
	{
		gameState = GameState.GetInstance();
	}

	void Update ()
	{
		if(!parsed)
		{
			parsed = true;
			string[,] grid1 = this.Parse();
			
			int width = grid1.GetLength(1);
			int height = grid1.GetLength(0);
			
			cameraControl.setGridParams (3.5f, 2.5f);
			cameraControl.SetBounds (Util.GridToVec2 (0, 0), Util.GridToVec2 (width-1, height-1));
			
			for (int i = 0; i < height; ++i)
			{
				for (int j = 0; j < width; ++j)
				{
//					Debug.Log(grid1[i,j] + " ");
					Vector3 pos = Util.GridToVec3(j,i);
					Quaternion rot = Quaternion.identity;
					Object obj = null;
					
					switch(grid1[i,j])
					{
					case "1":
						obj = gridCube;
						break;
					case "s":
						obj = player;
						break;
					case "e1":
						obj = guard1;
						break;
					default:
						continue;
					}
					
					Object temp = Instantiate(obj, pos, rot);
					
					//edit gamestate
					switch(grid1[i,j]){
					case "s":
						gameState.Player = (GameObject)temp;
						
						cameraControl.Target = gameState.Player;
						break;
					}

					allGameObjects.Add ((GameObject)temp);
				}
			}
		}

	}

	public string GridFilename {
		get {
			return gridFilename;
		}
		set {
			gridFilename = value;
			Reset();
			parsed = false;

		}
	}

	public void Reset(){
		while(allGameObjects.Count > 0){
			Destroy(allGameObjects[0]);
			allGameObjects.RemoveAt(0);
		}
	}
}
