using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsMenuScript : MonoBehaviour {

	public Text ResolutionOption;
	public Text ScreenModeOption;
	public Button ResolutionButton;
	public Button BackButton;
	public string[] ResolutionTexts;
	public int[] ResolutionsX;
	public int[] ResolutionsY;
	int counter = 0;
	Resolution[] Resolutions;

	// Use this for initialization
	void Start () {
		Resolutions = Screen.resolutions;
		ResolutionOption = ResolutionOption.GetComponent<Text> ();
		ScreenModeOption = ScreenModeOption.GetComponent<Text> ();
		ResolutionButton = ResolutionButton.GetComponent<Button> ();
		BackButton = BackButton.GetComponent<Button> ();

		ResolutionTexts = new string[4];
		ResolutionTexts[0] = "1920x1080";
		ResolutionTexts[1] = "1600x900";
		ResolutionTexts[2] = "1366x768";
		ResolutionTexts[3] = "1280x720";

		ResolutionsX = new int[4];
		ResolutionsX [0] = 1920;
		ResolutionsX [1] = 1600;
		ResolutionsX [2] = 1366;
		ResolutionsX [3] = 1280;

		ResolutionsY = new int[4];
		ResolutionsY [0] = 1080;
		ResolutionsY [1] = 900;
		ResolutionsY [2] = 768;
		ResolutionsY [3] = 720;

		for (int i = 0; i < ResolutionTexts.Length; i++) {
			if (ResolutionTexts[i].Contains(Screen.currentResolution.ToString())) {
				counter = i;
				break;
			}
		}
	}

	public void ResolutionButtonPress()
	{
		counter++;

		if (counter > ResolutionTexts.Length - 1)
			counter = 0;

		ResolutionOption.text = ResolutionTexts [counter];
		Screen.SetResolution (ResolutionsX [counter], ResolutionsY [counter], false);
	}

	public void ScreenMode()
	{
		if (Screen.fullScreen) {
			ScreenModeOption.text = "Windowed";
			Screen.fullScreen = false;
		}
		else {
			ScreenModeOption.text = "Full Screen";
			Screen.fullScreen = true;
		}
	}

	public void BackPress()
	{
		Application.LoadLevel (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
