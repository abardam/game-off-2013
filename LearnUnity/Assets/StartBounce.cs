using UnityEngine;
using System.Collections;

public class StartBounce : MonoBehaviour {
	
	public Transform bcube;
	// Use this for initialization
	void Start () {
		for(int i=0;i<5;++i){
			Instantiate(bcube, new Vector3(i,i,-3),  Quaternion.identity);
			
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
