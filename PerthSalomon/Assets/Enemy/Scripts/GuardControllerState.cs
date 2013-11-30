using UnityEngine;
using System.Collections;

public abstract class GuardControllerState 
{
	public abstract void Update(GuardController guardController);
	public abstract void TargetSighted(GuardController gc, GameObject target);
}
