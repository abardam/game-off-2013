using UnityEngine;
using System.Collections;

public class GridSpawner : MonoBehaviour {
	
	public Transform gridCube;

	
	// Use this for initialization
	void Start () {
		int[] grid1 = 
		{1, 0, 0, 0, 0,
			0, 0, 0, 0, 0,
			0, 0, 0, 0, 1,
			0, 0, 0, 0, 0};

		for(int i=0;i<20;++i){
			
			if(grid1[i] == 1){
				Instantiate(gridCube, new Vector3(i%5,i/5,0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
