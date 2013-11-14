using UnityEngine;
using System.Collections;

public class FileLoader : MonoBehaviour {
	
	string lab;
	
	IEnumerator fileOpen () {
		var filename = "/example.txt";
		Debug.Log(Application.dataPath + filename);
		lab = Application.dataPath + filename;
		WWW download = new WWW(Application.dataPath + filename);
		yield return download;
		var text = download.text;
		Debug.Log(text);
		lab = "fuck yall: " + text;
	}
	
	void Start(){
		lab = "start";
		StartCoroutine(fileOpen());
		
		
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI(){
		Rect rect = new Rect(0,0,Screen.width,Screen.height);
		GUI.Label(rect, lab);
	}
}
