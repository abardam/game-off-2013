using UnityEngine;
using System.Collections;
using System;

public class UIManager : StateDependable {

	private Rect UIRect;
	private bool cutscene;

	// Use this for initialization
	void Start () {
		UIRect = new Rect(0,Screen.height*0.9f,Screen.width,Screen.height*0.1f);
		cutscene = false;
		GameState.GetInstance().RegisterDependable(this);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){
		if(!cutscene)
		{
			float healthPercent = GameState.GetInstance().Player.GetComponent<PlayerController>().Health / PlayerController.MAXHEALTH;
			healthPercent = (float)Math.Round(healthPercent * 100);
			GUI.Box(UIRect, "Coins left: " + GameState.GetInstance().Coins + " Health: " + healthPercent + "%");
		}
	}

	public override void SetCutscene (bool cutscene)
	{
		this.cutscene = cutscene;
	}
}
