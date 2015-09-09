using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	public Canvas quitMenu;
	public Button play;
	public Button quit;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		play = play.GetComponent<Button> ();
		quit = quit.GetComponent<Button> ();
		quitMenu.enabled = false;
	}
	
	// Quit
	public void pressExit () {
		quitMenu.enabled = true;
		play.enabled = false;
		quit.enabled = false;
	}

	// No on quitMenu
	public void pressNo() {
		quitMenu.enabled = false;
		play.enabled = true;
		quit.enabled = true;
	}

	// Play
	public void playGame() {
		Application.LoadLevel (1);
	}

	// Access Menu from game
	public void goMenu() {
		Application.LoadLevel (0);
	}

	// Yes on quitMenu
	public void quitGame() {
		Application.Quit ();
	}
}
