using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour {

	private static float SPEED = 1f;
	private static float THRESHOLD = 0.1f;
	private GameObject target;
	private Vector3 targetV;

	private bool onObject;

	private bool atTarget;

	// Use this for initialization
	void Start () {
		onObject = false;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		if (onObject) {
			newPos = new Vector3(target.transform.position.x, 
			                     target.transform.position.y,
			                     -10);
		}

		if(!onObject) {
			newPos = targetV;
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
			atTarget = false;
		}
		else{
			atTarget = true;
		}
	}

	public bool IsAtTarget(){
		return atTarget;
	}

	public GameObject Target {
		get {
			return target;
		}
		set {
			target = value;
			onObject = true;
		}
	}

	public Vector3 TargetV {
		get {
			return targetV;
		}
		set {
			targetV = value;
			onObject = false;
		}
	}
}
