using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	public Button startButton;

	private string hello = "Hi there!";

	void Start () 
	{
		if (startButton != null)
		{
			Debug.Log("bla");
			startButton.clickHandler = SayHello;
		}
	}

	void Update ()
	{
	
	}

	private void SayHello()
	{
		Debug.Log(hello);
	}
}
