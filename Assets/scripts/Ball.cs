﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
	public float speed = 30;
	public GUIText score1;
	public GUIText score2;
	public int p1;
	public int p2;

	// Use this for initialization
	void Start () {
		p1 = 0;
		p2 = 0;
		UpdateScore ();

		// Initial velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
	}

	float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketHeight) {
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y - racketPos.y) / racketHeight;
	}

	void OnCollisionEnter2D(Collision2D col) {
		// col holds collision info; if ball collides with racket then:
		// racket = col.gameObject
		// racket position = col.transform.position
		// racket collider = col.collider

		// hit left racket
		if (col.gameObject.name == "RacketLeft") {
			// calculate hit factor
			float y = hitFactor (transform.position, col.transform.position, col.collider.bounds.size.y);

			// calculate direction, set length = 1 via .normalized
			Vector2 dir = new Vector2 (1, y).normalized;

			// set velocity with dir * speed
			GetComponent<Rigidbody2D> ().velocity = dir * speed;

			p1++;
			UpdateScore ();
		}

		// hit right racket
		if (col.gameObject.name == "RacketRight") {
			// calculate hit factor
			float y = hitFactor (transform.position, col.transform.position, col.collider.bounds.size.y);

			// calculate direction, set length = 1 via .normalized
			Vector2 dir = new Vector2 (-1, y).normalized;

			// set velocity with dir * speed
			GetComponent<Rigidbody2D> ().velocity = dir * speed;

			p2++;
			UpdateScore ();
		}

		if (speed < 50) {
			speed++;
		}

		GetComponent<AudioSource>().Play ();

	}

	void UpdateScore() {
		score1.text = "" + p1;
		score2.text = "" + p2;
	}

}
