using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

	private Rect UIRect;

	// Use this for initialization
	void Start () {
		UIRect = new Rect(0,Screen.height*0.9f,Screen.width,Screen.height*0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		GUI.Box(UIRect, "Coins left: " + GameState.GetInstance().Coins);
	}
}
