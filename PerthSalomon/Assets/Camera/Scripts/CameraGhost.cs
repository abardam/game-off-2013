using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour {

	private static float SPEED = 1f;
	private static float THRESHOLD = 0.1f;
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
		
		Vector3 dirVector = newPos - transform.position;

		float norm = dirVector.magnitude;

		if(norm > THRESHOLD)
		{

			float currSpeed = SPEED * Time.deltaTime;
			if(norm > currSpeed){
				norm = currSpeed;
			}

			Vector3 speedVector = currSpeed * dirVector.normalized;

			transform.position+=speedVector;
		}
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
