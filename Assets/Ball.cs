using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public float speed = 30;

	// Use this for initialization
	void Start () {
		// Initial velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// col holds collision info; if ball collides with racket then:
		// racket = col.gameObject
		// racket position = col.transform.position
		// racket collider = col.collider

}
