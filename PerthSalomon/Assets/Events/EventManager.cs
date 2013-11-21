using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class EventManager : MonoBehaviour {

	private List<SPEvent> eventList;

	// Use this for initialization
	void Start () {
		eventList = new List<SPEvent>();

		SPFileReader reader = new SPFileReaderLocal();
		XmlDocument xmlDoc = reader.ReadEvents();

		XmlNodeList eventNodes = xmlDoc.SelectNodes("//Events/Event");
		foreach(XmlNode eventNode in eventNodes){
			SPEvent e = new SPEvent();

			XmlNodeList triggerNodes = eventNode.SelectNodes("Trigger");
			foreach(XmlNode triggerNode in triggerNodes){

				string tts = triggerNode.Attributes["type"].Value;
				Trigger.TriggerType tt = Trigger.TriggerType.Start;
				switch(tts){
				case "start":
					tt = Trigger.TriggerType.Start;
					break;
				}

				Trigger t = new Trigger(tt);
				e.addTrigger(t);
			}
			
			XmlNodeList eventletNodes = eventNode.SelectNodes("Eventlet");
			foreach(XmlNode eventletNode in eventletNodes){

				Eventlet el = new Eventlet();

				if(eventletNode.Attributes["debug"] != null){
					el.setDebug(eventletNode.Attributes["debug"].Value);
				}

				e.addEventlet(el);
			}

			eventList.Add(e);
		}
	}
	
	// Update is called once per frame
	void Update () {
		//lets check our events if they trigger:

		for(int e=eventList.Count-1; e>=0; --e){
			bool triggered = true;
			//for now, events trigger if all of their triggers trigger
			foreach(Trigger t in eventList[e].getTriggerList()){
				switch(t.getTriggerType()){
				case Trigger.TriggerType.Start:
					//nothing! this triggertype automatically triggers
					break;
				default:
					triggered = false;
					break;
				}
			}


			if(!triggered) continue;

			//execute the eventlets

			foreach(Eventlet el in eventList[e].getEventletList()){
				//todo

				string db = el.getDebug();
				if(db != ""){
					Debug.Log("Event triggers: " + db);
				}
			}

			//remove the event from the eventlist
			eventList.RemoveAt(e);

		}
	}
}
