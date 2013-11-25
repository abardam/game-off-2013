using UnityEngine;
using System.Collections;

public class DialogueManager : MonoBehaviour {

	private static int TEXTSPEED = 50; //characters per second
	public enum CutsceneType {None, Dialogue, Camera};
	
	public CameraGhost cameraGhost;

	private string dialogue;
	private float dialogueIndex;
	private Rect dialogueRect;
	private CutsceneType activeCutscene;
	private Eventlet callback;
	// Use this for initialization
	void Start () {
		GameState.GetInstance().dialogueManager = this;
		activeCutscene = CutsceneType.None;
		dialogueRect = new Rect(0f, Screen.height*0.75f,Screen.width*1f,Screen.height*0.25f);
	}

	public void SetDialogue(string text){
		dialogueIndex = 0;
		dialogue = text;
		activeCutscene = CutsceneType.Dialogue;
		callback = null;
	}

	public void SetCallback(Eventlet e){
		callback = e;
	}
	
	// Update is called once per frame
	void Update () {
		if(activeCutscene == CutsceneType.Dialogue){
			dialogueIndex += (TEXTSPEED * Time.deltaTime);

			if(dialogueIndex >= dialogue.Length){

			}
		}
	}

	void OnGUI(){
		if(activeCutscene == CutsceneType.Dialogue)
		{
			string dispText = dialogue.Substring(0, dialogueIndex<dialogue.Length?
			                                     (int)dialogueIndex:dialogue.Length);
			GUI.Box (dialogueRect, dispText);
		}
	}

	public void SkipOrAdvance(){

		switch(activeCutscene)
		{
		case CutsceneType.Dialogue:
			if(dialogueIndex >= dialogue.Length){
				if(callback != null){
					callback.Executed = Eventlet.ExecuteState.Executed;
					activeCutscene = CutsceneType.None;
				}

			}else{
				dialogueIndex = dialogue.Length;
			}
			break;
		case CutsceneType.Camera:

			if(cameraGhost.IsAtTarget()){
				if(callback != null){
					callback.Executed = Eventlet.ExecuteState.Executed;
					activeCutscene = CutsceneType.None;
				}
			}

			break;
		}
	}

	
	
	public void SetTarget(GameObject obj){
		cameraGhost.Target = obj;
		activeCutscene = CutsceneType.Camera;
	}
	
	public void SetTarget(Vector3 vec){
		cameraGhost.TargetV = vec;
		activeCutscene = CutsceneType.Camera;
	}
}
