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

			Expand(current, endNode, open, closed);
		}

		Debug.Log("NOT FOUND");
		return null;
	}


	private static void Expand(
		Node current,
		Node endNode,
		System.Collections.Generic.SortedList<float, Node> open,
		HashSet<Node> closed
	)
	{
		HashSet<Node> succ = GetSuccessors(current);

		foreach (Node successor in succ)
		{
			if (closed.Contains(successor))
			{
				continue;
			}

			float tmpg = current.g + ComputeCost(current, successor);

			if (open.ContainsValue(successor))
			{
				float g = open.Values[open.IndexOfValue(successor)].g;

				if (g <= tmpg)
				{
					continue;
				}
			}

			float newg = tmpg;
			float f = tmpg + ComputeCost(successor, endNode);

			if (open.ContainsValue(successor))
			{
				open.RemoveAt(open.IndexOfValue(successor));
			}

			successor.g = newg;
			successor.f = f;

			open.Add(f, successor);
		}
	}

	private static HashSet<Node> GetSuccessors(Node current)
	{
		NodeEqComp eq = new NodeEqComp();
		HashSet<Node> succ = new HashSet<Node>(eq);


//		for (int u = current.gt.i - 1; u <= current.gt.i + 1; u++)
//		{
//			for (int v = current.gt.j - 1; v <= current.gt.j + 1; v++)
//			{
//				if (!((u == current.gt.i) && (v == current.gt.j)) && u >= 0 && v >= 0 && u < gw && v < gh)
//				{
//					succ.Add(new Node(new GridTile(u, v), 0.0f, 0.0f));
//				}
//			}
//		}


		int i = current.gt.i;
		int j = current.gt.j;

		SafeAdd(new Node(new GridTile(i - 1, j), 0.0f, 0.0f), succ);
		SafeAdd(new Node(new GridTile(i + 1, j), 0.0f, 0.0f), succ);
		SafeAdd(new Node(new GridTile(i, j - 1), 0.0f, 0.0f), succ);
		SafeAdd(new Node(new GridTile(i, j + 1), 0.0f, 0.0f), succ);

		return succ;
	}

	private static void SafeAdd(Node node, HashSet<Node> succ)
	{
		int gw = GameState.GetInstance().ObstacleGrid.GetLength(1);
		int gh = GameState.GetInstance().ObstacleGrid.GetLength(0);

		if (node.gt.i >= 0  && node.gt.i < gw && node.gt.j >= 0 && node.gt.j < gh)
		{
			succ.Add(node);
		}
	}

	private static float ComputeCost(Node current, Node successor)
	{
		return (float)(Math.Abs(current.gt.i - successor.gt.i) + Math.Abs(current.gt.j - successor.gt.j)); 
	}
}
