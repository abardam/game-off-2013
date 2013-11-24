using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class should contain every variable we could possibly want to access about the game
public class GameState : MonoBehaviour {

	private GameObject player;
	private GameObject player2;
	private List<GameObject> enemies;
	private bool cutscene;

	public Dialogue dialogueManager;

	// Use this for initialization
	void Start () {
		enemies = new List<GameObject> ();
		cutscene = false;
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

	public bool Cutscene {
		get {
			return cutscene;
		}
		set {
			cutscene = value;
		}
	}

}
