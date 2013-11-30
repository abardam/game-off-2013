using UnityEngine;
using System.Collections;

public class PlayerControllerStateFight : PlayerControllerState
{
	bool hasAnimationStarted;
	float elapsed;

	public PlayerControllerStateFight()
	{
		this.hasAnimationStarted = false;
		this.elapsed = 0.0f;
	}

	public override void Update(PlayerController playerController)
	{
		if (!this.hasAnimationStarted)
		{
			playerController.GetComponent<Animator>().Play("Fight");
			this.hasAnimationStarted =  true;
		}

		this.elapsed += Time.deltaTime;

		if (this.elapsed > 5.0f)
		{
			playerController.SetState(new PlayerControllerStateDiving());
		}

	}
	
}
