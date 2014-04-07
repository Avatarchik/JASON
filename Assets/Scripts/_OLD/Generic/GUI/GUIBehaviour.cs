using UnityEngine;
using System.Collections;

public class GUIBehaviour:MonoBehaviour {
	private Vector2 nativeSize = new Vector2(1920, 1080);

	protected virtual void OnGUI () {
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Screen.width / nativeSize.x, Screen.height / nativeSize.y, 1));
	}
}
