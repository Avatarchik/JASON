using UnityEngine;
using System.Collections;

public class GUIBehaviour : MonoBehaviour {
	private Vector2 nativeSize;
	public void OnGUI () {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / GUIManager.Instance.NativeSize.x, Screen.height / GUIManager.Instance.NativeSize.y, 1));
	}
}
