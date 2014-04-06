using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	[SerializeField] private Transform creditsCamera;

	[SerializeField] private Texture logo;

	[SerializeField] private GUIStyle playButton;
	[SerializeField] private GUIStyle optionsButton;
	[SerializeField] private GUIStyle creditsButton;

	private MenuOptions menuOptions;
	private MenuCredits menuCredits;

	private bool opened = false;

	void Start() {
		menuOptions = GetComponent<MenuOptions>();
		menuCredits = GetComponent<MenuCredits>();
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		GUI.DrawTexture(new Rect(707, 25, 496.5f, 512), logo);

		if(GUI.Button(new Rect(20, 915, 517.5f, 158.25f), new GUIContent("Play"), playButton)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			ExitApplication.Instance.GameStarted = true;

			GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();

			Application.LoadLevel("Normal Dungeon");
		}

		if(GUI.Button(new Rect(697, 915, 517.5f, 158.25f), new GUIContent("Options"), optionsButton)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			menuOptions.Open();
		}

		if(GUI.Button(new Rect(1381, 915, 517.5f, 158.25f), new GUIContent("Credits"), creditsButton)) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);

			Camera.main.transform.position = creditsCamera.position;
			Camera.main.transform.rotation = creditsCamera.rotation;

			menuCredits.Open();
		}
	}

	/** Open this GUI */
	public void Open() {
		menuOptions.Close();
		menuCredits.Close();

		opened = true;
	}

	/** Close this GUI */
	public void Close() {
		opened = false;
	}

	public bool IsOpen {
		get { return opened; }
	}
}
