using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GuardControllerStatePatrolling : GuardControllerState
{
	enum PatrolState
	{
		STATE_PATROLLING_START_TO_END = 0,
		STATE_PATROLLING_END_TO_START,
		STATE_PATROLLING_INITIAL_TO_START,
		STATE_NOT_PATROLLING
	};

	enum MoveState
	{
		STATE_MOVING_RIGHT = 0,
		STATE_MOVING_LEFT
	};

	private GridTile currentStartPoint;
	private GridTile currentEndPoint;
	private GridTile currentTile;
	private List<GridTile> currentPath;
	private PatrolState patrolState;
	private MoveState moveState;
	private Queue<GridTile> pathQueue;

	private float speed;

	public GuardControllerStatePatrolling()
	{
		// default init member
		this.speed = 0.8f;
		this.pathQueue = new Queue<GridTile>();
		this.moveState = MoveState.STATE_MOVING_LEFT;
		this.patrolState = PatrolState.STATE_NOT_PATROLLING;
	}

	public override void Update(GuardController guardController)
	{
		switch (this.patrolState)
		{
		case PatrolState.STATE_NOT_PATROLLING:
			this.SetStartPointAndEndPoint(guardController);
			break;
		default:
			this.Patrol(guardController);
			break;
		}
	}

	public override void TargetSighted (GameObject target)
	{
		throw new System.NotImplementedException ();
	}

	private void UpdateAnimation()
	{

	}

	private void Patrol(GuardController guardController)
	{
		UpdateCurrentTile(guardController);

		if (UpdatePathQueueAndCheckIsEnd(guardController))
		{
			this.SetStartPointAndEndPoint(guardController);
		}
		else
		{
			MoveTo(guardController);
		}
	}

	private void UpdateCurrentTile(GuardController guardController)
	{
		this.currentTile = Util.Vect3ToGrid(guardController.transform.position);
	}

	private bool UpdatePathQueueAndCheckIsEnd(GuardController guardController)
	{
		int i = 0;

		foreach (GridTile gt in this.currentPath)
		{
			if (gt.Equals(this.currentTile))
			{
				return this.DetermineNextTile(i, guardController);
			}
			
			i++;
		}

		return true;
	}

	private bool DetermineNextTile(int i, GuardController guardController)
	{
		Vector2 v0 = Util.GridToVec2(this.pathQueue.Peek());
		Vector2 v1 = Util.Vect3ToVect2(guardController.transform.position);
		Vector2 d = v0 - v1;

		if (d.magnitude <= 0.1f)
		{
			this.pathQueue.Dequeue();

			if (i >= currentPath.Count - 1)
			{
				return true;
			}

			this.pathQueue.Enqueue(currentPath[i + 1]);
		}

		return false;
	}

	private void MoveTo(GuardController guardController)
	{
		//this.pathQueue.Peek().Dump();
		Vector2 v0 = Util.GridToVec2(this.pathQueue.Peek());
		Vector2 v1 = Util.Vect3ToVect2(guardController.transform.position);
		Vector2 d = v0 - v1;

		d.Normalize();
	
		Vector3 m = new Vector3(d.x, d.y, 0.0f);

		// updated animation if neccessary
		if (d.x < 0f && this.moveState == MoveState.STATE_MOVING_RIGHT)
		{
			this.moveState = MoveState.STATE_MOVING_LEFT;
			guardController.GetComponent<Animator>().Play("DiveLeft");
		}
		if (d.x > 0f && this.moveState == MoveState.STATE_MOVING_LEFT)
		{
			this.moveState = MoveState.STATE_MOVING_RIGHT;
			guardController.GetComponent<Animator>().Play("Dive");
		}

		guardController.GetComponent<CharacterController>().Move(this.speed*Time.deltaTime*m);
	}

	private void SetStartPointAndEndPoint(GuardController guardController)
	{
		this.currentStartPoint = Util.Vect3ToGrid(guardController.transform.position);

		if (this.currentStartPoint.Equals(guardController.StartPoint))
		{
			this.currentStartPoint = guardController.StartPoint;
			this.currentEndPoint = guardController.EndPoint;
			patrolState = PatrolState.STATE_PATROLLING_START_TO_END;
		}
		else if (this.currentStartPoint.Equals(guardController.EndPoint))
		{
			this.currentStartPoint = guardController.EndPoint;
			this.currentEndPoint = guardController.StartPoint;
			patrolState = PatrolState.STATE_PATROLLING_END_TO_START;
		}
		else 
		{
			bool s = this.currentStartPoint.DistTo(guardController.StartPoint) < 
				this.currentStartPoint.DistTo(guardController.EndPoint);

			if (s)
			{
				this.currentEndPoint = guardController.StartPoint;
			}
			else
			{
				this.currentEndPoint = guardController.EndPoint;
			}

			patrolState = PatrolState.STATE_PATROLLING_INITIAL_TO_START;
		}

		this.currentTile = this.currentStartPoint;
		this.currentPath = Pathfinder.FindPath(this.currentStartPoint, this.currentEndPoint);
		this.pathQueue.Clear();
		this.pathQueue.Enqueue(this.currentStartPoint);
	
		if (this.currentEndPoint.i > this.currentStartPoint.i)
		{
			this.moveState = MoveState.STATE_MOVING_RIGHT;
			guardController.GetComponent<Animator>().Play("Dive");
		}
		else
		{
			this.moveState = MoveState.STATE_MOVING_LEFT;
			guardController.GetComponent<Animator>().Play("DiveLeft");
		}
		
	}
}
