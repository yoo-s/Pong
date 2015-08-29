using UnityEngine;
using System.Collections;

public class MoveRacket2 : MonoBehaviour {
	public float speed = 30;
	
	// Update is called once per frame
	void FixedUpdate () {
		float v = Input.GetAxisRaw ("Vertical2");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, v) * speed;
		
	}
}
