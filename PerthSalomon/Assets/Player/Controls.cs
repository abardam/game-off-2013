using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour 
{
	void Start () 
	{
	
	}
	
	void Update () 
	{
		Vector3 pos = this.transform.position;

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			pos.y += 1;
			Debug.Log("Hello From Player");
		}

		if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			pos.y -= 1;
			Debug.Log("Hello From Player");
		}

		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			pos.x -= 1;
			Debug.Log("Hello From Player");
		}

		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			pos.x += 1;
			Debug.Log("Hello From Player");
		}

		this.transform.position = pos;
	}
}
