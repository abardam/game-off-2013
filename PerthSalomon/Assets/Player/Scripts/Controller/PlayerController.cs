using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	private CharacterController characterController;
	private Animator animator;
	private PlayerControllerState state;

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
		this.state = new PlayerControllerStateDiving();
	}

	void Update() 
	{
		this.state.Update(this);
	}

	public CharacterController GetCharacterController()
	{
		return this.characterController;
	}

	public Animator GetAnimator()
	{
		return this.animator;
	}
}
