using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class EventManager : MonoBehaviour {

	private List<SPEvent> eventList;
	public GameState gameState;

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

				Rect r = new Rect();
				//check tags for correct properties, then assign rectangle
				if(triggerNode.Attributes["rectTop"] != null 				//y value of topmost tile (grid coordinates)
				   && triggerNode.Attributes["rectBottom"] != null			//y value of lowermost tile (grid coordinates)
				   && triggerNode.Attributes["rectLeft"] != null			//x value of leftmost tile
				   && triggerNode.Attributes["rectRight"] != null){
					string rectTs = triggerNode.Attributes["rectTop"].Value;
					string rectBs = triggerNode.Attributes["rectBottom"].Value;
					string rectLs = triggerNode.Attributes["rectLeft"].Value;
					string rectRs = triggerNode.Attributes["rectRight"].Value;

					int gcT = int.Parse (rectTs);
					int gcB = int.Parse (rectBs);
					int gcL = int.Parse (rectLs);
					int gcR = int.Parse (rectRs);

					Vector3 vTL = Util.GridToVec3(gcL, gcT);
					Vector3 vBR = Util.GridToVec3(gcR, gcB);
					
					r.width = vBR.x - vTL.x + 1f;
					r.height = vTL.y - vBR.y + 1f;
					r.x = vTL.x;
					r.y = vTL.y - r.height;
				}

				Trigger t = null;
				switch(tts){
				case "start":
					tt = Trigger.TriggerType.Start;
					t = new Trigger(tt);
					break;
				case "onenter":
					tt = Trigger.TriggerType.OnEnter;
					t = new Trigger(tt, r);
					break;
				}

				e.addTrigger(t);
			}
			
			XmlNodeList eventletNodes = eventNode.SelectNodes("Eventlet");
			foreach(XmlNode eventletNode in eventletNodes){

				Eventlet el = new Eventlet();

				if(eventletNode.Attributes["debug"] != null){
					el.Debug = (eventletNode.Attributes["debug"].Value);
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
			foreach(Trigger t in eventList[e].TriggerList){

				//this switch checks NEGATIVELY, i.e. it sets triggered to false if it doesn't trigger now
				switch(t.GetTriggerType){
				case Trigger.TriggerType.Start:
					//nothing! this triggertype automatically triggers
					break;
				case Trigger.TriggerType.OnEnter:
					//triggers when player enters a defined rectangle
					if(gameState.Player == null){
						triggered = false;
					}
					else if(!Util.Vec3WithinRect(gameState.Player.transform.position, t.Rectangle)){
						triggered = false;
					}

					break;
				default:
					triggered = false;
					break;
				}
			}


			if(!triggered) continue;

			//execute the eventlets

			foreach(Eventlet el in eventList[e].EventletList){
				//todo

				string db = el.Debug;
				if(db != ""){
					Debug.Log("Event triggers: " + db);
				}
			}

			//remove the event from the eventlist
			eventList.RemoveAt(e);

		}
	}
}
