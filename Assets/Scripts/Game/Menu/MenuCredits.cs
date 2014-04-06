using UnityEngine;
using System.Collections;
using SGUI;

public class MenuCredits:GUIBehaviour {
	[SerializeField] private Transform normalCamera;

	[SerializeField] private Texture credits;

	[SerializeField] private GUIStyle back;

	private MenuMain menuMain;

	private bool opened = false;

	void Start() {
		menuMain = GetComponent<MenuMain>();
	}

	protected override void OnGUI() {
		if(!opened)
			return;

		base.OnGUI();

		GUI.DrawTexture(new Rect(181.7f, -6.1f, 1536, 850.5f), credits);

		if(GUI.Button(new Rect(697, 915, 517.5f, 158.25f), new GUIContent("Back"), back)) {
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
	}

	/** Close this GUI */
	public void Close() {
		opened = false;
	}
}
