using UnityEngine;
using System.Collections;

public abstract class SPFileReader : MonoBehaviour {

	// Use this for initialization
	public abstract void Start ();
	public abstract string[] ReadLevel();
	public abstract string[] ReadEvents();


}
