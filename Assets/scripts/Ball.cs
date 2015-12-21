using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {
	public float speed = 30;
	public Canvas GUI;
	public Text sc1;
	public Text sc2;
	public Button menu;
	public Canvas scoreMenu;
	public Text winner;
	public Button replay;
	public Button quit;
	public int p1;
	public int p2;
	public GameObject ball;
	public GameObject newBall;
	public Rigidbody2D ballRB;
	Vector2 originalPosition;
	
	// Use this for initialization
	void Start () {
		GUI = GUI.GetComponent<Canvas> ();
		sc1 = sc1.GetComponent<Text> ();
		sc2 = sc2.GetComponent<Text> ();
		menu = menu.GetComponent<Button> ();
		scoreMenu = scoreMenu.GetComponent<Canvas> ();
		winner = winner.GetComponent<Text> ();
		replay = replay.GetComponent<Button> ();
		quit = quit.GetComponent<Button> ();
		GUI.enabled = true;
		scoreMenu.enabled = false;

		UpdateScore ();
		ballRB = GetComponent<Rigidbody2D> ();
		originalPosition = transform.position;
		
		// Initial velocity
		ballRB.velocity = Vector2.right * speed;
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
		}
		
		// hit right racket
		if (col.gameObject.name == "RacketRight") {
			// calculate hit factor
			float y = hitFactor (transform.position, col.transform.position, col.collider.bounds.size.y);
			
			// calculate direction, set length = 1 via .normalized
			Vector2 dir = new Vector2 (-1, y).normalized;
			
			// set velocity with dir * speed
			GetComponent<Rigidbody2D> ().velocity = dir * speed;
		}

		// dead zones
		if (col.gameObject.name == "WallLeft" || col.gameObject.name == "WallRight") {
			if (col.gameObject.name == "WallLeft") {
				p2++;
			} else {
				p1++;
			}
			UpdateScore ();
			// Show winner once 10 rounds are played
			if (p1+p2 == 10) {
				if (p1 > p2) {
					Destroy(ball);
					scoreMenu.enabled = true;
					winner.text = "Player 1 wins!";
					replay.enabled = true;
					quit.enabled = true;
				} else if (p1 < p2) {
					Destroy(ball);
					scoreMenu.enabled = true;
					winner.text = "Player 2 wins!";
					replay.enabled = true;
					quit.enabled = true;
				} else if (p1 == p2) {
					Destroy(ball);
					scoreMenu.enabled = true;
					winner.text = "It's a tie!";
					replay.enabled = true;
					quit.enabled = true;
				}
			// continue with rounds
			} else {
				resetBall();
				speed = 30;
			}
		}
		
		if (speed < 50) {
			speed++;
		}
		
		GetComponent<AudioSource>().Play ();
		
	}

	// respawns ball when it enters a dead zone
	void resetBall() {
		transform.position = originalPosition;
		ballRB.velocity = Vector2.right * speed;
	}

	void UpdateScore() {
		sc1.text = "" + p1;
		sc2.text = "" + p2;
	}
}

	