using UnityEngine;
using System;

[Serializable]
public class SUISpriteButton:SUIButton {
	[SerializeField] private Texture spriteSheet;
	[SerializeField] private SUISprite spriteNormal;
	[SerializeField] private SUISprite spriteHover;
	[SerializeField] private SUISprite spriteClick;
	
	private SUISprite spriteAcive;

	/** Initialize the button, called automaticly */
	protected override void Initialize() {
		base.Initialize();
		
		spriteAcive = spriteNormal;
	}

	/** Update the button */
	public override void Update(float nativeWidth, float nativeHeight) {
		if(!activated)
			return;

		if(!initialized)
			Initialize();

		base.Update(nativeWidth, nativeHeight);
		
		switch(Application.platform) {
		case RuntimePlatform.Android:
			CheckForTouch((int)spriteAcive.Size.x, (int)spriteAcive.Size.y, nativeWidth, nativeHeight);
			break;
		default:
			CheckForMouse((int)spriteAcive.Size.x, (int)spriteAcive.Size.y, nativeWidth, nativeHeight);
			break;
		}
		
		if(lastState != state)
			UpdateTexture();
		
		spriteAcive.Draw(position, spriteSheet);
	}

	/** Update the texture of the button */
	private void UpdateTexture() {
		switch(state) {
		case ButtonState.Normal:
			spriteAcive = spriteNormal;
			break;
		case ButtonState.Hover:
			spriteAcive = spriteHover;
			break;
		case ButtonState.Click:
			spriteAcive = spriteClick;
			break;
		}
	}

	/** Return the normal SUISprite attached to the button */
	public SUISprite SpriteNormal { get { return spriteNormal; } }
	
	/** Return the hover SUISprite attached to the button */
	public SUISprite SpriteHover { get { return spriteHover; } }
	
	/** Return the click SUISprite attached to the button */
	public SUISprite SpriteClick { get { return spriteClick; } } 
}