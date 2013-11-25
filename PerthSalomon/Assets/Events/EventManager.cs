using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;

public class EventManager : MonoBehaviour
{

		private List<SPEvent> eventList;
		private List<Eventlet> eventletQueue;
		public DialogueManager dialogueManager;
	
		private GameState gameState;
	
		// Use this for initialization
		void Start ()
		{
		gameState = GameState.GetInstance();

				eventList = new List<SPEvent> ();
				eventletQueue = new List<Eventlet> ();

				SPFileReader reader = new SPFileReaderLocal ();
				XmlDocument xmlDoc = reader.ReadEvents ();

				XmlNodeList eventNodes = xmlDoc.SelectNodes ("//Events/Event");
				foreach (XmlNode eventNode in eventNodes) {
						SPEvent e = new SPEvent ();

						XmlNodeList triggerNodes = eventNode.SelectNodes ("Trigger");
						foreach (XmlNode triggerNode in triggerNodes) {

								string tts = triggerNode.Attributes ["type"].Value;
								Trigger.TriggerType tt = Trigger.TriggerType.Start;

								bool checklist = false;
								//checklist?
								if (triggerNode.Attributes ["checklist"] != null && triggerNode.Attributes ["checklist"].Value == "true") {
										checklist = true;
								}

								Rect r = new Rect ();
								//check tags for correct properties, then assign rectangle
								if (triggerNode.Attributes ["rectTop"] != null 				//y value of topmost tile (grid coordinates)
										&& triggerNode.Attributes ["rectBottom"] != null			//y value of lowermost tile (grid coordinates)
										&& triggerNode.Attributes ["rectLeft"] != null			//x value of leftmost tile
										&& triggerNode.Attributes ["rectRight"] != null) {
										string rectTs = triggerNode.Attributes ["rectTop"].Value;
										string rectBs = triggerNode.Attributes ["rectBottom"].Value;
										string rectLs = triggerNode.Attributes ["rectLeft"].Value;
										string rectRs = triggerNode.Attributes ["rectRight"].Value;

										int gcT = int.Parse (rectTs);
										int gcB = int.Parse (rectBs);
										int gcL = int.Parse (rectLs);
										int gcR = int.Parse (rectRs);

										Vector3 vTL = Util.GridToVec3 (gcL, gcT);
										Vector3 vBR = Util.GridToVec3 (gcR, gcB);
					
										r.width = vBR.x - vTL.x + 1f;
										r.height = vTL.y - vBR.y + 1f;
										r.x = vTL.x - 0.5f;
										r.y = vTL.y - r.height + 0.5f;
								}

								Trigger t = null;
								switch (tts) {
								case "start":
										tt = Trigger.TriggerType.Start;
										t = new Trigger (tt);
										break;
								case "onenter":
										tt = Trigger.TriggerType.OnEnter;
										t = new Trigger (tt, r);
										break;
								case "onexit":
										tt = Trigger.TriggerType.Outside;
										t = new Trigger (tt, r);

					//onexit trigger is actually 2 triggers: one for entering the area and one for leaving
										Trigger t2 = new Trigger (Trigger.TriggerType.OnEnter, r);
										t2.Checklist = true;
										e.addTrigger (t2);

										break;
								}

								t.Checklist = checklist;

								e.addTrigger (t);
						}
			
						XmlNodeList eventletNodes = eventNode.SelectNodes ("Eventlet");
						foreach (XmlNode eventletNode in eventletNodes) {
				
								Eventlet.EventletType ett = Eventlet.EventletType.Nothing;

								if (eventletNode.Attributes ["type"] != null) {
										string ets = eventletNode.Attributes ["type"].Value;

										switch (ets) {
										case "dialogue":
												ett = Eventlet.EventletType.Dialogue;
												break;
					case "focus":
						ett = Eventlet.EventletType.Focus;
						break;

										}
								}

								Eventlet el = new Eventlet (ett);
				
								if (eventletNode.Attributes ["debug"] != null) {
										el.Debug = (eventletNode.Attributes ["debug"].Value);
								}

								if (eventletNode.Attributes ["text"] != null) {
										el.Text = eventletNode.Attributes ["text"].Value;
								}

				if(eventletNode.Attributes["targetX"] != null && 
				   eventletNode.Attributes["targetY"] != null){
					int gridX = int.Parse(eventletNode.Attributes["targetX"].Value);
					int gridY = int.Parse(eventletNode.Attributes["targetY"].Value);

					el.Target = Util.GridToVec3(gridX, gridY);
				}

								e.addEventlet (el);
						}

						eventList.Add (e);
				}
		}
	
		// Update is called once per frame
		void Update ()
		{
				//lets check our events if they trigger:

				for (int e=eventList.Count-1; e>=0; --e) {
						bool triggered = true;
						//for now, events trigger if all of their triggers trigger
						for (int tr=eventList[e].TriggerList.Count-1; tr >= 0; --tr) {
								Trigger t = eventList [e].TriggerList [tr];
								//this switch checks NEGATIVELY, i.e. it sets triggered to false if it doesn't trigger now
								switch (t.GetTriggerType) {
								case Trigger.TriggerType.Start:
					//nothing! this triggertype automatically triggers

										eventList [e].TriggerATrigger (t);

										break;
								case Trigger.TriggerType.OnEnter:
					//triggers when player enters a defined rectangle
										if (gameState.Player == null) {
												triggered = false;
										} else if (!Util.Vec3WithinRect (gameState.Player.transform.position, t.Rectangle)) {
												triggered = false;
										} else {
												eventList [e].TriggerATrigger (t);
										}
										break;
								case Trigger.TriggerType.Outside:
					//triggers when player is not inside a defined rectangle
										if (gameState.Player == null) {
												triggered = false;
										} else if (Util.Vec3WithinRect (gameState.Player.transform.position, t.Rectangle)) {
												triggered = false;
										} else {
												eventList [e].TriggerATrigger (t);
										}
										break;
								default:
										triggered = false;
										break;
								}
						}


						if (!triggered)
								continue;

						//execute the eventlets
						eventletQueue.AddRange (eventList [e].EventletList);

						//remove the event from the eventlist
						eventList.RemoveAt (e);

				}

				//execute the queued up eventlets
				if (eventletQueue.Count > 0) {
						//execute the first
						Eventlet el = eventletQueue [0];
						if (el.Executed == Eventlet.ExecuteState.Start) {
								el.Executed = Eventlet.ExecuteState.Executing;
			
								string db = el.Debug;
								if (db != "") {
										Debug.Log ("Event triggers: " + db);
								}
			
								switch (el.GetEventletType) {
								case Eventlet.EventletType.Nothing:
										el.Executed = Eventlet.ExecuteState.Executed;
										break;
				case Eventlet.EventletType.Dialogue:
					gameState.SetModeDialogue();
										dialogueManager.SetDialogue (el.Text);
										dialogueManager.SetCallback (el);
										break;
								case Eventlet.EventletType.Focus:
					gameState.SetModeDialogue();
									dialogueManager.SetTarget(el.Target);
									dialogueManager.SetCallback(el);

									break;
								}
						} else if (el.Executed == Eventlet.ExecuteState.Executed) {
				eventletQueue.RemoveAt (0);
				gameState.SetModeGame();
				dialogueManager.SetTarget(gameState.Player);
						}
				}
		}
}
