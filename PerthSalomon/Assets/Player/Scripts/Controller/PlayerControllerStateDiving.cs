using UnityEngine;
using System.Collections;

public class PlayerControllerStateDiving : PlayerControllerState
{	
	public override void Update(PlayerController playerController) 
	{
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
}
