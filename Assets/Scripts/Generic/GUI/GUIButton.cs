using UnityEngine;
using System;
using System.Collections.Generic;

namespace SGUI {
	[Serializable]
	public class SUI {
		[SerializeField] protected bool activated = true;
		
		[SerializeField] protected Rect bounds;
	}

	[Serializable]
	public class SUIButton:SUI {
		private static List<SUIButton> buttons = new List<SUIButton>();
		
		public SUIButton() {
			buttons.Add(this);
		}
		
		internal virtual void Update(Vector2 nativeScreenSize) { }
		
		public static void UpdateButtons(Vector2 nativeScreenSize) {
			foreach(SUIButton button in buttons)
				if(button.activated)
					button.Update(nativeScreenSize);
		}
	}
	
	[Serializable]
	public class SUITextureButton:SUIButton {
		[SerializeField] private Texture2D textureNormal;
		[SerializeField] private Texture2D textureHover;
		[SerializeField] private Texture2D textureActive;
		
		private Texture2D activeTexture;
		
		public SUITextureButton():base() {
			activeTexture = textureNormal;
		}
		
		internal override void Update(Vector2 nativeScreenSize) {
			if(activeTexture == null)
				return;
				
			Graphics.DrawTexture(bounds, activeTexture);
		}
	}
}