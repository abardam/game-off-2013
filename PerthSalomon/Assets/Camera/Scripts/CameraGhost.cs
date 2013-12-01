using UnityEngine;
using System.Collections;

public class CameraGhost : MonoBehaviour {

	private static float ACCELERATION = 1f;
	private static float MAXSPEED = 5f;
	private static float THRESHOLD = 0.01f;
	private GameObject target;
	private Vector3 targetV;

	private bool onObject;

	private bool atTarget;
	private float speed;

	// Use this for initialization
	void Start () {
		onObject = false;
		speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = transform.position;
		if (onObject && target != null) {
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
			
			atTarget = false;
			
			speed += ACCELERATION * Time.deltaTime;
			if(speed > MAXSPEED) speed = MAXSPEED;
		}else{
			
			atTarget = true;
			
			speed -= ACCELERATION * Time.deltaTime;
			if(speed < 0) speed = 0;
		}

		float currSpeed = speed * Time.deltaTime;
		if(norm > currSpeed){
			norm = currSpeed;
		}else if(currSpeed > norm){
			currSpeed = norm;
		}

		Vector3 speedVector = currSpeed * dirVector.normalized;

		transform.position+=speedVector;
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
