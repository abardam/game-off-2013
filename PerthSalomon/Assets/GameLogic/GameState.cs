﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class should contain every variable we could possibly want to access about the game
public class GameState {

	private GameObject player;
	private GameObject player2;
	private List<GameObject> enemies;
	private bool cutscene;
	private List<StateDependable> stateDependables;

	public DialogueManager dialogueManager;

	// Use this for initialization
	private GameState() {
		enemies = new List<GameObject> ();
		stateDependables = new List<StateDependable>();
		cutscene = false;

	}

	private static GameState instance;
	public static GameState GetInstance(){
		if(instance == null){
			instance = new GameState();
		}

		return instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject Player {
		get {
			return player;
		}
		set {
			player = value;
		}
	}

	public GameObject Player2 {
		get {
			return player2;
		}
		set {
			player2 = value;
		}
	}

	public List<GameObject> Enemies {
		get {
			return enemies;
		}
	}

	public void SetModeDialogue(){
		cutscene = true;

		foreach(StateDependable sd in stateDependables){
			sd.SetCutscene(cutscene);
		}
	}

	public void SetModeGame(){
		cutscene = false;
		
		foreach(StateDependable sd in stateDependables){
			sd.SetCutscene(cutscene);
		}
	}

	public void RegisterDependable(StateDependable sd){
		stateDependables.Add(sd);
	}
}
