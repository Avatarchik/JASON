using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace SGUI {
	/** SGUI base class */
	[Serializable]
	public class SGUI {
		[SerializeField] protected bool activated = true;

		[SerializeField] protected Rect bounds;

		/** Set and/or get wheter or not the GUI element is activated */
		public bool Activated {
			set { activated = value; }
			get { return activated; }
		}

		/** Set and/or get the bounds of the GUI element */
		public Rect Bounds {
			set { bounds = value; }
			get { return bounds; }
		}
	}

	/** Texture class */
	[Serializable]
	public class SGUITexture:SGUI {
		[SerializeField] private Texture2D texture;	

		/** Create the texture */
		public void Create() {
			SGUIManager.Instance.RegisterTexture(this);
		}

		/** Destroy the texture */
		public void Destroy() {
			SGUIManager.Instance.RemoveTexture(this);
		}

		/** Update the texture */
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated || texture == null)
				return;

			GUI.DrawTexture(bounds, texture);
		}

		/** Compare the texture to another */
		private bool Equals(SGUITexture other) {
			bool result = false;
			
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(texture.Equals(other.texture))
					result = true;
			
			return result;
		}

		/** Get the texture */
		public Texture2D Texture { get { return texture; } }
	}

	/** Sprite class */
	[Serializable]
	public class SGUISprite:SGUI {
		[SerializeField] private Texture2D spriteSheet;
		[SerializeField] private Vector2 spriteSheetSize;
		
		[SerializeField] private Vector2 coordinates;

		/** Create the sprite */
		public void Create() {		
			SGUIManager.Instance.RegisterSprite(this);
		}
		
		/** Destroy the sprite */
		public void Destroy() {
			SGUIManager.Instance.RemoveSprite(this);
		}
		
		/** Update the sprite */
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated)
				return;

			GUI.DrawTextureWithTexCoords(bounds, spriteSheet, new Rect(coordinates.x / spriteSheetSize.x, coordinates.y / spriteSheetSize.y, 0, 0));
		}

		/** Compare the sprite to another */
		private bool Equals(SGUISprite other) {
			bool result = false;
			
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(spriteSheet.Equals(other.spriteSheet) && spriteSheetSize.Equals(other.spriteSheetSize) && coordinates.Equals(other.coordinates))
					result = true;
			
			return result;
		}
	}

	/** Button base class */
	[Serializable]
	public class SGUIButton:SGUI {
		protected enum ButtonState {
			NORMAL,
			HOVER,
			ACTIVE
		}

		protected ButtonState state;

		/** Create the button */
		public void Create() {		
			state = ButtonState.NORMAL;
			
			SGUIManager.Instance.RegisterButton(this);
		}

		/** Destroy the button */
		public void Destroy() {
			SGUIManager.Instance.RemoveButton(this);
		}

		/** Update the button */
		internal virtual void Update(Vector2 nativeScreenSize) { }

		/** Update the texture of the button */
		internal virtual void SwitchTexture() { }
		
		/** Return wheter or not the mouse is hovering */
		internal bool IsMouseOver(SGUIButton button) {
			Vector3 mouse = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
			
			if(mouse.x >= button.bounds.x && mouse.x <= button.bounds.x + button.bounds.width &&
			   mouse.y >= button.bounds.y && mouse.y <= button.bounds.y + button.bounds.width)
				return true;
			
			return false;
		}

		/** Return wheter or not the button is currently in it's default state */
		public bool Normal { get { return state == ButtonState.NORMAL; } }
		
		/** Return wheter or not the button is currently being hovered */
		public bool Hover { get { return state == ButtonState.HOVER; } }
		
		/** Return wheter or not the button is currently being clicked */
		public bool Click { get { return state == ButtonState.ACTIVE; } }
	}

	/** Texture button class */
	[Serializable]
	public class SGUITextureButton:SGUIButton {
		[SerializeField] private Texture2D textureNormal;
		[SerializeField] private Texture2D textureHover;
		[SerializeField] private Texture2D textureActive;
		
		private Texture2D currentTexture;

		/** Update the button */
		internal override void Update(Vector2 nativeScreenSize) {
			if(!activated)
				return;
				
			if(currentTexture == null)
				currentTexture = textureNormal;
				
			if(IsMouseOver(this)) {
				state = ButtonState.HOVER;
				
				if(Input.GetMouseButton(0)) 
					state = ButtonState.ACTIVE;
			} else {
				state = ButtonState.NORMAL;
			}

			GUI.DrawTexture(bounds, currentTexture);
			
			SwitchTexture();
		}

		/** Switch the texture of the button */
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

		/** Compare the button to another */
		private bool Equals(SGUITextureButton other) {
			bool result = false;
			
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(textureNormal.Equals(other.textureNormal) && textureHover.Equals(other.textureHover) && textureActive.Equals(other.textureActive))
					result = true;
			
			return result;
		}
	}

	/** Sprite button class */
	[Serializable]
	public class SGUISpriteButton:SGUIButton {
		[SerializeField] private Texture2D spriteSheet;
		[SerializeField] private Vector2 spriteSheetSize;

		[SerializeField] private Vector2 spriteNormal;
		[SerializeField] private Vector2 spriteHover;
		[SerializeField] private Vector2 spriteActive;

		private Vector2 currentSprite;

		private bool initialized;

		/** Update the button */
		internal override void Update(Vector2 nativeScreenSize) {
			if(!activated)
				return;
			
			if(!initialized) {
				currentSprite = spriteNormal;
				
				initialized = true;
			}
			
			if(IsMouseOver(this)) {
				state = ButtonState.HOVER;
				
				if(Input.GetMouseButton(0)) 
					state = ButtonState.ACTIVE;
			} else {
				state = ButtonState.NORMAL;
			}	

			GUI.DrawTextureWithTexCoords(bounds, spriteSheet, new Rect(currentSprite.x / spriteSheetSize.x, currentSprite.y / spriteSheetSize.y, 0, 0));
			
			SwitchTexture();
		}

		/** Switch the texture of the button */
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

		/** Switch the texture of the button */
		private bool Equals(SGUISpriteButton other) {
			bool result = false;
			
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(spriteSheet.Equals(other.spriteSheet) && spriteSheetSize.Equals(other.spriteSheetSize) && spriteNormal.Equals(other.spriteNormal) && spriteHover.Equals(other.spriteHover) && spriteActive.Equals(other.spriteActive))
					result = true;
			
			return result;
		}
	}
}