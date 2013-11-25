using UnityEngine;
using System.Collections;

public class PlayerControllerStateDiving : PlayerControllerState
{	
	enum HState
	{
		H_IDLE_LEFT = 0,
		H_IDLE_RIGHT,
		H_MOVE_LEFT,
		H_MOVE_RIGHT,
		H_ERROR
	};

	enum HEvent
	{
		KEY_LEFT_DOWN = 0,
		KEY_LEFT_UP,
		KEY_RIGHT_DOWN,
		KEY_RIGHT_UP
	};

	HState[,] deltaH = {
			{HState.H_MOVE_LEFT, HState.H_MOVE_RIGHT, HState.H_MOVE_RIGHT, HState.H_MOVE_LEFT}, // Transitions for State: H_IDLE_LEFT
			{HState.H_MOVE_LEFT, HState.H_MOVE_RIGHT, HState.H_MOVE_RIGHT, HState.H_MOVE_LEFT}, // Transitions for State: H_IDLE_RIGHT
			{HState.H_ERROR, HState.H_IDLE_LEFT, HState.H_IDLE_LEFT, HState.H_ERROR}, // Transitions for state: H_MOVE_LEFT
			{HState.H_IDLE_RIGHT, HState.H_ERROR, HState.H_ERROR, HState.H_IDLE_RIGHT} // Transitions for state: H_MOVE_LEFT
		};

	enum VState
	{
		V_IDLE = 0,
		V_MOVE_UP,
		V_MOVE_DOWN,
		V_ERROR
	};

	enum VEvent
	{
		KEY_UP_DOWN = 0,
		KEY_UP_UP,
		KEY_DOWN_DOWN,
		KEY_DOWN_UP
	};

	VState[,] deltaV = {
		{VState.V_MOVE_UP, VState.V_MOVE_DOWN, VState.V_MOVE_DOWN, VState.V_MOVE_UP}, // Transitions for state: V_IDLE
		{VState.V_ERROR, VState.V_IDLE, VState.V_IDLE, VState.V_ERROR}, // Transitions for state: V_MOVE_UP
		{VState.V_IDLE, VState.V_ERROR, VState.V_ERROR, VState.V_IDLE}, // Transitions for state: V_MOVE_DOWN
		{VState.V_ERROR, VState.V_ERROR, VState.V_ERROR, VState.V_ERROR}
	};
		
	HState hState;
	VState vState;

	
	public PlayerControllerStateDiving()
	{
		this.hState = HState.H_IDLE_RIGHT;
		this.vState = VState.V_IDLE;
	}

	public override void Update(PlayerController playerController) 
	{
		this.UpdateState(playerController);
		this.UpdateAnimation(playerController);

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

	private void UpdateState(PlayerController playerController)
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow)) 
		{
			this.hState = deltaH[(int)this.hState, (int)HEvent.KEY_LEFT_DOWN];
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) 
		{
			this.hState = deltaH[(int)this.hState, (int)HEvent.KEY_RIGHT_DOWN];
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow)) 
		{
			this.hState = deltaH[(int)this.hState, (int)HEvent.KEY_LEFT_UP];
		}
		
		if (Input.GetKeyUp(KeyCode.RightArrow)) 
		{
			this.hState = deltaH[(int)this.hState, (int)HEvent.KEY_RIGHT_UP];
		}

		if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{
			this.vState = deltaV[(int)this.vState, (int)VEvent.KEY_UP_DOWN];
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow)) 
		{
			this.vState = deltaV[(int)this.vState, (int)VEvent.KEY_DOWN_DOWN];
		}
		
		if (Input.GetKeyUp(KeyCode.UpArrow)) 
		{
			this.vState = deltaV[(int)this.vState, (int)VEvent.KEY_UP_UP];
		}
		
		if (Input.GetKeyUp(KeyCode.DownArrow)) 
		{
			this.vState = deltaV[(int)this.vState, (int)VEvent.KEY_DOWN_UP];
		}

	
	}

	private void UpdateAnimation(PlayerController playerController)
	{
		// figure out the correct animation string
		string animation = "IdleRight";
		switch (this.vState)
		{
		case VState.V_IDLE:

			switch (this.hState)
			{
			case HState.H_IDLE_RIGHT: 
				animation = "IdleRight";
				break;
			case HState.H_IDLE_LEFT: 
				animation = "IdleLeft";
				break;
			case HState.H_MOVE_RIGHT: 
				animation = "Dive";
				break;
			case HState.H_MOVE_LEFT: 
				animation = "DiveLeft";
				break;
			}
			break;

		case VState.V_MOVE_UP:
		case VState.V_MOVE_DOWN:

			switch (this.hState)
			{
			case HState.H_IDLE_RIGHT: 
				animation = "Dive";
				break;
			case HState.H_IDLE_LEFT: 
				animation = "DiveLeft";
				break;
			case HState.H_MOVE_RIGHT: 
				animation = "Dive";
				break;
			case HState.H_MOVE_LEFT: 
				animation = "DiveLeft";
				break;
			}
			break;
		}

		// if the animationstring differs from the one of the current animation play the new animation
		if (!playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(animation))
		{
			playerController.GetAnimator().Play(animation);
		}

		Debug.Log(this.vState);

	}
}
