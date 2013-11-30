using UnityEngine;
using System.Collections;

public class GuardControllerStateFight : GuardControllerState
{

	public GuardControllerStateFight()
	{
		Debug.Log("FIGHTING");

	}

	public override void Update(GuardController guardController)
	{


	}

	public override void TargetSighted(GuardController gc, GameObject target)
	{

	}

}
