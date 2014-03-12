using UnityEngine;
using System;
using System.Collections.Generic;

namespace SGUI {
	[Serializable]
	public class SGUI {
		[SerializeField] protected bool activated = true;
		
		[SerializeField] protected Rect bounds;
	}

	[Serializable]
	public class SGUIButton:SGUI {
		private static List<SGUIButton> buttons = new List<SGUIButton>();

		protected enum ButtonState {
			NORMAL,
			HOVER,
			ACTIVE
		}

		protected ButtonState state;
		protected ButtonState oldState;
		
		public SGUIButton() {
			state = ButtonState.NORMAL;
			oldState = ButtonState.NORMAL;

			buttons.Add(this);
		}

		public void Destroy() {
			buttons.Remove(this);
		}

		/** Update the button */
		internal virtual void Update(Vector2 nativeScreenSize) { }

		/** Update the texture of the button */
		internal virtual void SwitchTexture() { }

		/** Return wheter or not the button is currently in it's default state */
		public bool Normal { get { return state == ButtonState.NORMAL; } }
		
		/** Return true the first frame when the button returned to it's default state */
		public bool OnNormal { get { return (state == ButtonState.NORMAL) && (oldState != ButtonState.NORMAL); } }
		
		/** Return wheter or not the button is currently being hovered */
		public bool Hover { get { return state == ButtonState.HOVER; } }
		
		/** Return true the first frame when the button is hovered */
		public bool OnHover { get { return (state == ButtonState.HOVER) && (oldState != ButtonState.HOVER); } }
		
		/** Return wheter or not the button is currently being clicked */
		public bool Click { get { return state == ButtonState.ACTIVE; } }
		
		/** Return true the first frame when the button is clicked */
		public bool OnClick { get { return (state == ButtonState.ACTIVE) && (oldState != ButtonState.ACTIVE); } }

		/** Update the buttons */
		public static void UpdateButtons(Vector2 nativeScreenSize) {
			foreach(SGUIButton button in buttons) {
				if(button.activated) {
					if(isMouseOver(button)) {
						button.state = ButtonState.HOVER;

						if(Input.GetMouseButton(0)) 
							button.state = ButtonState.ACTIVE;
					} else {
						button.state = ButtonState.NORMAL;
					}

					button.Update(nativeScreenSize);

					if(button.state != button.oldState)
						button.SwitchTexture();
				}
			}
		}

		/** Return wheter or not the mouse is hovering */
		private static bool isMouseOver(SGUIButton button) {
			Vector3 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

			if(mouse.x >= button.bounds.x && mouse.x <= button.bounds.x + button.bounds.width &&
			   mouse.y >= button.bounds.y && mouse.y <= button.bounds.y + button.bounds.width)
				return true;

			return false;
		}
	}
	
	[Serializable]
	public class SGUITextureButton:SGUIButton {
		[SerializeField] private Texture2D textureNormal;
		[SerializeField] private Texture2D textureHover;
		[SerializeField] private Texture2D textureActive;
		
		private Texture2D currentTexture;
		
		internal override void Update(Vector2 nativeScreenSize) {
			if(currentTexture == null)
				currentTexture = textureNormal;

			Graphics.DrawTexture(bounds, currentTexture);
		}

		internal override void SwitchTexture() {
			switch(state) {
			case ButtonState.NORMAL:
				currentTexture = textureNormal;
				break;
			case ButtonState.HOVER:
				currentTexture = textureHover;
				break;
			case ButtonState.ACTIVE:
				currentTexture = textureActive;
				break;
			}
		}
	}

	[Serializable]
	public class SGUISpriteButton:SGUIButton {
		[SerializeField] private Texture2D spriteSheet;
		[SerializeField] private Vector2 spriteSheetSize;

		[SerializeField] private Vector2 spriteNormal;
		[SerializeField] private Vector2 spriteHover;
		[SerializeField] private Vector2 spriteActive;

		private Vector2 currentSprite;

		private bool initialized;

		internal override void Update(Vector2 nativeScreenSize) {
			if(!initialized) {
				currentSprite = spriteNormal;

				initialized = true;
			}

			Graphics.DrawTexture(bounds, spriteSheet, new Rect(currentSprite.x / spriteSheetSize.x, currentSprite.y / spriteSheetSize.y, 0, 0), 0, 0, 0, 0);
		}

		internal override void SwitchTexture() {
			switch(state) {
			case ButtonState.NORMAL:
				currentSprite = spriteNormal;
				break;
			case ButtonState.HOVER:
				currentSprite = spriteHover;
				break;
			case ButtonState.ACTIVE:
				currentSprite = spriteActive;
				break;
			}
		}
	}
}