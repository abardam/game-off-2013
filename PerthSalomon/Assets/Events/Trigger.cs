
using System;

public class Trigger
{
	public enum TriggerType {Start};

	private TriggerType triggerType;
	public Trigger (TriggerType tt)
	{
		triggerType = tt;
	}

	public Trigger.TriggerType getTriggerType(){
		return triggerType;
	}
}

