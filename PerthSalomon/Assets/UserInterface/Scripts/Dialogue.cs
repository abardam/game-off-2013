using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	private static int TEXTSPEED = 10; //characters per second

	private string dialogue;
	private float dialogueIndex;
	private Rect dialogueRect;
	private bool active;
	private Eventlet callback;
	// Use this for initialization
	void Start () {
		active = false;
		dialogueRect = new Rect(0f, Screen.height*0.75f,Screen.width*1f,Screen.height*0.25f);
	}

	public void SetDialogue(string text){
		dialogueIndex = 0;
		dialogue = text;
		active = true;
		callback = null;
	}

	public void SetCallback(Eventlet e){
		callback = e;
	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			dialogueIndex += (TEXTSPEED * Time.deltaTime);

			if(dialogueIndex >= dialogue.Length){

			}
		}
	}

	void OnGUI(){
		if(active)
		{
			string dispText = dialogue.Substring(0, dialogueIndex<dialogue.Length?
			                                     (int)dialogueIndex:dialogue.Length);
			GUI.Box (dialogueRect, dispText);
		}
	}

	public void SkipOrAdvance(){
		if(dialogueIndex >= dialogue.Length){
			if(callback != null){
				callback.Executed = Eventlet.ExecuteState.Executed;
				active = false;
			}

		}else{
			dialogueIndex = dialogue.Length;
		}
	}
}
