using UnityEngine;

public class GUIManager:Singleton<GUIManager> {
	[SerializeField] private Vector2 nativeSize;

	public static void UpdateGUIMatrix() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / GUIManager.Instance.nativeWidth, Screen.height / GUIManager.Instance.nativeHeight, 1));
	}

	public float nativeWidth { get { return nativeSize.x; } }

	public float nativeHeight { get { return nativeSize.y; } }
}
