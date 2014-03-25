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

	/** Button base class */
	[Serializable]
	public class SGUIButton:SGUI {
		public enum ButtonState {
			NORMAL,
			HOVER,
			ACTIVE,
			TOGGLED
		}
		
		[SerializeField] private Texture2D textureNormal;
		[SerializeField] private Texture2D textureHover;
		[SerializeField] private Texture2D textureActive;
		
		[SerializeField] private string text;
		[SerializeField] private Font textFont;
		[SerializeField] private int textSize;
		[SerializeField] private TextAnchor textAnchor;
		
		[SerializeField] private bool isToggle;
		
		private ButtonState state;
		
		private Texture2D currentTexture;
		
		private bool wasMouseDown;
		private bool firstToggle;
        private bool manualEdit;

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
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated)
				return;
			
			if(currentTexture == null)
				currentTexture = textureNormal;

            if(Application.platform == RuntimePlatform.Android) {
                if(Input.touchCount <= 0)
                    return;
            } else {
                if(IsMouseOver()) {
                    if(state != ButtonState.TOGGLED)
                        state = ButtonState.HOVER;
                    if(!isToggle) {
                        if(Input.GetMouseButton(0))
                            state = ButtonState.ACTIVE;
                    } else {
                        if(!wasMouseDown && Input.GetMouseButtonDown(0)) {
                            if(state == ButtonState.HOVER) {
                                state = ButtonState.TOGGLED;
                            } else {
                                state = ButtonState.ACTIVE;
                            }

                            firstToggle = true;
                            wasMouseDown = true;
                        } else {
                            wasMouseDown = false;
                        }
                    }
                } else {
                    if(state != ButtonState.TOGGLED)
                        state = ButtonState.NORMAL;
                }
            }
			
			if(!String.IsNullOrEmpty(text)) {
				GUIStyle style = new GUIStyle();
				
				style.normal.background = currentTexture;
				
				style.font = textFont;
				style.fontSize = textSize;
				style.alignment = textAnchor;
				
				GUI.Label(bounds, new GUIContent(text), style);
			} else {
				GUI.DrawTexture(bounds, currentTexture);
			}
			
			SwitchTexture();
		}

		/** Update the texture of the button */
		private void SwitchTexture() {
			switch(state) {
			case ButtonState.NORMAL:
				currentTexture = textureNormal;
				break;
			case ButtonState.HOVER:
				currentTexture = textureHover;
				break;
			case ButtonState.ACTIVE:
			case ButtonState.TOGGLED:
				currentTexture = textureActive;
				break;
			}
		}
		
		/** Return wheter or not the mouse is hovering */
		private bool IsMouseOver() {
			Vector3 mouse = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
			
			if(mouse.x >= bounds.x && mouse.x <= bounds.x + bounds.width &&
			   mouse.y >= bounds.y && mouse.y <= bounds.y + bounds.width)
				return true;
			
			return false;
		}
		
		/** Compare the button to another */
		private bool Equals(SGUIButton other) {
			bool result = false;
			
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(textureNormal.Equals(other.textureNormal) && textureHover.Equals(other.textureHover) && textureActive.Equals(other.textureActive))
					result = true;
			
			return result;
		}

		/** Return wheter or not the button is currently in it's default state */
		public bool Normal {
			get { return state == ButtonState.NORMAL; }
		}
		
		/** Return wheter or not the button is currently being hovered */
		public bool Hover {
			get { return state == ButtonState.HOVER; }
		}
		
		/** Return wheter or not the button is currently being clicked */
		public bool Click {
			get { return state == ButtonState.ACTIVE; }
		}
		
		/** Return wheter or not the button is toggled */
		public bool Toggle {
			get {
				if(firstToggle) {
					firstToggle = false;
					return true;
				}
				
				return false;
			}
		}

        /** Manually set the state */
        public void SetState(ButtonState state) {
            manualEdit = true;
            this.state = state;

            if(state == ButtonState.TOGGLED)
                firstToggle = true;
        }

        /** Automaticly detect the state */
        public void ResetState() {
            manualEdit = false;
        }
	}
}