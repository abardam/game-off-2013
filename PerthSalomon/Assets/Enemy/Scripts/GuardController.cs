using UnityEngine;
using System.Collections;
using System;

public class GuardController : MonoBehaviour 
{

	private float arc;
	private float range = 5.0f;
	private Vector2 orientation;
	private CharacterController characterController;
	private GuardControllerState state;
	private GridTile startPoint;		// start point for patrolling
	private GridTile endPoint;			// end point for patrolling


	void Start() 
	{	
		this.orientation = new Vector2(-1.0f, 0.0f);
		this.arc = 1.0f/3.0f*(float)(Math.PI);
		this.characterController = this.GetComponent<CharacterController>();
		this.startPoint = Util.Vect3ToGrid(this.transform.position);
		this.endPoint = new GridTile(1, 1);
		this.state = new GuardControllerStatePatrolling();

		Physics.IgnoreCollision(this.characterController, GameState.GetInstance().Player.GetComponent<CharacterController>());
	}
	
	void Update() 
	{
		this.state.Update(this);
//		this.FindPathToPlayer();

//		if (this.IsPlayerVisible())
//		{
//			FollowPlayer();
//		}
	}

//	private bool IsPlayerVisible()
//	{
//		Vector3 pp = GameState.GetInstance().Player.transform.position;
//		Vector3 gp = this.transform.position;
//
//		Vector3 d = pp - gp;
//		float dist = d.magnitude;
//
//		bool isVisible = false;
//
//		Vector2 d2 = new Vector2();
//		d2.x = d.x;
//		d2.y = d.y;
//
//		if (this.range > dist && this.arc/2.0f > Math.Acos(Vector2.Dot(d2, orientation)/(d2.magnitude*orientation.magnitude)))
//		{
//			Debug.Log("I SEE YA");
//			isVisible = true;
//		}
//
//		return isVisible;
//	}
//
//	private void FollowPlayer()
//	{
//		Vector3 pp = GameState.GetInstance().Player.transform.position;
//		Vector3 gp = this.transform.position;
//
//		Vector3 d = pp - gp;
//		d.Normalize();
//		this.characterController.Move(Time.deltaTime*0.25f*d);
//		Debug.Log(d);
//	
//	}

//	private void FindPathToPlayer()
//	{
//		Vector2 gpos = new Vector2(1.5f, 1.5f);
//		gpos.x = this.transform.position.x;
//		gpos.y = this.transform.position.y;
//		
//		Vector2 ppos = new Vector2(2.5f, 2.5f);
//		ppos.x = GameState.GetInstance().Player.transform.position.x;
//		ppos.y = GameState.GetInstance().Player.transform.position.y;
//		
//		GridTile ggt = Util.Vect2ToGrid(gpos);
//		GridTile pgt = Util.Vect2ToGrid(ppos);
//				
//		Pathfinder.FindPath(ggt, pgt);
//	}

	public GridTile StartPoint {
		get {
			return startPoint;
		}
		set {
			startPoint = value;
		}
	}

	public GridTile EndPoint {
		get {
			return endPoint;
		}
		set {
			endPoint = value;
		}
	}
}
