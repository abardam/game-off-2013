using UnityEngine;
using System.Collections;

public class PlayerController : StateDependable 
{
	public float speed;
	private CharacterController characterController;
	private Animator animator;
	private PlayerControllerState state;

	public PlayerController():base()
	{	
		this.state = new PlayerControllerStateDiving();
	}

	void Start() 
	{
		this.characterController = this.GetComponent<CharacterController>();

		if (!this.characterController)
		{
			Debug.LogWarning("Warning: no [CharacterController] component found.");
		}

		this.animator = this.GetComponent<Animator>();

		if (!this.animator)
		{
			Debug.LogWarning("Warning: no [Animator] component found");
		}

		this.animator.Play("IdleRight");

		this.speed = 0.8f;
	}

	void SetIdle()
	{
		this.state = new PlayerControllerStateIdle();
	}

	void SetDiving()
	{
		this.state = new PlayerControllerStateDiving();
	}

	void Update() 
	{
		if (this.state != null)
		{
			this.state.Update(this);
		}
	}

	public CharacterController GetCharacterController()
	{
		return this.characterController;
	}

	public Animator GetAnimator()
	{
		return this.animator;
	}

	public override void SetCutscene (bool cutscene)
	{
		if(cutscene){
			this.state = new PlayerControllerStateIdle();
		}else{
			this.state = new PlayerControllerStateDiving();
		}
	}
}
