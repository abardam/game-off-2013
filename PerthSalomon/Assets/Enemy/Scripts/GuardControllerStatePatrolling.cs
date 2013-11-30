using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GuardControllerStatePatrolling : GuardControllerState
{
	private GridTile currentStartPoint;
	private GridTile currentEndPoint;

	private List<GridTile> currentPath;
	private List<GridTile> patrolPath;

	public override void Update(GuardController guardController)
	{
		
	}

	private void SetStartPointAndEndPoint(GuardController guardController)
	{
		this.currentStartPoint = Util.Vect3ToGrid(guardController.transform.position);
	}
}
