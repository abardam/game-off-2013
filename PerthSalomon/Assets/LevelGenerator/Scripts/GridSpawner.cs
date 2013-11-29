﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

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

	private class CodeFilenamePair
	{
		public string code;
		public string filename;
		public float up=0f;
		public float down=1f;
		public float left=0f;
		public float right=1f;
	}

	private List<CodeFilenamePair> tileCodes;

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
		ReadInTileCodes("TileCodes.xml");
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

			gameState.SetGridSize(height, width);
			
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
					switch(grid1[i,j])
					{
					case "s":
						gameState.Player = (GameObject)temp;
						
						cameraControl.Target = gameState.Player;
						break;
					case "1":
						this.SetTexture((GameObject)temp, grid1, i, j);
						break;
					}

					if(grid1[i,j] == "1"){
						gameState.SetGridCell(i,j,1);
					}else{
						gameState.SetGridCell(i,j,0);
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

	public void Reset()
	{
		while(allGameObjects.Count > 0)
		{
			Destroy(allGameObjects[0]);
			allGameObjects.RemoveAt(0);
		}
	}

	private void ReadInTileCodes(string filename)
	{
		tileCodes = new List<CodeFilenamePair>(); 

		SPFileReader reader = new SPFileReaderLocal();
		XmlDocument xml  = reader.ReadXML (filename);
		XmlNodeList nodeList = xml.SelectNodes("//Tiles/Tile");

		int i = 0;
		foreach(XmlNode node in nodeList){
			CodeFilenamePair cfp = new CodeFilenamePair();
			cfp.code = node.Attributes["code"].Value;
			cfp.filename = node.Attributes["filename"].Value;

			if(node.Attributes["up"] != null){
				cfp.up = float.Parse(node.Attributes["up"].Value);
			}
			if(node.Attributes["down"] != null){
				cfp.down = float.Parse(node.Attributes["down"].Value);
			}
			if(node.Attributes["left"] != null){
				cfp.left = float.Parse(node.Attributes["left"].Value);
			}
			if(node.Attributes["right"] != null){
				cfp.right = float.Parse(node.Attributes["right"].Value);
			}


			tileCodes.Add(cfp);
		}

	}

	private void SetTexture(GameObject obj, string[,] grid, int i, int j)
	{
		SpriteRenderer r = obj.GetComponent<SpriteRenderer>();
		BoxCollider bc = obj.GetComponent<BoxCollider>();

		if (!r)
		{
			Debug.Log("SpriteRenderer not found");
		}

		if(!bc){
			Debug.Log("BoxCollider not found");
		}

		char[] neighborCode = ComputeNeighborCode(grid, i, j);

		//r.material.mainTexture = t;

		foreach (CodeFilenamePair cfp in tileCodes)
		{
			if (IsSameCode(neighborCode, cfp.code.ToCharArray()))
			{
				Texture2D t = (Texture2D)Resources.Load(cfp.filename);

				if (!t)
				{
					Debug.Log("Texture not found!!!");
				}

				Sprite s = Sprite.Create(t, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 127.0f);
				r.sprite = s;

				bc.center = new Vector3((cfp.right + cfp.left - 1f)/2f,
				                        -(cfp.up + cfp.down - 1f)/2f,
				                        0f);

				bc.size = new Vector3(cfp.right - cfp.left,
				                      cfp.down - cfp.up,
				                      1f);

				return;
			}
		}
		{
		Texture2D t = (Texture2D)Resources.Load("error");
		
		if (!t)
		{
			Debug.Log("Texture not found!!!");
		}
		
		Sprite s = Sprite.Create(t, new Rect(0, 0, 128, 128), new Vector2(0.5f, 0.5f), 128.0f);
		r.sprite = s;
		}
	}
	
	private bool IsSameCode(char[] neighborCode, char[] tileCode)
	{
		if (neighborCode.Length != tileCode.Length)
		{
			Debug.Log("invalid array length");
		}

		bool s = true;

		for (int i = 0; i < 8; i++)
		{
			if (!((neighborCode[i] == tileCode[i]) || (tileCode[i] == '*')))
			{
				s = false;
			}
		}

		return s;
	}

	private bool CheckValid(int i, int maximum){
		return 0 <= i && i < maximum;
	}

	private char[] ComputeNeighborCode(string[,] grid, int i, int j)
	{
		char[] n = new char[8];

		int u=0;
		for(int i2=i-1;i2<=i+1;++i2){
			for(int j2=j-1;j2<=j+1;++j2){

				if(i2 == i && j2 == j) continue;

				if(CheckValid(i2,grid.GetLength(0)) && CheckValid(j2,grid.GetLength(1))){
					n[u] = grid[i2,j2] == "1"? '1' : '0';
				}else{
					n[u] = '1';
				}

				++u;
			}
		}

		return n;

	}
}
