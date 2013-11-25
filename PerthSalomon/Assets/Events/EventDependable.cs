using UnityEngine;
using System.Collections;

//everything that depends on events should implement this class
public interface EventDependable {
	//this is what happens to the object when a cutscene happens (example: player and enemies should pause, and 
	//dialogue should get ready to catch input
	void SetCutscene(bool cutscene);
}
