
using System;
using UnityEngine;

public class Eventlet
{
	public enum EventletType {Nothing, Dialogue, Focus, LoadLevel};
	public enum ExecuteState {Start, Executing, Executed};

	private string debug;
	private string text;
	private EventletType eventletType;
	private ExecuteState executed;
	private Vector3 target;
	public Eventlet (EventletType et)
	{
		eventletType = et;
		debug = "";
		executed = ExecuteState.Start;
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

	public ExecuteState Executed {
		get {
			return executed;
		}
		set {
			executed = value;
		}
	}

	public Vector3 Target {
		get {
			return target;
		}
		set {
			target = value;
		}
	}
}