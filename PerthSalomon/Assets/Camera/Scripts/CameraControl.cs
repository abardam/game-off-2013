using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {

	public CameraGhost ghost;
	private Vector2 minBounds;
	private Vector2 maxBounds;
	private float Xoff;
	private float Yoff;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPos = transform.position;
		newPos = new Vector3(ghost.transform.position.x, 
		                     ghost.transform.position.y,
		                     -10);
		
		if (minBounds != null && maxBounds != null) {
			if (newPos.x < minBounds.x) {
				newPos.x = minBounds.x;
			}
			if (newPos.y < minBounds.y) {
				newPos.y = minBounds.y;
			}
			
			if (newPos.x > maxBounds.x) {
				newPos.x = maxBounds.x;
			}
			if (newPos.y > maxBounds.y) {
				newPos.y = maxBounds.y;
			}
		}
		
		transform.position = newPos;
		
	}
	
	public GameObject Target {
		get {
			return ghost.Target;
		}
		set {
			ghost.Target = value;
		}
	}

	public void SetBounds(Vector2 min, Vector2 max){
		minBounds = min;
		maxBounds = max;

		if (minBounds.x > maxBounds.x) {
			float temp = minBounds.x;
			minBounds.x = maxBounds.x;
			maxBounds.x = temp;
		}
		
		if (minBounds.y > maxBounds.y) {
			float temp = minBounds.y;
			minBounds.y = maxBounds.y;
			maxBounds.y = temp;
		}

		minBounds.x += Xoff;
		maxBounds.x -= Xoff;
		minBounds.y += Yoff;
		maxBounds.y -= Yoff;
	}

	public void setGridParams (float d, float d2)
	{
		Xoff = d;
		Yoff = d2;
	}
}
