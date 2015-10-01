using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuScript : MonoBehaviour {

	public Canvas QuitMenu;
	public Button StartButton;
	public Button OptionsButton;
	public Button ExitButton;

	// Use this for initialization
	void Start () {
		QuitMenu = QuitMenu.GetComponent<Canvas> ();
		StartButton = StartButton.GetComponent<Button> ();
		ExitButton = ExitButton.GetComponent<Button> ();
		QuitMenu.enabled = false;
	}

	public void OptionsPress()
	{
		Application.LoadLevel (1);
	}

	public void ExitPress()
	{
		QuitMenu.enabled = true;
		StartButton.enabled = false;
		OptionsButton.enabled = false;
		ExitButton.enabled = false;
	}

	//When no is selected in the quitMenu
	public void NoPress()
	{
		QuitMenu.enabled = false;
		StartButton.enabled = true;
		OptionsButton.enabled = true;
		ExitButton.enabled = true;
	}

	//Loads the first level
	public void StartLevel()
	{
		Application.LoadLevel (2);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
