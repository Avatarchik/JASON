using UnityEngine;
using System;

[Serializable]
public class SUITextureButton:SUIButton {
	[SerializeField] private SUITexture textureNormal;
	[SerializeField] private SUITexture textureHover;
	[SerializeField] private SUITexture textureClick;

	private SUITexture textureActive;

	/** Initialize the button, called automaticly */
	protected override void Initialize() {
		base.Initialize();

		textureActive = textureNormal;
	}

	/** Update the button */
	public override void Update(float nativeWidth, float nativeHeight) {
		if(!activated)
			return;

		if(!initialized)
			Initialize();

		switch(Application.platform) {
		case RuntimePlatform.Android:
			CheckForTouch(textureActive.Texture.width, textureActive.Texture.height, nativeWidth, nativeHeight);
			break;
		default:
			CheckForMouse(textureActive.Texture.width, textureActive.Texture.height, nativeWidth, nativeHeight);
			break;
		}

		if(lastState != state)
			UpdateTexture();

		textureActive.Draw(position);

		base.Update(nativeWidth, nativeHeight);
	}

	/** Update the texture of the button */
	private void UpdateTexture() {
		switch(state) {
		case ButtonState.Normal:
			textureActive = textureNormal;
			break;
		case ButtonState.Hover:
			textureActive = textureHover;
			break;
		case ButtonState.Click:
			textureActive = textureClick;
			break;
		}
	}

	/** Return the normal SUITexture attached to the button */
	public SUITexture TextureNormal { get { return textureNormal; } }

	/** Return the hover SUITexture attached to the button */
	public SUITexture TextureHover { get { return textureHover; } }

	/** Return the click SUITexture attached to the button */
	public SUITexture TextureClick { get { return textureClick; } } 
}
