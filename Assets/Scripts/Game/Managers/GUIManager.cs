using UnityEngine;

public class GUIManager:Singleton<GUIManager> {
	[SerializeField] private Vector2 nativeSize;

	void OnGUI() {
		if(Event.current.type == EventType.Repaint)
			SGUI.SGUIButton.UpdateButtons(GUIManager.Instance.NativeSize);
	}

	public Vector2 NativeSize { get { return nativeSize; } }
}
