using UnityEngine;
using System.Collections;
using System;

public class GuardController : MonoBehaviour 
{

	private float arc;
	private float range = 5.0f;
	private Vector2 orientation;
	private CharacterController characterController;

	void Start() 
	{
		this.orientation = new Vector2(-1.0f, 0.0f);
		this.arc = 1.0f/3.0f*(float)(Math.PI);
		this.characterController = this.GetComponent<CharacterController>();

	}
	
	void Update() 
	{

		//GridTile gtg = Util.Vect2ToGrid(


		if (this.IsPlayerVisible())
		{
			FollowPlayer();
		}
	}

	private bool IsPlayerVisible()
	{
		Vector3 pp = GameState.GetInstance().Player.transform.position;
		Vector3 gp = this.transform.position;

		Vector3 d = pp - gp;
		float dist = d.magnitude;

		bool isVisible = false;

		Vector2 d2 = new Vector2();
		d2.x = d.x;
		d2.y = d.y;

		if (this.range > dist && this.arc/2.0f > Math.Acos(Vector2.Dot(d2, orientation)/(d2.magnitude*orientation.magnitude)))
		{
			Debug.Log("I SEE YA");
			isVisible = true;
		}

		return isVisible;
	}

	private void FollowPlayer()
	{
		Vector3 pp = GameState.GetInstance().Player.transform.position;
		Vector3 gp = this.transform.position;

		Vector3 d = pp - gp;
		d.Normalize();
		this.characterController.Move(Time.deltaTime*0.25f*d);
		Debug.Log(d);
	
	}

}
