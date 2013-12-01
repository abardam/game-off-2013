using UnityEngine;
using System.Collections;

public class StartScreenScript : MonoBehaviour {

	public GUISkin skin;
	public string toDisplay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey){
			Application.LoadLevel("Level");
		}
	}

	void OnGUI(){
		
		GUI.skin = skin;
		GUI.Label(new Rect(Screen.width*0.75f, Screen.height*0.75f,Screen.width*0.25f, Screen.height*0.25f), toDisplay);
	}
}
