using UnityEngine;
using System.Collections;
using System.IO;

public class SPFileReaderLocal : SPFileReader {

	// Use this for initialization
	public override void Start () {
	
	}

	public override string[] ReadLevel(){
		return File.ReadAllLines("Book1.csv");
	}

	public override string[] ReadEvents ()
	{
		throw new System.NotImplementedException ();
	}
}
