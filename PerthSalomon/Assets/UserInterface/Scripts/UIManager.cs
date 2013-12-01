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
			PlayerController p = GameState.GetInstance().Player.GetComponent<PlayerController>();
			float healthPercent = p.Health / PlayerController.MAXHEALTH;
			healthPercent = (float)Math.Round(healthPercent * 100);
			string outRect = "Coins left: " + GameState.GetInstance().Coins + "\nHealth: " + healthPercent + "%\n";

			if(p.IsBoosted())
			{
				outRect += "Speed boosted!";
			}else{
				int salmon = p.Salmon;
				if(salmon > 0) outRect += "Salmon: " + salmon;
			}

			GUI.Box(UIRect, outRect);
		}
	}

	public override void SetCutscene (bool cutscene)
	{
		this.cutscene = cutscene;
	}
}
