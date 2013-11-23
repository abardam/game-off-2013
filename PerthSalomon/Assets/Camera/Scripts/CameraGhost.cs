using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour {

	private GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		if (target != null) {
			newPos = new Vector3(target.transform.position.x, 
			                     target.transform.position.y,
			                     -10);
		}
		
		transform.position = newPos;
	}

	public GameObject Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}
}
