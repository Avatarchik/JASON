using UnityEngine;
using System.Collections;
using SGUI;

public class MenuMain:GUIBehaviour {
	[SerializeField] private Transform creditsCamera;

	[SerializeField] private SGUITexture[] textures;
	[SerializeField] private SGUIButton[] buttons;

	private MenuOptions menuOptions;
	private MenuCredits menuCredits;

	private bool opened = false;

	void Start() {
		foreach(SGUITexture texture in textures) {
			texture.Create();
			texture.Activated = false;
		}

		foreach(SGUIButton button in buttons) {
			button.Create();
			button.Activated = false;
		}

		menuOptions = GetComponent<MenuOptions>();
		menuCredits = GetComponent<MenuCredits>();
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		if(buttons[0].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			ExitApplication.Instance.GameStarted = true;

			GameObject.Find("SGUI Manager").GetComponent<SGUIManager>().RemoveAll();
			
			Application.LoadLevel("Normal Dungeon");
		}

		if(buttons[1].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);

			Camera.main.transform.position = creditsCamera.position;
			Camera.main.transform.rotation = creditsCamera.rotation;

			menuCredits.Open();
		}

		if(buttons[2].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);
			menuOptions.Open();
		}
	}

	/** Open this GUI */
	public void Open() {
		menuOptions.Close();
		menuCredits.Close();

		opened = true;

		StartCoroutine(ButtonDelay());
	}

	/** Close this GUI */
	public void Close() {
		opened = false;

		foreach(SGUITexture texture in textures)
			texture.Activated = false;

		foreach(SGUIButton button in buttons)
			button.Activated = false;
	}

	private IEnumerator ButtonDelay() {
		yield return new WaitForSeconds(0.001f);

		foreach(SGUITexture texture in textures)
			texture.Activated = true;

		foreach(SGUIButton button in buttons)
			button.Activated = true;
	}
}
