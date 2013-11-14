using UnityEngine;
using System.Collections;
using System.IO;

public class GridSpawner : MonoBehaviour {
	
	public Transform gridCube;
	public Camera camera;

	// parse in thing
	int[,] parse () {
		string[] values = File.ReadAllLines("Book1.csv");
		int height = values.Length;
		string[] t = values[0].Split(',');

		int[,] array = new int[height,t.Length];

		for(int i=0;i<values.Length;++i){
			string[] st = values[i].Split(',');

			for(int j=0;j<st.Length;++j){
				array[i,j] = int.Parse(st[j]);
			}
		}

		return array;
	}

	// Use this for initialization
	void Start () {

		int[,] grid1 = parse ();

		int width = grid1.GetLength(1);
		int height = grid1.GetLength(0);

		for(int i=0;i<height;++i){
			for(int j=0;j<width;++j)
			{
				if(grid1[i,j] == 1){
					Instantiate(gridCube, new Vector3(j,-i,0), Quaternion.identity);
				}
			}
		}

		camera.transform.position.Set(3.5f, -2.5f, -10f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
