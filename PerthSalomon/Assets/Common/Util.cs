
using System;
using UnityEngine;


// a grid tile
public class GridTile
{
	public int i;
	public int j;

	public GridTile(int x, int y)
	{
		i = x;
		j = y;
	}

	public bool Equals(GridTile gt)
	{
		bool a = this.i == gt.i;
		bool b = this.j == gt.j;
		bool c = a && b;
		return c;
	}

	public void Dump()
	{
	//	Debug.Log("Grid Tile = [i = " + i + " j = " + j + "]");
	}

	public string ToString()
	{ 
		return "[i = " + i + " j = " + j + "]";
	}

	public float DistTo(GridTile gt)
	{
		return (float)(Math.Abs(this.i - gt.i) + Math.Abs(this.j - gt.j));
	}
};


public class Util
{
	public static bool Vec3WithinRect(Vector3 vec3, Rect rect){
		return vec3.x > rect.x && vec3.x < rect.x + rect.width && vec3.y > rect.y && vec3.y < rect.y + rect.height;
	}

	//converts from grid coords (top-left 0,0, btm right N,M) to unity coords (top-left -3.5, 2.5, btm right N-3.5, 2.5-M
	public static Vector3 GridToVec3(int x, int y)
	{
		
		float xs = -3.5f;
		float ys = 2.5f;

		return new Vector3 (xs + x, ys - y, 0);
	}

	public static Vector3 GridToVec2(int x, int y)
	{
		
		float xs = -3.5f;
		float ys = 2.5f;
		
		return new Vector2 (xs + x, ys - y);
	}

	public static Vector3 GridToVec2(GridTile gt)
	{
		
		float xs = -3.5f;
		float ys = 2.5f;
		
		return new Vector2 (xs + gt.i, ys - gt.j);
	}


	public static Vector2 Vect3ToVect2(Vector3 v)
	{
		return new Vector2(v.x, v.y);
	}

	
	public static GridTile GetGridCoordForGameobject(GameObject o)
	{
		return Vect2ToGrid(Vect3ToVect2(o.transform.position));
	}

	public static GridTile Vect3ToGrid(Vector3 v)
	{
		return Vect2ToGrid(Vect3ToVect2(v));
	}


	public static GridTile Vect2ToGrid(Vector2 v)
	{
		int i = (int)(v.x + 4.0f);
		int j = -(int)(v.y - 3.0f);

		return new GridTile(i, j);
	}

}
