using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class GuardController : StateDependable
{

	private float arc;
	private float range = 4.0f;
	private Vector2 orientation;

	private CharacterController characterController;
	private GuardControllerState state;
	private GridTile startPoint;		// start point for patrolling
	private GridTile endPoint;			// end point for patrolling
	private int id;						//guard id, w/r/t the grid

	public GameObject sightArcPrefab;
	private GameObject sightArcCW; //the sight arc line clockwise
	private GameObject sightArcCCW; //the sight arc line counterclockwise

	private float alertTimer;
	private static float PLAYER_SPOTTED_ALERT = 10; //10 seconds after the player leaves the sight arcs, the guard will auto reset to green

	private bool sightArcUp;

	public enum AlertState{
		RED,
		YELLOW,
		GREEN
	};

	private AlertState alertState;

	void Start() 
	{	
		sightArcUp = true;
		alertTimer = -1;
		alertState = AlertState.GREEN;
		this.orientation = new Vector2(-1.0f, 0.0f);
		this.arc = 1.0f/3.0f*(float)(Math.PI);
		this.characterController = this.GetComponent<CharacterController>();
		this.startPoint = Util.Vect3ToGrid(this.transform.position);
		//this.state = new GuardControllerStatePatrolling(); apparently this comes after events, so i commented it -- enzo

		Physics.IgnoreCollision(this.characterController, GameState.GetInstance().Player.GetComponent<CharacterController>());

		//sight arcs
		sightArcCW = (GameObject)Instantiate (sightArcPrefab, this.transform.position, Quaternion.identity);
		sightArcCCW = (GameObject)Instantiate (sightArcPrefab, this.transform.position, Quaternion.identity);
	}
	
	void Update() 
	{
		alertTimer -= Time.deltaTime;

		if(this.state == null || (alertTimer < 0 && alertState != AlertState.GREEN)) {
			this.state = new GuardControllerStatePatrolling();
			alertState = AlertState.GREEN;
		}

		bool playerVisible = this.IsPlayerVisible(GameState.GetInstance().Player);

		if(alertState == AlertState.RED && !playerVisible)
		{
			if(state is GuardControllerStatePatrolling){
				Vector2 d = (state as GuardControllerStatePatrolling).GetLastMovement();
				if(d.x < 0) orientation.x = - Math.Abs(orientation.x);
				else if(d.x > 0) orientation.x = Math.Abs(orientation.x);
			}

		}
		else if(alertState == AlertState.GREEN && !playerVisible){
			if(sightArcUp){
				orientation.y = 0.5f;
			}else{
				orientation.y = -0.5f;
			}
			if(state is GuardControllerStatePatrolling){
				Vector2 d = (state as GuardControllerStatePatrolling).GetLastMovement();
				if(d.x < 0) orientation.x = -0.86603f;
				else if(d.x > 0) orientation.x = 0.86603f;
			}
		}
		else if (playerVisible)
		{
			FollowPlayer(GameState.GetInstance().Player);
			alertTimer = PLAYER_SPOTTED_ALERT;
			alertState = AlertState.RED;
			orientation = GameState.GetInstance().Player.transform.position - this.transform.position;
		}

		if (this.IsPlayerInFightingRange() && !(this.state is GuardControllerStateFight))
		{
			this.state = new GuardControllerStateFight();
			GameState.GetInstance().Player.GetComponent<PlayerController>().SetFighting();
		}
		
		this.state.Update(this);
		//this.FindPathToPlayer();

		float orientationAngle = (float)Math.Atan2 (orientation.y, orientation.x);

		float cwAngle = (orientationAngle - arc / 2f) * (180f/(float)Math.PI);
		sightArcCW.transform.position = this.transform.position;
		sightArcCW.transform.rotation = Quaternion.identity;
		sightArcCW.transform.RotateAround (sightArcCW.transform.position,
		                                  Vector3.forward,
		                                  cwAngle);
		sightArcCW.GetComponent<LineRenderer> ().
			SetPosition (0, new Vector3 (range, 0, 0));

		float ccwAngle = (orientationAngle + arc / 2f) * (180f/(float)Math.PI);
		sightArcCCW.transform.position = this.transform.position;
		sightArcCCW.transform.rotation = Quaternion.identity;
		sightArcCCW.transform.RotateAround (sightArcCCW.transform.position,
		                                   Vector3.forward,
		                                    ccwAngle);
		sightArcCCW.GetComponent<LineRenderer> ().
			SetPosition (0, new Vector3 (range, 0, 0));
	}

	private bool IsPlayerVisible(GameObject go)
	{
		Vector3 pp = go.transform.position;
		Vector3 gp = this.transform.position;

		Vector3 d = pp - gp;
		float dist = d.magnitude;

		bool isVisible = false;

		Vector2 d2 = new Vector2();
		d2.x = d.x;
		d2.y = d.y;

		if (this.range > dist && this.arc/2.0f > Math.Acos(Vector2.Dot(d2, orientation)/(d2.magnitude*orientation.magnitude)))
		{
			List<GridTile> gtList = GameState.GetInstance().GetAllWallTilesBetween(
				Util.Vect3ToGrid(go.transform.position), Util.Vect3ToGrid(this.transform.position));

			isVisible = true;

			foreach(GridTile gt in gtList){
				Ray ray = new Ray(gp, d);
				Debug.DrawRay(gp, d,Color.green);
				Bounds b = new Bounds();
				b.center = Util.GridToVec3(gt.i, gt.j);
				b.size = new Vector3(1f,1f,1f);

				if(b.IntersectRay(ray)){
					isVisible = false;
					break;
				}
			}

		}

		return isVisible;
	}

	public void FlipSightArc(){
		sightArcUp = !sightArcUp;
	}

	private bool IsPlayerInFightingRange()
	{
		float fightingRange = 0.1f;
		float dist = Vector3.SqrMagnitude(this.transform.position - GameState.GetInstance().Player.transform.position);

		return dist < fightingRange;
	}

	private void FollowPlayer(GameObject go)
	{
		this.state.TargetSighted (this, go);
	}
	
	

	public GridTile StartPoint {
		get {
			return startPoint;
		}
		set {
			startPoint = value;
		}
	}

	public GridTile EndPoint 
	{
		get {
			return endPoint;
		}
		set {
			endPoint = value;
		}
	}

	public override void SetCutscene(bool cutscene)
	{
		if (cutscene) 
		{
			if(!(this.state is GuardControllerStateIdle))
			{
				state = new GuardControllerStateIdle();
			}

		} 
		else 
		{
			if((this.state is GuardControllerStateIdle))
			{
				state = new GuardControllerStatePatrolling();
			}
		}
	}

	public int ID {
		get {
			return id;
		}
		set {
			id = value;
		}
	}

	public void OnDestroy(){
		GameObject.Destroy(sightArcCW);
		GameObject.Destroy(sightArcCCW);
	}

	public void SetAlertState (GuardController.AlertState gREEN)
	{
		alertState = gREEN;
	}

}
