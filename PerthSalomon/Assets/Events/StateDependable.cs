using UnityEngine;
using System.Collections;

//everything that depends on events should implement this class
public abstract class StateDependable : MonoBehaviour{
	//this is what happens to the object when a cutscene happens (example: player and enemies should pause, and 
	//dialogue should get ready to catch input
	public abstract void SetCutscene(bool cutscene);

	public StateDependable():base(){
		GameState.GetInstance().RegisterDependable(this);
	}
}
