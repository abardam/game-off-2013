using UnityEngine;
using System.Collections;

public class PlayerControllerStateDiving : PlayerControllerState
{	
	enum Orientation
	{
		LEFT = 0,
		RIGHT
	};

	enum VerticalMotion
	{
		NONE = 0,
		UP,
		DOWN
	};

	enum Motion
	{
		IDLE = 0,
		IN_MOTION,
	};

	Orientation orientation;
	Motion motion;
	VerticalMotion verticalMotion;

	public PlayerControllerStateDiving()
	{
		this.orientation = Orientation.RIGHT;
		this.motion = Motion.IDLE;
		this.verticalMotion = VerticalMotion.NONE;
	}

	public override void Update(PlayerController playerController) 
	{
		this.UpdateAnimationState(playerController);

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

	// Updates the animation of the player depending on the pressed keys.
	private void UpdateAnimationState(PlayerController playerController)
	{
		// figure out state based on keyboard input

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			if (this.motion == Motion.IDLE)
			{
				this.motion = Motion.IN_MOTION;
				this.orientation = Orientation.RIGHT;
			}
			else if (this.motion == Motion.IN_MOTION)
			{
				this.motion = Motion.IDLE;
			}
		}
		
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			if (this.motion == Motion.IDLE)
			{
				this.motion = Motion.IN_MOTION;
				this.orientation = Orientation.RIGHT;
			}
			else if (this.motion == Motion.IN_MOTION)
			{
				this.motion = Motion.IDLE;
			}
		}

		if (Input.GetKeyUp(KeyCode.RightArrow))
		{
			if (this.motion == Motion.IDLE)
			{
				this.motion = Motion.IN_MOTION;
				this.orientation = Orientation.LEFT;
			}
			else if (this.motion == Motion.IN_MOTION)
			{
				this.motion = Motion.IDLE;
			}
		}

		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{
			if (this.motion == Motion.IDLE)
			{
				this.motion = Motion.IN_MOTION;
				this.orientation = Orientation.RIGHT;
			}
			else if (this.motion == Motion.IN_MOTION)
			{
				this.motion = Motion.IDLE;
			}			
		}


		if (Input.GetKeyUp(KeyCode.RightArrow))
		{

		}
		
		if (Input.GetKeyUp(KeyCode.LeftArrow))
		{

		}

		Debug.Log(this.orientation);
		Debug.Log(this.motion);


		// figure out the animation's name from the state.
		string animation = "Dive";

		switch (this.motion)
		{
		case Motion.IDLE:

			switch (this.orientation)
			{
			case Orientation.LEFT:
			case Orientation.RIGHT:
				animation = "IdleRight";
				break;
			}

			break;
		case Motion.IN_MOTION:

			switch (this.orientation)
			{
			case Orientation.LEFT:
			case Orientation.RIGHT:
				animation = "Dive";
				break;
			}

			break;
		}

		//Debug.Log(playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(animation));

		// if the animation is not playing already, play it.
		if (!playerController.GetAnimator().GetCurrentAnimatorStateInfo(0).IsName(animation))
		{
			playerController.GetAnimator().Play(animation);
		}
	}
}
