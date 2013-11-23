using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	public float speed = 0.1f;
	private CharacterController charController;
	public GameState gameState;
	
	void Start () 
	{
		this.charController = this.GetComponent<CharacterController>();

		if (!this.charController)
		{
			Debug.LogWarning("In [Controls]: No [CharacterController] component.");
		}
	}
	
	void Update () 
	{
		Vector3 v = Vector3.zero;
		bool next = false;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			v.x -= 1.0f;
		}
		
		if (Input.GetKey(KeyCode.RightArrow))
		{
			v.x += 1.0f;
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			v.y += 1.0f; 
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			v.y -= 1.0f;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			next = true;
		}

		v.Normalize();
		v *= (speed*Time.deltaTime);

		if(gameState.Cutscene){
			if(next){
				gameState.dialogueManager.SkipOrAdvance();
			}
		}
		else{
			charController.Move(v);
		}


	}
}
