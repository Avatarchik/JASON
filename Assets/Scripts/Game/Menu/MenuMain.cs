using UnityEngine;
using SGUI;

public class MenuMain:GUIBehaviour {
	//public SGUITextureButton fap;
	public SGUISpriteButton fap2;

	protected override void OnGUI() {
		base.OnGUI();

		if(fap2.OnClick)
			Debug.Log("On Click");

		if(fap2.OnHover)
			Debug.Log("On Hover");

		if(fap2.OnNormal)
			Debug.Log("On Normal");
	}
}
