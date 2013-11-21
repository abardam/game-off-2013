using UnityEngine;
using System.Collections;
using System.IO;

public class GridSpawner : MonoBehaviour 
{	
	public GameObject gridCube;
	public GameObject player;

	// parse in thing
	string[,] Parse() 
	{
		SPFileReader reader = new SPFileReaderLocal();

		string[] values = reader.ReadLevel();
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
	
	void Start() 
	{
		string[,] grid1 = this.Parse();

		int width = grid1.GetLength(1);
		int height = grid1.GetLength(0);

		float xs = -3.5f;
		float ys = 2.5f;

		for (int i = 0; i < height; ++i)
		{
			for (int j = 0; j < width; ++j)
			{
				Debug.Log(grid1[i,j] + " ");
				Vector3 pos = new Vector3(xs + j, ys - i, 0);
				Quaternion rot = Quaternion.identity;
				Object obj = null;

				if (grid1[i,j] == "1")
				{
					obj = gridCube;
				}
				else if (grid1[i,j] == "s")
				{
					Debug.Log("s here");
					obj = player;
				}
				else{
					continue;
				}

				Instantiate(obj, pos, rot);
			}
		}


	}

	void Update ()
	{
	
	}
}
