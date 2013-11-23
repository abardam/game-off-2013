using UnityEngine;
using System.Collections;

public class Dialogue : MonoBehaviour {

	private static int TEXTSPEED = 10; //characters per second

	string dialogue;
	float dialogueIndex;
	Rect dialogueRect;
	bool active;
	// Use this for initialization
	void Start () {
		active = false;
		dialogueRect = new Rect(0f, Screen.height*0.75f,Screen.width*1f,Screen.height*0.25f);
	}

	public void SetDialogue(string text){
		dialogueIndex = 0;
		dialogue = text;
		active = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(active){
			dialogueIndex += (TEXTSPEED * Time.deltaTime);
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
}
