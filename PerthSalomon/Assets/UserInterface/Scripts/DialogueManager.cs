using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialoguePortrait {
	private List<Texture2D> textures;
	private float currentFrame;

	public DialoguePortrait(){
		textures = new List<Texture2D>();
		currentFrame = 0;
	}

	public void AddTexture(Texture2D texture){
		textures.Add(texture);
	}

	public void AddTime(float t){
		currentFrame += t;
	}

	public Texture2D GetCurrentTexture(){
		if(currentFrame > textures.Count){
			currentFrame = 0;
		}
		return textures[(int)currentFrame];
	}
}

public class DialogueManager : MonoBehaviour {

	private static int TEXTSPEED = 50; //characters per second
	private static int PORTRAITSPEED = 12;
	public enum CutsceneType {None, Dialogue, Camera};
	
	public CameraGhost cameraGhost;

	private string dialogue;
	private float dialogueIndex;
	private Rect dialogueRect;
	private CutsceneType activeCutscene;
	private Eventlet callback;

	private Hashtable dialoguePortraitTable;
	private bool portraitsLoaded;
	private Rect portraitRectLeft;
	private Rect portraitRectRight;

	private string currentPortraitLeft;
	private string currentPortraitRight;

	public DialogueManager():base(){
		dialoguePortraitTable = new Hashtable();
		portraitsLoaded = false;

	}

	// Use this for initialization
	void Start () {
		GameState.GetInstance().dialogueManager = this;
		activeCutscene = CutsceneType.None;
		dialogueRect = new Rect(0f, Screen.height*0.75f,Screen.width*1f,Screen.height*0.25f);
	}

	public void SetDialogue(string text, string portraitLeft, string portraitRight){
		dialogueIndex = 0;
		dialogue = text;

		currentPortraitLeft = portraitLeft;
		currentPortraitRight = portraitRight;

		activeCutscene = CutsceneType.Dialogue;
		callback = null;
	}

	public void SetCallback(Eventlet e){
		callback = e;
	}

	private void LoadPortraits(){

		portraitRectLeft = new Rect(0f, Screen.height*0.5f, Screen.height*0.5f, Screen.height*0.5f);
		portraitRectRight = new Rect(Screen.width - Screen.height*0.5f, Screen.height*0.5f, Screen.height*0.5f,Screen.height*0.5f);

		//init textures
		
		DialoguePortrait alaskaTalk = new DialoguePortrait();
		alaskaTalk.AddTexture((Texture2D)Resources.Load("alaska talk 1"));
		alaskaTalk.AddTexture((Texture2D)Resources.Load("alaska talk 2"));
		alaskaTalk.AddTexture((Texture2D)Resources.Load("alaska talk 3"));
		
		dialoguePortraitTable.Add("alaska talk", alaskaTalk);

		DialoguePortrait solomonTalk = new DialoguePortrait();
		solomonTalk.AddTexture((Texture2D)Resources.Load("solomon talk 1"));
		solomonTalk.AddTexture((Texture2D)Resources.Load("solomon talk 2"));
		solomonTalk.AddTexture((Texture2D)Resources.Load("solomon talk 3"));

		dialoguePortraitTable.Add("solomon talk", solomonTalk);
	}

	// Update is called once per frame
	void Update () {

		if(!portraitsLoaded){
			LoadPortraits();
			portraitsLoaded = true;
		}

		if(activeCutscene == CutsceneType.Dialogue){
			dialogueIndex += (TEXTSPEED * Time.deltaTime);

			if(dialogueIndex >= dialogue.Length){

			}

			foreach(DictionaryEntry entry in dialoguePortraitTable){
				(entry.Value as DialoguePortrait).AddTime(PORTRAITSPEED * Time.deltaTime);
			}
		}
	}

	void OnGUI(){
		if(activeCutscene == CutsceneType.Dialogue)
		{
			string dispText = dialogue.Substring(0, dialogueIndex<dialogue.Length?
			                                     (int)dialogueIndex:dialogue.Length);
			GUI.Box (dialogueRect, dispText);

			if(currentPortraitLeft != ""){
				GUI.DrawTexture(portraitRectLeft, (dialoguePortraitTable[currentPortraitLeft] as DialoguePortrait).GetCurrentTexture());
			}
			if(currentPortraitRight != ""){	
				GUI.DrawTexture(portraitRectRight, (dialoguePortraitTable[currentPortraitRight] as DialoguePortrait).GetCurrentTexture());
			}

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
