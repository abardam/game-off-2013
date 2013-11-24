using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class Button : MonoBehaviour 
{
	public GUISkin skin;

	// a delegate describing a click handler
	public delegate void OnClick();

	// a click handler that is called when set and the button is clicked.
	public OnClick clickHandler;

	public string text;
	public Vector2 relPos;
	private Vector2 screenPos;
	public Vector2 screenDim;

	private AudioSource clickSound;


	void Start()
	{
		this.screenPos.x = this.relPos.x*Screen.width - this.screenDim.x/2;
		this.screenPos.y = this.relPos.y*Screen.height - this.screenDim.y/2;
		this.clickSound = this.GetComponent<AudioSource>();
	}

	void OnGUI()
	{
		Rect r = new Rect(screenPos.x, screenPos.y, screenDim.x, screenDim.y);

		if (this.skin != null)
		{
			GUI.skin = this.skin;
		}
		
		if (GUI.Button(r, this.text) && null != this.clickHandler)
		{
			this.clickSound.Play();
			this.clickHandler();
		}
	}
}