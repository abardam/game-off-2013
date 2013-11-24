using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
	public float speed;
	private CharacterController characterController;
	private PlayerControllerState state;

	void Start() 
	{
		this.characterController = this.GetComponent<CharacterController>();

		if (!this.characterController)
		{
			Debug.LogWarning("Warning: no [CharacterController] component found.");
		}

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
}
