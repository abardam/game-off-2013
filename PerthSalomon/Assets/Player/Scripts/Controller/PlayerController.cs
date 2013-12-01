using UnityEngine;
using System.Collections;

public class PlayerController : StateDependable 
{
	private float speed;
	private CharacterController characterController;
	private Animator animator;
	private PlayerControllerState state;
	private float health;
	private int salmon;

	public static float MAXHEALTH=15f;

	private static float SALMONTIME = 10f;
	private static float BOOSTSPEED = 0.8f;
	private float speedBoostTime;

	public PlayerController():base()
	{	
	}

	void Start() 
	{
		health = MAXHEALTH;
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
		speedBoostTime = 0;
		salmon = 0;
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

		if (this.state == null) 
			this.state = new PlayerControllerStateDiving();
		this.state.Update(this);

		if(this.health < 0) Application.LoadLevel ("GameOver");

		if(state is PlayerControllerStateDiving){
			if(speedBoostTime >= 0) speedBoostTime -= Time.deltaTime;
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
		if(cutscene)
		{
			if(!(this.state is PlayerControllerStateIdle))
			{
				this.state = new PlayerControllerStateIdle();
			}
		}
		else
		{
			if((this.state is PlayerControllerStateIdle)) 
			{
				this.state = new PlayerControllerStateDiving();
			}
		}
	}

	public void SetFighting()
	{
		this.state = new PlayerControllerStateFight();
	}

	public void SetState(PlayerControllerState state)
	{
		this.state = state;
	}

	public float Health {
		get {
			return health;
		}
		set {
			health = value;
			if(health > MAXHEALTH) health = MAXHEALTH;
		}
	}

	public int Salmon {
		get {
			return salmon;
		}
		set {
			salmon = value;
		}
	}

	public bool IsBoosted(){
		return this.speedBoostTime > 0;
	}

	public void SpeedBoost ()
	{
		if(!IsBoosted() && salmon > 0){
			--salmon;
			speedBoostTime = SALMONTIME;
		}
	}

	public float RealSpeed {
		get {
			return speed + (IsBoosted()?BOOSTSPEED:0);
		}
		set {
			speed = value;
		}
	}
}
