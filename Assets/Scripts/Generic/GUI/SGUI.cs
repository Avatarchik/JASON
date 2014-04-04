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

		protected bool destroyed;

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

	/** Label class */
	[Serializable]
	public class SGUILabel:SGUI {
		[SerializeField] protected String text;

		[SerializeField] protected Color textColor = Color.white;

		[SerializeField] protected Font font;

		[SerializeField] protected int fontSize;

		private GUIStyle style;
		
		/** Create the label */
		public virtual void Create() {
			style = new GUIStyle();

			SGUIManager.Instance.RegisterLabel(this);
		}

		/** Destroy the label */
		public void Destroy() {
			destroyed = true;

			SGUIManager.Instance.RemoveLabel(this);
		}

		/** Update the label */
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated || String.IsNullOrEmpty(text) || destroyed)
				return;

			style.normal.textColor = textColor;
			style.font = font;
			style.fontSize = fontSize;
			style.wordWrap = true;

			GUI.Label(bounds, new GUIContent(text), style);
		}

		/** Compare the label to another */
		public bool Equals(SGUILabel other) {
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(text.Equals(other.text) && textColor.Equals(other.textColor) && font.Equals(other.font) && fontSize.Equals(other.fontSize))
					return true;

			return false;
		}

		public string Text {
			set { text = value; }
			get { return text; }
		}
	}

	[Serializable]
	public class SGUISlowWriteLabel:SGUILabel {
		[SerializeField] private float minDelay;
		[SerializeField] private float maxDelay;

		private string originalText;

		private bool finished;

		/** Create the label */
		public override void Create() {
			base.Create();

			originalText = text;
			text = "";
		}

		/** Write the text */
		public IEnumerator Write() {
			yield return new WaitForSeconds(0.05f);

			if(originalText == null)
				throw new NullReferenceException("The label you're trying to write is null. Text: " + text);

			foreach(char letter in originalText.ToCharArray()) {
				if(!activated)
					yield break;

				text += letter;

				yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));
			}

			yield return new WaitForSeconds(0.1f);
			
			finished = true;
		}

		/** Compare the label to another */
		public bool Equals(SGUISlowWriteLabel other) {
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(text.Equals(other.text) && textColor.Equals(other.textColor) && font.Equals(other.font) && fontSize.Equals(other.fontSize))
					if(minDelay.Equals(other.minDelay) && maxDelay.Equals(other.maxDelay))
					return true;

			return false;
		}

		/** Whether or not the text has finished writing */
		public bool FinishedWriting {
			get { return finished; }
		}
	}

	/** Texture class */
	[Serializable]
	public class SGUITexture:SGUI {
		[SerializeField] private Texture2D texture;	

		[SerializeField] private string text;
		[SerializeField] private Font textFont;
		[SerializeField] private Color textColor = Color.white;
		[SerializeField] private int textSize;
		[SerializeField] private TextAnchor textAnchor;

		private GUIStyle style;

		private Color oldColor;
		private Color color = Color.white;

		/** Create the texture */
		public void Create() {
			style = new GUIStyle();

			SGUIManager.Instance.RegisterTexture(this);
		}

		/** Destroy the texture */
		public void Destroy() {
			destroyed = true;

			SGUIManager.Instance.RemoveTexture(this);
		}

		/** Update the texture */
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated || texture == null || destroyed)
				return;

			oldColor = GUI.color;
			GUI.color = color;

			if(!String.IsNullOrEmpty(text)) {
				style.normal.background = texture;

				style.font = textFont;
				style.fontSize = textSize;
				style.alignment = textAnchor;
				style.normal.textColor = textColor;

				GUI.Label(bounds, new GUIContent(text), style);
			} else {
				GUI.DrawTexture(bounds, texture);
			}

			GUI.color = oldColor;
		}

		/** Compare the texture to another */
		public bool Equals(SGUITexture other) {
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(texture.Equals(other.texture))
					return true;
			
			return false;
		}

		/** Get the texture */
		public Texture2D Texture {
			get { return texture; }
		}

		public Color Color {
			set { color = value; }
		}
	}

	/** Button base class */
	[Serializable]
	public class SGUIButton:SGUI {
		public enum ButtonState {
			NORMAL,
			HOVER,
			ACTIVE
		}
		
		[SerializeField] private Texture2D textureNormal;
		[SerializeField] private Texture2D textureHover;
		[SerializeField] private Texture2D textureActive;
		
		[SerializeField] private string text;
		[SerializeField] private Font textFont;
		[SerializeField] private Color textColor = Color.white;
		[SerializeField] private int textSize;
		[SerializeField] private TextAnchor textAnchor;

		private Texture2D currentTexture;

		private ButtonState state;
		private ButtonState lastState;

		private GUIStyle style;

		private bool stateChanged;
		private bool manualEdit;

		/** Create the button */
		public void Create() {
			style = new GUIStyle();

			state = ButtonState.NORMAL;

			currentTexture = textureNormal;
			
			SGUIManager.Instance.RegisterButton(this);
		}

		/** Destroy the button */
		public void Destroy() {
			destroyed = true;

			SGUIManager.Instance.RemoveButton(this);
		}

		/** Compare the button to another */
		public bool Equals(SGUIButton other) {
			if(activated.Equals(other.activated && bounds.Equals(other.bounds)))
				if(textureNormal.Equals(other.textureNormal) && textureHover.Equals(other.textureHover) && textureActive.Equals(other.textureActive))
					if(text.Equals(other.text) && textFont.Equals(other.textFont) && textSize.Equals(other.textSize) && textAnchor.Equals(other.textAnchor))
						return true;

			return false;
		}

		/** Start automaticly updating the button again */
		public void ResetState() {
			manualEdit = true;
		}

		/** Update the button */
		internal void Update(Vector2 nativeScreenSize) {
			if(!activated || destroyed)
				return;

			if(!manualEdit) {
				switch(Application.platform) {
				case RuntimePlatform.Android:
					HandleTouchInput();
					break;
				case RuntimePlatform.WindowsEditor:
					HandleTouchInput();
					HandleInput();
					break;
				default:
					HandleInput();
					break;
				}
			}

			if(!String.IsNullOrEmpty(text)) {
				style.normal.background = currentTexture;
				
				style.font = textFont;
				style.fontSize = textSize;
				style.alignment = textAnchor;
				style.normal.textColor = textColor;
				
				GUI.Label(bounds, new GUIContent(text), style);
			} else {
				GUI.DrawTexture(bounds, currentTexture);
			}
			
			SwitchTexture();
		}

		/** Handle touch input */
		private void HandleTouchInput() {
			if(Input.touchCount <= 0)
				return;

			foreach(Touch touch in Input.touches) {
				if(touch.phase == TouchPhase.Began) {
					if(touch.position.x >= bounds.x && touch.position.x <= bounds.x + bounds.width && touch.position.y >= bounds.y && touch.position.y <= bounds.y + bounds.height) {
						UpdateState(ButtonState.ACTIVE);
					} else {
						UpdateState(ButtonState.NORMAL);
					}
				}
			}
		}

		/** Handle mouse input */
		private void HandleInput() {
			Vector2 mouse = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);

			if(mouse.x >= bounds.x && mouse.x <= bounds.x + bounds.width && mouse.y >= bounds.y && mouse.y <= bounds.y + bounds.height) {
				if(Input.GetMouseButtonDown(0)) {
					UpdateState(ButtonState.ACTIVE);
				} else {
					UpdateState(ButtonState.HOVER);
				}
			} else {
				UpdateState(ButtonState.NORMAL);
			}
		}

		/** Update the button's state */
		private void UpdateState(ButtonState newState) {
			lastState = state;
			state = newState;

			if(lastState != state)
				stateChanged = true;
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
				currentTexture = textureActive;
				break;
			}
		}

		/** Return wheter or not the button was updated to it's default state */
		public bool OnNormal {
			get {
				if(state == ButtonState.NORMAL && stateChanged) {
					stateChanged = false;
					return true;
				}

				return false;
			}
		}

		/** Return wheter or not the button was updated to it's hover state */
		public bool OnHover {
			get {
				if(state == ButtonState.HOVER && stateChanged) {
					stateChanged = false;
					return true;
				}

				return false;
			}
		}

		/** Return wheter or not the button was updated to it's active state */
		public bool OnClick {
			get {
				if(state == ButtonState.ACTIVE && stateChanged) {
					stateChanged = false;
					return true;
				}

				return false;
			}
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

        /** Set and/or get the state */
        public ButtonState State {
            set {
				manualEdit = true;
				state = value;
			}

			get { return state; }
        }

		/** Get wheter or not the state has been manually changed */
		public bool IsManual {
			get { return manualEdit; }
		}

		/** Get the text color of the label */
		public Color TextColor {
			set { textColor = value; }
			get { return textColor; }
		}
	}
}