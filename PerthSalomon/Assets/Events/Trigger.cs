
using System;
using UnityEngine;

public class Trigger
{
	public enum TriggerType {Start, OnEnter, OnExit};

	private TriggerType triggerType;
	private Rect rectangle;
	public Trigger (TriggerType tt)
	{
		triggerType = tt;
	}

	public Trigger (TriggerType tt, Rect r){
		triggerType = tt;
		rectangle = r;
	}

	public TriggerType GetTriggerType {
		get {
			return triggerType;
		}
	}

	public Rect Rectangle {
		get {
			return rectangle;
		}
	}
}

