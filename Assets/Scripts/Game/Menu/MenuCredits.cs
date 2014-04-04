using UnityEngine;
using System.Collections;
using SGUI;

public class MenuCredits:GUIBehaviour {
	[SerializeField] private Transform normalCamera;

	[SerializeField] private SGUITexture[] textures;
	[SerializeField] private SGUIButton[] buttons;
	[SerializeField] private SGUILabel[] labels;

	private MenuMain menuMain;

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

		foreach(SGUILabel label in labels) {
			label.Create();
			label.Activated = false;
		}

		menuMain = GetComponent<MenuMain>();
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		if(buttons[0].OnClick) {
			AudioManager.Instance.SetAudio(AudioManager.AudioFiles.ButtonClick, true);

			Camera.main.transform.position = normalCamera.position;
			Camera.main.transform.rotation = normalCamera.rotation;

			menuMain.Open();
		}
	}

	/** Open this GUI */
	public void Open() {
		menuMain.Close();

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

		foreach(SGUILabel label in labels)
			label.Activated = false;
	}

	private IEnumerator ButtonDelay() {
		yield return new WaitForSeconds(0.001f);

		foreach(SGUITexture texture in textures)
			texture.Activated = true;

		foreach(SGUIButton button in buttons)
			button.Activated = true;

		foreach(SGUILabel label in labels)
			label.Activated = true;
	}
}
