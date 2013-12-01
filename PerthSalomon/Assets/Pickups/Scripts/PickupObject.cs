using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

	public enum PickupType{
		Coin, Key, Health, Speed
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
				--GameState.GetInstance().Coins;
				GameObject.Destroy(this.gameObject);
				break;
			case PickupType.Key:
				GameState.GetInstance().PickupKey(ID);
				GameObject.Destroy(this.gameObject);
				break;
			case PickupType.Health:
				c.gameObject.GetComponent<PlayerController>().Health += 10f;
				GameObject.Destroy(this.gameObject);
				break;
			case PickupType.Speed:
				c.gameObject.GetComponent<PlayerController>().Salmon += 1;
				GameObject.Destroy(this.gameObject);
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
