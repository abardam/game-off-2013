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
		STATE_NOT_PATROLLING,
		STATE_DIRECT_CHASE
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

	//enzo hacky shit
	private GameObject directTarget;
	private static float DIRECTDISTANCESQ = 2f;

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
		case PatrolState.STATE_DIRECT_CHASE:
			MoveToDirect(guardController);
			break;
		default:
			this.Patrol(guardController);
			break;
		}
	}

	public override void TargetSighted (GuardController guardController, GameObject target)
	{
		float dist = Vector3.SqrMagnitude (guardController.transform.position - target.transform.position);

		if(dist > DIRECTDISTANCESQ){
			SetStartPointAndEndPointTarget (guardController, Util.Vect3ToGrid (target.transform.position));
		}else{
			directTarget = target;
			patrolState = PatrolState.STATE_DIRECT_CHASE;
		}
	}

	private void UpdateAnimation(GuardController guardController, Vector2 d)
	{
		
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

		if(this.currentPath == null) return false;

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
		if (pathQueue.Count < 1)
						return true;

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

		if(this.pathQueue == null || this.pathQueue.Count < 1) return;

		Vector2 v0 = Util.GridToVec2(this.pathQueue.Peek());
		Vector2 v1 = Util.Vect3ToVect2(guardController.transform.position);
		Vector2 d = v0 - v1;

		d.Normalize();
	
		Vector3 m = new Vector3(d.x, d.y, 0.0f);

		UpdateAnimation (guardController, d);

		guardController.GetComponent<CharacterController>().Move(this.speed*Time.deltaTime*m);
	}

	private void MoveToDirect(GuardController guardController){
		Vector3 m = directTarget.transform.position - guardController.transform.position;

		if(Vector3.SqrMagnitude(m) > DIRECTDISTANCESQ){

			this.patrolState = PatrolState.STATE_NOT_PATROLLING;

			return;
		}

		m.Normalize ();
		Vector2 d = new Vector2 (m.x, m.y);
		
		UpdateAnimation (guardController, d);
		
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
	}

	private void SetStartPointAndEndPointTarget(GuardController guardController, GridTile targetTile)
	{
		patrolState = PatrolState.STATE_PATROLLING_INITIAL_TO_START;
		this.currentStartPoint = Util.Vect3ToGrid(guardController.transform.position);

		this.currentEndPoint = targetTile;
		this.currentTile = this.currentStartPoint;
		this.currentPath = Pathfinder.FindPath(this.currentStartPoint, this.currentEndPoint);
		this.pathQueue.Clear();
		if(currentPath != null){
			if(currentPath.Count > 1)
				this.pathQueue.Enqueue (currentPath[1]);
			else pathQueue.Enqueue(currentPath[0]);
		}
	}
}
