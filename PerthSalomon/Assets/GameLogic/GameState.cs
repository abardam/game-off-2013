using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//This class should contain every variable we could possibly want to access about the game
public class GameState {

	private GameObject player;
	private GameObject player2;
	private List<GameObject> enemies;
	private bool cutscene;
	private List<StateDependable> stateDependables;
	private LevelLoader levelLoader;

	public DialogueManager dialogueManager;

	private int[,] obstacleGrid;
	
	private static GameState instance;
	public static GameState GetInstance(){
		if(instance == null){
			instance = new GameState();
		}

		return instance;
	}

	private GameState() {
		enemies = new List<GameObject> ();
		stateDependables = new List<StateDependable>();
		cutscene = false;
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

	public int[,] ObstacleGrid {
		get {
			return obstacleGrid;
		}
		set {
			obstacleGrid = value;
		}
	}

	public void SetGridSize(int height, int width){
		this.obstacleGrid = new int[height,width];
	}

	public void SetGridCell(int row, int col, int value){
		this.obstacleGrid[row,col] = value;
	}

	public List<GridTile> GetAllWallTilesBetween(GridTile a, GridTile b){ //non inclusive
		int top = a.j < b.j? a.j : b.j;
		int bottom = a.j >= b.j? a.j : b.j;
		int left = a.i < b.i? a.i : b.i;
		int right = a.i >= b.i ? a.i : b.i;

		List<GridTile> gtList = new List<GridTile>();

		for(int r = top; r <= bottom; ++ r){
			for(int c = left; c <= right; ++c){
				if(obstacleGrid[r,c] == 1){
					GridTile gt = new GridTile(c,r);

					if(!gt.Equals(a) && !gt.Equals(b))
						gtList.Add(gt);
				}
			}
		}

		return gtList;
	}
}
