
using System;

public class Eventlet
{
	private string debug;

	public Eventlet ()
	{
		debug = "";
	}

	public void setDebug(string s){
		debug = s;
	}

	public string getDebug(){
		return debug;
	}
}