
using System;
using UnityEngine;

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


	public class GridCoordinate
	{
		int i;
		int j;
	}

	public static GridCoordinate Vect2ToGrid(Vector2 v)
	{
		int i = (int)(v.x + 4.0f);
		int j = (int)(v.y - 3);

		return new GridCoordinate();
	}

}
