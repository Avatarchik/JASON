using UnityEngine;
using SGUI;

public class GUIManager:Singleton<GUIManager> {
	[SerializeField] private Vector2 nativeSize;

	public Vector2 NativeSize { get { return nativeSize; } }
}
