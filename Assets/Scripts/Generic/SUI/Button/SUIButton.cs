using UnityEngine;
using System.Collections;

public class SUIButton:SUI {
	protected enum ButtonState {
		Normal,
		Hover,
		Click
	}

	protected ButtonState lastState;
	protected ButtonState state;

	protected bool initialized;

	/** Initialize the button, called automaticly */
	protected virtual void Initialize() {
		state = ButtonState.Normal;

		initialized = true;
	}

	/** Update the button */
	public virtual void Update(float nativeWidth, float nativeHeight) {
		lastState = state;
	}

	/** Check for a mouse click */
	protected void CheckForMouse(int width, int height, float nativeWidth, float nativeHeight) {
		Rect area = new Rect(position.x, position.y, width, height);
		Vector3 mousePosition = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

		mousePosition.x *= nativeWidth / Screen.width;
		mousePosition.y *= nativeHeight / Screen.height;

		if(!Input.GetMouseButton(0) && area.Contains(mousePosition)) {
			state = ButtonState.Hover;
		} else if(Input.GetMouseButton(0) && area.Contains(mousePosition)) {
			state = ButtonState.Click;
		} else {
			state = ButtonState.Normal;
		}
	}

	/** Check for a touch */
	protected void CheckForTouch(int width, int height, float nativeWidth, float nativeHeight) {
		if(Input.touchCount > 0) {
			Rect area = new Rect(position.x, position.y, width, height);

			for(int i= 0; i < Input.touchCount; i++) {
				Vector2 touchPosition = Input.GetTouch(i).position;

				touchPosition.x *= nativeWidth / Screen.width;
				touchPosition.y *= nativeHeight / Screen.height;

				if(area.Contains(touchPosition)) {
					state = ButtonState.Click;
					break;
				} else {
					state = ButtonState.Normal;
				}
			}
		} else {
			state = ButtonState.Normal;
		}
	}

	/** Return wheter or not the button is currently in it's default state */
	public bool Normal { get { return state == ButtonState.Normal; } }

	/** Return true the first frame when the button returned to it's default state */
	public bool OnNormal { get { return (state == ButtonState.Normal) && (lastState != ButtonState.Normal); } }

	/** Return wheter or not the button is currently being hovered */
	public bool Hover { get { return state == ButtonState.Hover; } }

	/** Return true the first frame when the button is hovered */
	public bool OnHover { get { return (state == ButtonState.Hover) && (lastState != ButtonState.Hover); } }

	/** Return wheter or not the button is currently being clicked */
	public bool Click { get { return state == ButtonState.Click; } }

	/** Return true the first frame when the button is clicked */
	public bool OnClick { get { return (state == ButtonState.Click) && (lastState != ButtonState.Click); } }
}
