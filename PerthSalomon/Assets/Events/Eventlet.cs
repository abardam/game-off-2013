
using System;

public class Eventlet
{
	public enum EventletType {Nothing, Dialogue};
	private string debug;
	private string text;
	private EventletType eventletType;
	public Eventlet (EventletType et)
	{
		eventletType = et;
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

	public EventletType GetEventletType {
		get {
			return eventletType;
		}
	}

	public string Text {
		get {
			return text;
		}
		set {
			text = value;
		}
	}
}