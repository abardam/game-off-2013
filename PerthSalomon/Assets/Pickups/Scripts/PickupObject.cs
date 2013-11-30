using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

	public enum PickupType{
		Coin, Key
	};

	public PickupType pickupType;
	private int id;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider c){
		if(c.gameObject.GetComponent<PlayerController>() != null){
			switch(pickupType){
			case PickupType.Coin:
				break;
			case PickupType.Key:
				break;
			}

		}
	}

	public int ID {
		get {
			return id;
		}
		set {
			id = value;
		}
	}
}
