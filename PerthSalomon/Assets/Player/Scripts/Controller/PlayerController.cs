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
		Vector2 pos = new Vector2();
		pos.x = this.transform.position.x;
		pos.y = this.transform.position.y;

		GridTile gc = Util.Vect2ToGrid(pos);

//		Debug.Log("[" + gc.i + ", " + gc.j + "]");
//		Pathfinder.FindPath();

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
			if(!(this.state is PlayerControllerStateIdle))
				this.state = new PlayerControllerStateIdle();
		}else{
			if(!(this.state is PlayerControllerStateDiving))
				this.state = new PlayerControllerStateDiving();
		}
	}
}
