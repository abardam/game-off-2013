
using System;
using UnityEngine;

public class Trigger
{
	public enum TriggerType {Start, OnEnter, Outside};

	private TriggerType triggerType;
	private Rect rectangle;
	private bool checklist;
	public Trigger (TriggerType tt)
	{
		triggerType = tt;
		checklist = false;
	}

	public Trigger (TriggerType tt, Rect r){
		triggerType = tt;
		rectangle = r;
		checklist = false;
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

	public bool Checklist {
		get {
			return checklist;
		}
		set {
			checklist = value;
		}
	}

}

