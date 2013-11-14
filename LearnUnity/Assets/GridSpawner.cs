using UnityEngine;
using System.Collections;

public class GridSpawner : MonoBehaviour {
	
	public Transform gridCube;
	
	// Use this for initialization
	void Start () {
		int[] grid1 = 
			{0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1, 0, 0, 1, 0, 0,
			0, 0, 1, 0, 0, 1, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0,
			0, 1, 0, 0, 0, 0, 1, 0,
			0, 1, 1, 1, 1, 1, 1, 0};

		int width = 8;
		int height = 6;

		for(int i=0;i<width*height;++i){
			
			if(grid1[i] == 1){
				Instantiate(gridCube, new Vector3(i%width,-i/width,0), Quaternion.identity);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
