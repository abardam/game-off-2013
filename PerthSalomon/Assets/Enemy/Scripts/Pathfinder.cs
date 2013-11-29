using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class DistComp : System.Collections.Generic.IComparer<float>
{
	public int Compare(float a, float b)
	{
		if (a < b)
		{
			return -1;
		}
		else if (a == b)
		{
			return 1;
		}

		return 1;
	}
}



// finds the shortest path (a list of tiles) from one point to the other
public class Pathfinder
{

	// a grid tile
	public class GridTile
	{
		int i;
		int j;
	};



	public static List<GridTile> FindPath()
	{
		DistComp dc = new DistComp();
		System.Collections.Generic.SortedList<float, string> open = new System.Collections.Generic.SortedList<float, string>(dc);

		open.Add(56.0f, "a");
		open.Add(5.0f, "b");
		open.Add(90.0f, "c");

		foreach(System.Collections.Generic.KeyValuePair<float,string> str in open)
		{
			Debug.Log(str.Value);
		}


		return null;
	}




//	SortedList<float, GridTile> open;

}
