using UnityEngine;
using System.Collections;

public class PlayerControllerStateIdle : PlayerControllerState
{
	public override void Update(PlayerController playerController) 
	{
		if (Input.anyKey)
		{
			Debug.Log("Dude I am Idle");
		}
	}
}
