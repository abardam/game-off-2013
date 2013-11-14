using UnityEngine;
using System.Collections;

public class BounceScript : MonoBehaviour {
	
	float r;
	
	// Use this for initialization
	void Start () {
		r=0;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A)){
			r += .5f;
		}
		
		if(Input.GetKey(KeyCode.B)){
			r -= .5f;
		}
		transform.Rotate(Vector3.up,r);
	}
	
	void OnCollisionEnter (Collision other){
		gameObject.rigidbody.AddForce(new Vector3(0,200,0));
	}
	
	void OnCollisionStay (Collision other){
		gameObject.rigidbody.AddForce(new Vector3(0,200,0));
	}
	
	
}
