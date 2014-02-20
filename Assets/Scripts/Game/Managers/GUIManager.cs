using UnityEngine;

public class GUIManager:Singleton<GUIManager> {
	[SerializeField] private Vector2 nativeSize;

	public static void UpdateGUIMatrix() {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / GUIManager.Instance.nativeSize.x, Screen.height / GUIManager.Instance.nativeSize.y, 1));
	}

	public Vector2 NativeSize { get { return nativeSize; } }
}
