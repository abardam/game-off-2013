
using System;

public class Eventlet
{
	private string debug;

	public Eventlet ()
	{
		debug = "";
	}

	public string Debug {
		get {
			return debug;
		}
		set {
			debug = value;
		}
	}
}