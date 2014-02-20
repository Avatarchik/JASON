using UnityEngine;

public class MenuMain:MonoBehaviour {
	public SUITextureButton[] buttons;

	void OnGUI() {
		GUIManager.UpdateGUIMatrix();

		foreach(SUITextureButton button in buttons) {
			button.Update(GUIManager.Instance.nativeWidth, GUIManager.Instance.nativeHeight);

			if(button.OnClick)
				HandleButton(button.ID);
		}
	}

	private void HandleButton(int id) {
		Debug.Log(id);
	}
}
