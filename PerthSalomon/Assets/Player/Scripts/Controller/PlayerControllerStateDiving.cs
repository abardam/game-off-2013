using UnityEngine;
using System.Collections;

public class PlayerControllerStateDiving : PlayerControllerState
{	
	enum DiveState
	{
		DIVE_STATE_IDLE = 0,
		DIVE_STATE_LEFT,
		DIVE_STATE_RIGHT,
	};

	enum Event
	{
		EVENT_RIGHT_ARROW_DOWN = 0,
		EVENT_LEFT_ARROW_DOWN,
		EVENT_RIGHT_ARROW_UP,
		EVENT_LEFT_ARROW_UP
	};

	DiveState[,] delta = {
		{DiveState.DIVE_STATE_RIGHT, DiveState.DIVE_STATE_LEFT, DiveState.DIVE_STATE_LEFT, DiveState.DIVE_STATE_RIGHT}, // transitions for IDLE state
		{DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE}, // transitions fo DIVE_STATE_LEFT
		{DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE, DiveState.DIVE_STATE_IDLE} // transitions fo DIVE_STATE_RIGHT
	};


	private DiveState state;

	public PlayerControllerStateDiving()
	{
		this.state = DiveState.DIVE_STATE_IDLE;
	}

	public override void Update(PlayerController playerController) 
	{
		this.UpdateDiveState();

		Vector3 v = Vector3.zero;



		if (Input.GetKey(KeyCode.LeftArrow))
		{
			v.x -= 1.0f;
		}
				
		if (Input.GetKey(KeyCode.RightArrow))
		{
			v.x += 1.0f;
		}
		
		if (Input.GetKey(KeyCode.UpArrow))
		{
			v.y += 1.0f; 
		}
		
		if (Input.GetKey(KeyCode.DownArrow))
		{
			v.y -= 1.0f;
		}

		v.Normalize();

		v *= (playerController.speed*Time.deltaTime);
		
		playerController.GetCharacterController().Move(v);
	}

	private void UpdateDiveState()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			this.state = delta[(int)this.state, (int)Event.EVENT_RIGHT_ARROW_DOWN];
			Debug.Log(this.state);
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			this.state = delta[(int)this.state, (int)Event.EVENT_LEFT_ARROW_DOWN];
			Debug.Log(this.state);
		}

		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			this.state = delta[(int)this.state, (int)Event.EVENT_RIGHT_ARROW_UP];
			Debug.Log(this.state);
		}
		
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			this.state = delta[(int)this.state, (int)Event.EVENT_LEFT_ARROW_UP];
			Debug.Log(this.state);
		}

	}
}
