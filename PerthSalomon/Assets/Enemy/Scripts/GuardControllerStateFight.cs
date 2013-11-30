using UnityEngine;
using System.Collections;

public class GuardControllerStateFight : GuardControllerState
{
	bool hasAnimationStarted;
	float elapsed;

	public GuardControllerStateFight()
	{
		Debug.Log("da");
		this.hasAnimationStarted = false;
		this.elapsed = 0.0f;
	}

	public override void Update(GuardController guardController)
	{
		if (!this.hasAnimationStarted)
		{
			guardController.GetComponent<Animator>().Play("Fight");
			this.hasAnimationStarted = true;
		}


		this.elapsed += Time.deltaTime;
		//Debug.Log(this.elapsed);

		if (elapsed > 4.8f)
		{
			Debug.Log("DELETESELF");
			GameObject.Destroy(guardController.gameObject);
		}
	}

	public override void TargetSighted(GuardController gc, GameObject target)
	{

	}

}
