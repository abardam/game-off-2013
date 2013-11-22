
using System;
using System.Collections.Generic;

public class SPEvent
{
	private List<Trigger> triggerList;
	private List<Eventlet> eventletList;
	public SPEvent ()
	{
		triggerList = new List<Trigger>();
		eventletList = new List<Eventlet>();
	}

	public void addTrigger(Trigger t){
		triggerList.Add (t);
	}

	public void addEventlet(Eventlet e){
		eventletList.Add (e);
	}

	public List<Trigger> TriggerList {
		get {
			return triggerList;
		}
	}

	public List<Eventlet> EventletList {
		get {
			return eventletList;
		}
	}
}