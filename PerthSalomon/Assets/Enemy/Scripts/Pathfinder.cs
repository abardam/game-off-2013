using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class Node
{
	public GridTile gt;
	public float g;
	public float f;

	public Node(GridTile gtN, float gN, float fN)
	{
		this.gt = gtN;
		this.g = gN;
		this.f = fN;
	}

	public bool Equals(Node a)
	{
		return this.gt.Equals(a.gt);
	}
}

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

public class NodeEqComp : System.Collections.Generic.IEqualityComparer<Node>
{
	public bool Equals(Node a, Node b)
	{
		return a.gt.Equals(b.gt);
	}

	public int GetHashCode(Node a)
	{
		return a.gt.i + a.gt.j*GameState.GetInstance().ObstacleGrid.GetLength(1);
	}
}

// finds the shortest path (a list of tiles) from one point to the other
public class Pathfinder
{
	// noob A* implementation
	public static List<GridTile> FindPath(GridTile start, GridTile end)
	{
		DistComp dc = new DistComp();
		NodeEqComp eq = new NodeEqComp();
		System.Collections.Generic.SortedList<float, Node> open = new System.Collections.Generic.SortedList<float, Node>(dc);
		HashSet<Node> closed = new HashSet<Node>(eq);
		Node endNode = new Node(end, 0.0f, 0.0f);

		open.Add(0.0f, new Node(start, 0.0f, 0.0f));

		while (open.Count > 0)
		{

			Node current = open.Values[0];
			open.RemoveAt(0);

			if (current.Equals(endNode))
			{
				Debug.Log("FOUND");
				return null;
			}

			closed.Add(current);

		}

		Debug.Log("NOT FOUND");
		return null;
	}


	private static void Expand(
		Node current,
		System.Collections.Generic.SortedList<float, GridTile> open,
		HashSet<GridTile> closed
	)
	{
		HashSet<Node> succ = GetSuccessors(current);

//		foreach (GridTile successor in succ)
//		{
//			if (closed.Contains(successor))
//			{
//				float f = 
//			}
//		}
	}

	private static HashSet<Node> GetSuccessors(Node current)
	{
		NodeEqComp eq = new NodeEqComp();
		HashSet<Node> succ = new HashSet<Node>(eq);

		int gw = GameState.GetInstance().ObstacleGrid.GetLength(1);
		int gh = GameState.GetInstance().ObstacleGrid.GetLength(0);

		for (int u = current.gt.i - 1; u <= current.gt.i + 1; u++)
		{
			for (int v = current.gt.j - 1; v <= current.gt.j + 1; v++)
			{
				if (!((u == current.gt.i) && (v == current.gt.j)) && u >= 0 && v >= 0 && u < gw && v < gh)
				{
					succ.Add(new Node(new GridTile(u, v), 0.0f, 0.0f));
				}
			}
		}

		return succ;
	}

}
