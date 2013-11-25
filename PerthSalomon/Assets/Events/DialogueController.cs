using UnityEngine;
using System.Collections;

public class DialogueController : StateDependable {

	public DialogueManager dialogueManager;
	private bool cutscene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(cutscene){
			if(Input.GetKeyDown(KeyCode.Space)){
				dialogueManager.SkipOrAdvance();
			}
		}
	
	}

	public override void SetCutscene(bool c){
		cutscene = c;
	}
}
